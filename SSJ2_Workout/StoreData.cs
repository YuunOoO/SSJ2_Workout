using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SSJ2_Workout
{
    public class StoreData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Day { get; set; }
        public double Total_burned { get; set; }
        public double Total_delivered { get; set; }
        public double Total_calories { get; set; }
        public int Total_steps { get; set; }
    }
}
