using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationSqlDAO
    {
        int AddReservation(int siteId, string name, DateTime fromDate, DateTime toDate);

        IList<Reservation> getAllReservations(int siteId);
    }
}
