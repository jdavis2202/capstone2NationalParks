using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteSqlDAO
    {
        private string connectionString;

        public SiteSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public IList<Site> GetSiteId(int siteId)
        {
            IList<Site> sites = new List<Site>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();


                const string sql = "SELECT * FROM SITE WHERE campground_id = @campgroundId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@campgroundId", siteId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sites.Add(ParseRow(rdr));
                }
            }

            return sites;
        }

        private Site ParseRow(SqlDataReader rdr)
        {
            Site sites = new Site();

            sites.CampgroundId = Convert.ToInt32(rdr["campground_id"]);
            sites.SiteId = Convert.ToInt32(rdr["site_id"]);
            sites.SiteNumber = Convert.ToInt32(rdr["site_number"]);
            sites.MaxOccupancy = Convert.ToInt32(rdr["max_occupancy"]);
            sites.IsAccessible = Convert.ToBoolean(rdr["accessible"]);
            sites.MaxRvLength = Convert.ToInt32(rdr["max_rv_length"]);
            sites.HasUtilites = Convert.ToBoolean(rdr["utilities"]);


            return sites;
        }
    }
}
