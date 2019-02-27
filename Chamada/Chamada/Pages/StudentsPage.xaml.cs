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

namespace Chamada.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentsPage : ContentPage
	{
        public Group _group;
        SQLiteAsyncConnection _connection;
        public Student _student = new Student();
        public ObservableCollection<Student> _students { get; set; }

		public StudentsPage (Group group)
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _group = group;                      
        }

        protected async override void OnAppearing()
        {
            
            await _connection.CreateTableAsync<Student>();
            LoadStudents();

            base.OnAppearing();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            _student.Name = NameEntry.Text;
            _student.GroupId = _group.Id;

            await _connection.InsertAsync(_student);
            LoadStudents();
            NameEntry.Text = "";
        }

        public async void LoadStudents()
        {
            var students = await _connection.Table<Student>().Where(s => s.GroupId == _group.Id).OrderBy(s => s.Name)
                .ToListAsync();

            _students = new ObservableCollection<Student>(students);
            lvStudents.ItemsSource = _students;
        }

        private async void lvStudents_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var studentSelected = (Student)((ListView)sender).SelectedItem;

            var alert = await DisplayAlert("Edit or Delete?", "What do you want to do?", "Edit", "Delete");

            if (alert)
            {
                await Navigation.PushModalAsync(new StudentEditPage(studentSelected));
            }
            else
            {
                await _connection.CreateTableAsync<Student>();
                await _connection.DeleteAsync(studentSelected);
                LoadStudents();
            }
        }        
    }
}