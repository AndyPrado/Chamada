﻿using Chamada.Database;
using Chamada.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Chamada
{
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public static ObservableCollection<Group> _groups;       

        public MainPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDB>().GetConnection();
                        
        }
       
        protected override async void OnAppearing()
        {            
            await _connection.CreateTableAsync<Group>();
            var groups = await _connection.Table<Group>().ToListAsync();
            _groups = new ObservableCollection<Group>(groups);
            groupList.ItemsSource = _groups;

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
    }
}
