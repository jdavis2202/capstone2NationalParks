using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundSqlDAOTests
    {
        private string connectionString = @"Server=.\SQLEXPRESS; Database=npcampground; Trusted_Connection=True;";
        private TransactionScope transaction;

        // Hold ids
        private int acadia;

        [TestInitialize]
        public void SetupDB()
        {
            transaction = new TransactionScope();

            string sqlScript = File.ReadAllText("Setup.sql");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlScript, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    acadia = Convert.ToInt32(rdr["Acadia"]);
                }
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            // Roll back
            transaction.Dispose();
        }

        [TestMethod]
        public void CheckCampgroundCount()
        {
            // Assign
            CampgroundSqlDAO dao = new CampgroundSqlDAO(connectionString);

            // Act
            IList<Campground> list = dao.GetCampgroundById(acadia);

            // Assert
            Assert.AreEqual(1, list.Count);
        }
        [TestMethod]
        public void CheckCampgroundMonths()
        {
            // Assign
            CampgroundSqlDAO dao = new CampgroundSqlDAO(connectionString);

            // Act
            IList<Campground> list = dao.GetCampgroundById(acadia);

            // Assert
            Assert.AreEqual("January", list[0].OpenMonth);
        }
    }
}
