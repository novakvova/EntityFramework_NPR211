using Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext())
            {
                applicationDbContext.Database.Migrate();
            }
            //ImageWorker.ImageSaveUrl("https://montevista.greatheartsamerica.org/wp-content/uploads/sites/2/2016/11/default-placeholder.png", "default");
            Application.Run(new MainForm());
        }
    }
}