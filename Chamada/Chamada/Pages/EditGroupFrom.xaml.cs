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
    public partial class EditGroupFrom : ContentPage
    {
        Group _group;
        SQLiteAsyncConnection _connection;

        public EditGroupFrom(Group group)
        {
            _group = group;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            InitializeComponent();

            Title = "Edit " + group.Name;

            NameEntry.Text = group.Name;
            StartTime.Time = new TimeSpan(group.StartTime.Hour, group.StartTime.Minute, group.StartTime.Second);
            FinishTime.Time = new TimeSpan(group.FinishTime.Hour, group.FinishTime.Minute, group.FinishTime.Second);

        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var startTime = StartTime.Time;
            var finishTime = FinishTime.Time;
            var frequency = Frequency();

            _group.Name = NameEntry.Text;
            _group.StartTime = new DateTime(startTime.Ticks);
            _group.FinishTime = new DateTime(finishTime.Ticks);
            _group.Frequency = frequency;

            //open the table and update it.
            await _connection.CreateTableAsync<Group>();
            await _connection.UpdateAsync(_group);

            var answer = await DisplayAlert("Confirmation", "Are you sure you want to edit this group?", "Yes", "No" );

            if (answer)
            {
                await Navigation.PopModalAsync();
            }
            
        }

        private string Frequency()
        {
            var freq = "";

            if (Monday.Checked)
            {
                freq += "Mon ";
            }
            if (Tuesday.Checked)
            {
                freq += "Tue ";
            }
            if (Wednesday.Checked)
            {
                freq += "Wed ";
            }
            if (Thursday.Checked)
            {
                freq += "Thu ";
            }
            if (Friday.Checked)
            {
                freq += "Fri ";
            }
            if (Saturday.Checked)
            {
                freq += "Sat ";
            }

            return freq;
        }
    }
}