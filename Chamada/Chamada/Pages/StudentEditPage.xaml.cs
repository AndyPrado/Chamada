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
	public partial class StudentEditPage : ContentPage
	{
        Student _student;
        SQLiteAsyncConnection _connection;

		public StudentEditPage (Student student)
		{
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _student = student;
			InitializeComponent ();

            EntryEdit.Text = student.Name;
		}

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            _student.Name = EntryEdit.Text;
            await _connection.CreateTableAsync<Student>();
            await _connection.UpdateAsync(_student);
            await Navigation.PopModalAsync();
        }
    }
}