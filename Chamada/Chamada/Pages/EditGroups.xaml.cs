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
	public partial class EditGroups : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        public ObservableCollection<Group> _groups { get; set; }

        public EditGroups ()
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CreateTables();
            LoadGroups();
        }


        public async void LoadGroups()
        {
            var groups = await _connection.Table<Group>().ToListAsync();
            _groups = new ObservableCollection<Group>(groups);
            groupList.ItemsSource = _groups;
        }

        //Create the tables on the db
        private async System.Threading.Tasks.Task CreateTables()
        {
            await _connection.CreateTableAsync<Group>();
            await _connection.CreateTableAsync<Student>();
            await _connection.CreateTableAsync<Register>();
        }

        private void groupList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var groupSelected = (Group)((ListView)sender).SelectedItem;            
            Navigation.PushAsync(new EditGroupFrom(groupSelected));
            
        }
    }
}