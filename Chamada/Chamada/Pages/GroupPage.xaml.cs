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
	public partial class GroupPage : TabbedPage
	{
        public static Group _group;
        public SQLiteAsyncConnection _connection;

		public GroupPage (Group group)
		{
			InitializeComponent ();
            _group = group;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            Children.Add(new RegisterPage(_group));
            Children.Add(new StudentsPage(_group));
            

		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = _group.Name;
        }
    }
}