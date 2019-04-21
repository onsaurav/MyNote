using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNote.Model
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string PhotoPath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte[] Photo { get; set; }
    }
}
