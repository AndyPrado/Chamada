using Chamada.Database;
using Chamada.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace Chamada.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditRegisterPage : ContentPage
    {
        Register _register;
        List<Student> _absentStudent;
        CheckBox _isSelected;
        ObservableCollection<Student> _students;
        SQLiteAsyncConnection _connection;
        string _absentList;
        

        public EditRegisterPage(Register register)
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<Student>();
            _isSelected = new CheckBox();
            _absentStudent = new List<Student>();
            _register = register;

            dpClassDate.Date = new DateTime(_register.Day.Year, _register.Day.Month, _register.Day.Day);
            editorClasswork.Text = _register.Classwork;
            editorECampus.Text = _register.ECampus;
            editorHomework.Text = _register.Homework;
            _register.AbsentStudents = "";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var students = await _connection.Table<Student>().Where(s => s.GroupId == _register.GroupId).ToListAsync();
            _students = new ObservableCollection<Student>(students);            

            lvStudentsRegister.ItemsSource = _students;        
        }       


        void OnSelection(object sender, EventArgs args)
        {
            _isSelected = (CheckBox)sender;
            var selectedStudent = _isSelected.BindingContext as Student;

            //adiciona e remove os alunos à lista
            if (_isSelected.Checked)
            {
                _absentStudent.Add(selectedStudent);
            }
            if (!_isSelected.Checked)
            {
                _absentStudent.Remove(selectedStudent);
            }            
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {

            _register.Day = dpClassDate.Date;
            _register.Classwork = editorClasswork.Text;
            _register.ECampus = editorECampus.Text;
            _register.Homework = editorHomework.Text;

            foreach (Student s in _absentStudent)
            {
                _absentList += s.Name + ",";
            }

            if (_absentList == null)
            {
                _register.AbsentStudents = "No absences";
            }
            else
            {
                _register.AbsentStudents = _absentList;
            }


            var answer = await DisplayAlert("Confirmation", "Are you sure you want to save changes?", "Yes", "No");

            if (answer)
            {
                await _connection.CreateTableAsync<Register>();                
                await _connection.UpdateAsync(_register);                
            }
            await Navigation.PopToRootAsync();

        }
    }
}