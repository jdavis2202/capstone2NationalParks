using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int CampgroundId { get; set; }
        public int SiteId { get; set; }
        public bool IsBooked
        {
            get
            {
                foreach (Reservation reservation in this.Reservations)
                {
                    if (reservation.StartDate <= UserStartTime && reservation.EndDate >= UserStartTime)
                    {
                        return true;
                    }

                    if (reservation.StartDate <= UserEndTime && reservation.EndDate >= UserEndTime)
                    {
                        return true;
                    }

                    if (reservation.EndDate <= UserStartTime && reservation.EndDate >= UserEndTime)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public bool IsAccessible { get; set; }
        public int MaxRvLength { get; set; }
        public bool HasUtilites { get; set; }
        public IList<Reservation> Reservations { get; set; }
        public DateTime  UserStartTime { get; set; }
        public DateTime UserEndTime { get; set; }
        public decimal TotalFee { get; set; }

        public override string ToString()
        {
            return $"{this.SiteNumber}";
        }
    }
}
