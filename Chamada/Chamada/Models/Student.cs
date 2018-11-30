using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chamada.Models
{
    [Table("Students")]
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }        
    }
}
