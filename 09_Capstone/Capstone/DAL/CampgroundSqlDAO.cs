using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundSqlDAO
    {
        private string connectionString;

        public CampgroundSqlDAO(string databaseconnectionString)
        {
            this.connectionString = databaseconnectionString;
        }

        public IList<Campground> GetCampgroundById(int parkId)
        {
            IList<Campground> campgrounds = new List<Campground>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                const string sql = "SELECT * FROM campground WHERE park_id = @parkId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parkId", parkId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    campgrounds.Add(ParseRow(rdr));
                }
            }

            return campgrounds;
        }

        private Campground ParseRow(SqlDataReader rdr)
        {
            Campground campground = new Campground();

            campground.CampgroundId = Convert.ToInt32(rdr["campground_id"]);
            campground.ParkId = Convert.ToInt32(rdr["park_id"]);
            campground.Name = Convert.ToString(rdr["name"]);
            campground.OpenInt = Convert.ToInt32(rdr["open_from_mm"]);
            campground.CloseInt = Convert.ToInt32(rdr["open_to_mm"]);
            campground.DailyFee = Convert.ToDecimal(rdr["daily_fee"]);

            return campground;
        }
    }
}
