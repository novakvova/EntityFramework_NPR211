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
            Application.Run(new MainForm());
        }
    }
}