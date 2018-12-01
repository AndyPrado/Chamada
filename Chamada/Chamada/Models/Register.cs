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
        public string StudentId { get; set; }
        public DateTime Day { get; set; }            
    }
}
