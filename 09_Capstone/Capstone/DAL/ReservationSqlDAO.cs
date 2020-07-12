using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
   public class ReservationSqlDAO : IReservationSqlDAO
    {
        private string connectionString;

        public ReservationSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public int AddReservation(int siteId, string name, DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Reservation (site_id, name, from_date, to_date, create_date) VALUES (@siteId, @name, @fromDate, @toDate, @createDate); Select @@identity;", conn );
                cmd.Parameters.AddWithValue("@siteId", siteId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@createDate", DateTime.Now);

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                return id;
            }
            
        }
        public IList<Reservation> getAllReservations(int siteId)
        {
            IList<Reservation> reservations = new List<Reservation>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                const string sql = "Select * from reservation where site_id = @site_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@site_id", siteId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    reservations.Add(ParseRow(rdr));
                }
            }
            return reservations;

        }

        private Reservation ParseRow(SqlDataReader rdr)
        {
            Reservation reservation = new Reservation();

            reservation.SiteId = Convert.ToInt32(rdr["site_id"]);
            reservation.ReservationId = Convert.ToInt32(rdr["reservation_id"]);
            reservation.Name = Convert.ToString(rdr["name"]);
            reservation.StartDate = Convert.ToDateTime(rdr["from_date"]);
            reservation.EndDate = Convert.ToDateTime(rdr["to_date"]);
            reservation.CreateDate = Convert.ToDateTime(rdr["create_date"]);

            return reservation;
        }
    }
   
}
