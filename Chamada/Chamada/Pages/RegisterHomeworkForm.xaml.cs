using Chamada.Database;
using Chamada.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chamada.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterHomeworkForm : ContentPage
    {
        SQLiteAsyncConnection _connection;
        Register _register;
        List<Student> _students;
        string _absentStudents;

        public RegisterHomeworkForm(Register register, List<Student> students)
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _register = register;
            _students = students;

            foreach (Student s in _students)
            {
                _absentStudents += s.Name + ",";
            }

            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await _connection.CreateTableAsync<Register>();

            base.OnAppearing();

        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _register.Classwork = EntryClasswork.Text;
            _register.ECampus = EntryECampus.Text;
            _register.Homework = EntryHomework.Text;
            _register.AbsentStudents = _absentStudents;

            var alert = await DisplayAlert("Confirmation",
                "Are you sure you want to save this register?",
                "Yes", "No");

            if (alert)
            {
                await _connection.InsertAsync(_register);
                await Navigation.PopModalAsync();
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}