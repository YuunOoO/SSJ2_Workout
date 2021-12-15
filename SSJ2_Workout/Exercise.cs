using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SSJ2_Workout
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public bool Did { get; set; }
    }
}
