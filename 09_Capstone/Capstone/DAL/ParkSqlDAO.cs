using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkSqlDAO
    {
        private string connectionString;

        public ParkSqlDAO(string databaseconnectionString)
        {
            this.connectionString = databaseconnectionString;
        }

        public IList<Park> GetParks()
        {
            IList<Park> parks = new List<Park>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                const string sql = "SELECT * FROM park ORDER BY name ASC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parks.Add(ParseRow(rdr));
                }
            }

            return parks;
        }

        internal Park GetPark(int parkId)
        {
            Park park = new Park();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                const string sql = "SELECT * FROM park WHERE park_id = @parkId;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parkId", parkId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    park = ParseRow(rdr);
                }
            }

            return park;
        }

        private Park ParseRow(SqlDataReader rdr)
        {
            Park park = new Park();

            park.ParkId = Convert.ToInt32(rdr["park_id"]);
            park.Name = Convert.ToString(rdr["name"]);
            park.Location = Convert.ToString(rdr["location"]);
            park.EstablishedDate = Convert.ToDateTime(rdr["establish_date"]);
            park.Area = Convert.ToInt32(rdr["area"]);
            park.Visitors = Convert.ToInt32(rdr["visitors"]);
            park.Description = Convert.ToString(rdr["description"]);

            return park;
        }
    }
}
