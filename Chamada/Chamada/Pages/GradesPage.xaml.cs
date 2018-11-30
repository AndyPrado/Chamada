using Chamada.Models;
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
	public partial class GradesPage : ContentPage
	{
        Group _group;
		public GradesPage (Group group)
		{
			InitializeComponent ();

            _group = group;
            
		}
	}
}