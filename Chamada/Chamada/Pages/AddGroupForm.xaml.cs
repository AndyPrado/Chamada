using Chamada.Database;
using Chamada.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Chamada.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddGroupForm : ContentPage
	{
        SQLiteAsyncConnection _connection;
        public Group _group = new Group();
        public string _frequency;

		public AddGroupForm (Group group)
		{
			InitializeComponent ();

            _connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            _group = group;
		}

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            _frequency = Frequency();

            var startTime = StartTime.Time;
            var finishTime = FinishTime.Time;            

            _group.Name = NameEntry.Text;
            _group.Frequency = _frequency;
            _group.StartTime = string.Format("{0:00}:{1:00}", startTime.Hours, startTime.Minutes);
            _group.FinishTime = string.Format("{0:00}:{1:00}", finishTime.Hours, finishTime.Minutes);            

            await _connection.InsertAsync(_group);
            
            await Navigation.PopModalAsync();
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