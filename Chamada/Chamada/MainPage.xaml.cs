﻿using Chamada.Database;
using Chamada.Models;
using Chamada.Pages;
using SQLite;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Chamada
{
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public ObservableCollection<Group> _groups { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();            
        }        

        protected override void OnAppearing()
        {
            LoadGroups();            

            base.OnAppearing();
        }

        private void FAB_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Pages.AddGroupForm(new Group()));
        }

        private void groupList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Group tappedGroup = (Group)((ListView)sender).SelectedItem;
            Navigation.PushAsync(new Pages.GroupPage(tappedGroup));
        }

        public void OnEdit(object sender, EventArgs e)
        {
           //Navigation.PushAsync(new EditGroups());

            Group itemToEdit = ((sender as MenuItem).BindingContext as Group);
            Navigation.PushModalAsync(new EditGroupFrom(itemToEdit));
        }

        public void OnDelete(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new DeleteGroup());
            Group itemToDelete = ((sender as MenuItem).BindingContext as Group);
            DeleteGroup(itemToDelete);
        }
        public async void LoadGroups()
        {
            await CreateTables();
            var groups = await _connection.Table<Group>().OrderBy(g => g.Name)
                .ToListAsync();
            _groups = new ObservableCollection<Group>(groups);
            groupList.ItemsSource = _groups;
        }

        public async void DeleteGroup(Group group)
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

    }
}
