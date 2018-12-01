using Chamada.Models;
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
	public partial class AddRegisterForm : ContentPage
	{
        Group _group;
        ObservableCollection<Student> _students;

		public AddRegisterForm (Group group, ObservableCollection<Student>students)
		{
			InitializeComponent();

            _group = group;
            _students = students;
		}

        protected override void OnAppearing()
        {
            lvStudentsRegister.ItemsSource = _students;
            base.OnAppearing();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {

        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}