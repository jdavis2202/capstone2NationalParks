using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace CLI
{
    /// <summary>
    /// The top-level menu in our application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        // You may want to store some private variables here.  YOu may want those passed in 
        // in the constructor of this menu
        private ParkSqlDAO parkDao;
        private CampgroundSqlDAO campgroundDao;
        private SiteSqlDAO siteDao;
        private ReservationSqlDAO reservationDao;
        IList<Park> parks;

        /// <summary>
        /// Constructor adds items to the top-level menu. You will likely have parameters  passed in
        /// here...
        /// </summary>
        public MainMenu(ParkSqlDAO park, CampgroundSqlDAO campground, SiteSqlDAO site, ReservationSqlDAO reservation) : base("Main Menu")
        {
            this.parkDao = park;
            this.campgroundDao = campground;
            this.siteDao = site;
            this.reservationDao = reservation;
            parks = this.parkDao.GetParks();
        }

        protected override void SetMenuOptions()
        {
            // A Sample menu.  Build the dictionary here
            foreach (Park park in parks)
            {
                this.menuOptions.Add(park.ParkId.ToString(), park.Name);
            }


            this.menuOptions.Add("Q", "Quit program");
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            int parkId = int.Parse(choice);
            Park park = parkDao.GetPark(parkId);

            if (park.ParkId == parkId)
            {
                ParkMenu parkMenu = new ParkMenu(park, parkDao, campgroundDao, siteDao, reservationDao);
                parkMenu.Run();
                
            }
            else
            {
                Console.WriteLine("That park is not in our database, please try again.");
                this.ExecuteSelection(choice);
                return false;
            }

            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }


        private void PrintHeader()
        {
            SetColor(ConsoleColor.White);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Main Menu"));
            ResetColor();
        }
    }
}
