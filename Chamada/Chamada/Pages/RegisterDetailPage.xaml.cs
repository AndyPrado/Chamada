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
	public partial class RegisterDetailPage : ContentPage
	{
        Register _register;
        string[] _absentStudents;
        string present = "all students were present";
        List<string> _studentsList;
        public List<string> Students { get; set; }
        SQLiteAsyncConnection _connection;

        public RegisterDetailPage (Register register)
		{
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _register = register;            
            _studentsList = new List<string>();
            if (register.AbsentStudents != null)
            {
                _absentStudents = new string[26];
                _absentStudents = register.AbsentStudents.Split(',');
            }
            else
            {
                _absentStudents = new string[1];
                _absentStudents[0] = present;
            }
                
            
            Title = _register.Day.ToString("dd/MMM/yyyy");

            InitializeComponent ();

            foreach (string s in _absentStudents)
            {
                var nome = s;
                var label = new Label();
                label.Text = s;
                label.TextColor = Color.DarkRed;
                label.FontSize = 18;
                label.Margin = new Thickness(10, 0, 10, 0);
                SLAbsentStudents.Children.Add(label);
            }

            //put absent students on a list to pass it to the editing page
            foreach (string s in _absentStudents)
            {
                _studentsList.Add(s);
            }

            lblClasswork.Text = _register.Classwork;
            lbleCampus.Text = _register.ECampus;
            lblHomework.Text = _register.Homework;
        }
               
        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new EditRegisterPage(_register));            
            Array.Clear(_absentStudents, 0, _absentStudents.Length);
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this register?", "Yes", "No");

            if (answer)
            {
                await _connection.CreateTableAsync<Register>();
                await _connection.DeleteAsync(_register);
                await Navigation.PopAsync();
            }            
        }
    }
}