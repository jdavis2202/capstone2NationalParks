using Capstone.DAL;
using Capstone.Models;
using Capstone.Views;
using System;
using System.Collections.Generic;

namespace CLI
{
    /// <summary>
    /// A sub-menu 
    /// </summary>
    public class ParkMenu : CLIMenu
    {
        // Store any private variables here....
        private ParkSqlDAO parkDao;
        private CampgroundSqlDAO campgroundDao;
        private SiteSqlDAO siteDao;
        private ReservationSqlDAO reservationDao;
        private Park park;
        private CampgroundMenu campgroundMenu;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public ParkMenu(Park park, ParkSqlDAO parkDao, CampgroundSqlDAO campgroundDao, SiteSqlDAO siteDao, ReservationSqlDAO reservationDao) :
            base("Park Menu")
        {
            this.parkDao = parkDao;
            this.campgroundDao = campgroundDao;
            this.siteDao = siteDao;
            this.reservationDao = reservationDao;
            this.park = park;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "View Campgrounds");
            this.menuOptions.Add("B", "Return to Previous Screen");
            this.quitKey = "B";
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1": // Do whatever option 1 is
                    park.Campgrounds = this.campgroundDao.GetCampgroundById(this.park.ParkId);
                    //foreach (Campground campground in campgrounds)
                    //{
                    //    park.Campgrounds.Add(campground);
                    //}
                    this.campgroundMenu = new CampgroundMenu(park, campgroundDao, siteDao, reservationDao);
                    campgroundMenu.Run();

                    Pause("");
                    return true;
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Green);
            Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");
            ResetColor();
        }

        private void PrintHeader()
        {
            SetColor(ConsoleColor.Blue);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render(this.park.Name));
            Console.WriteLine($"Location: {this.park.Location}");
            Console.WriteLine($"Established: {this.park.EstablishedDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"Area: {String.Format("{0:n0}",this.park.Area)}");
            Console.WriteLine($"Annual Visitors: {String.Format("{0:n0}", this.park.Visitors)}");
            Console.WriteLine();
            Console.WriteLine(this.park.Description);
            ResetColor();
        }

    }
}
