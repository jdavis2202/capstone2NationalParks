using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenInt { get; set; }
        public int CloseInt { get; set; }
        public decimal DailyFee { get; set; }
        public IList<Site> Sites { get; set; }
        public string OpenMonth
        {
            get
            {
                return NumToMonth[this.OpenInt];
            }
        }
        public string CloseMonth
        {
            get
            {
                return NumToMonth[this.CloseInt];
            }
        }

        private Dictionary<int, string> NumToMonth = new Dictionary<int, string>()
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "Novemeber" },
            { 12, "December" }
        };


        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
