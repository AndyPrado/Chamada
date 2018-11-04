using System;
using System.Collections.Generic;
using System.Text;

namespace Chamada.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime Time { get; set; }
        public List<string> DaysOfTheWeek { get; set; }
        public int NumberOfStudents { get; set; }
        public string FormattedWeekDays
        {
            get
            {
                string days = " ";
                foreach(string day in DaysOfTheWeek)
                {
                    days += day;
                }

                return days;
            }            
        }
    }
}
