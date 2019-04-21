using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNote.Model
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhotoPath { get; set; }
        public string Details { get; set; }
        public byte[] Photo { get; set; }
    }
}
