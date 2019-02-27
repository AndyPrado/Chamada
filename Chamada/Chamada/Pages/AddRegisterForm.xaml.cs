using Chamada.Models;
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
    public partial class AddRegisterForm : ContentPage
    {
        Group _group;
        ObservableCollection<Student> _students;
        CheckBox _isSelected;
        Register _register;
        List<Student> _absentStudent;

        public AddRegisterForm(Group group, ObservableCollection<Student> students)
        {
            InitializeComponent();

            _register = new Register();
            _isSelected = new CheckBox();
            _group = group;
            _students = students;
            _absentStudent = new List<Student>();
        }

        protected override void OnAppearing()
        {
            //preenche a lista de alunos na chamada
            lvStudentsRegister.ItemsSource = _students;
            base.OnAppearing();
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

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _register.Day = dpClassDate.Date;
            _register.GroupId = _group.Id;
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new RegisterHomeworkForm(_register, _absentStudent));
           
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}