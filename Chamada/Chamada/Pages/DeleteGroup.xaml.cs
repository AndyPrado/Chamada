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
	public partial class DeleteGroup : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        public ObservableCollection<Group> _groups { get; set; }

        public DeleteGroup ()
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            
        }

        protected async override void OnAppearing()
        {
            await CreateTables();
            LoadGroups();

            base.OnAppearing();
        }

        public async void LoadGroups()
        {
            var groups = await _connection.Table<Group>().ToListAsync();
            _groups = new ObservableCollection<Group>(groups);
            groupList.ItemsSource = _groups;
        }

        public async void DeleteGroups(Group group)
        {
            //open and load tables
            await CreateTables();
            var students = await _connection.Table<Student>().Where(s => s.GroupId == group.Id).ToListAsync();
            var registers = await _connection.Table<Register>().Where(r => r.GroupId == group.Id).ToArrayAsync();

            //run through the list of students and delete them
            foreach (Student s in students)
            {
                await _connection.DeleteAsync(s);
            }

            //run through the list of registers and delete them
            foreach (Register r in registers)
            {
                await _connection.DeleteAsync(r);
            }

            //delete the group itself
            await _connection.DeleteAsync(group);

            //reload the groups on the page
            LoadGroups();

        }

        //Create the tables on the db
        private async System.Threading.Tasks.Task CreateTables()
        {
            await _connection.CreateTableAsync<Group>();
            await _connection.CreateTableAsync<Student>();
            await _connection.CreateTableAsync<Register>();
        }

        private async void groupList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedGroup = (Group)((ListView)sender).SelectedItem;

            var answer = await DisplayAlert("Confirmation", "Are you sure you want to delete this group?", "Yes", "No");

            if (answer)
            {
                DeleteGroups(selectedGroup);                
                
                await Navigation.PopAsync();
            }
             
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
                LoadGroups();
        }
    }
}