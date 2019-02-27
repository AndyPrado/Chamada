using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Chamada.Models
{
    [Table("Registers")]
    public class Register
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Classwork { get; set; }
        public string ECampus { get; set; }
        public string Homework { get; set; }
        public string AbsentStudents { get; set; }
        public DateTime Day { get; set; }
    }


}
