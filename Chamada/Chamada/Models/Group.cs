using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chamada.Models
{
    [Table("Groups")]
    public class Group
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Frequency { get; set; }        
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }        
    }
}
