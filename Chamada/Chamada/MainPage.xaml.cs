using Chamada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Chamada
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            List<string> days = new List<string>();
            days.Add("Monday");
            days.Add("Wednesday");

            groupList.ItemsSource = new List<Groups>()
            {
                new Groups(){ Name = "Turma 1", NumberOfDays = 0, Id = 1, Time = DateTime.Now, NumberOfStudents = 0,
                    DaysOfTheWeek = days },
                new Groups(){ Name = "Turma 2", NumberOfDays = 0, Id = 1, Time = DateTime.Now, NumberOfStudents = 0,
                    DaysOfTheWeek = days },
                new Groups(){ Name = "Turma 3", NumberOfDays = 0, Id = 1, Time = DateTime.Now, NumberOfStudents = 0,
                    DaysOfTheWeek = days }
            };
        }
    }
}
