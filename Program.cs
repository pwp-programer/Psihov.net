using System.Drawing.Drawing2D;

namespace Psihov.net
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
            Application.Run(new StartScreen());
        }
    }


    class New_User
    {
        public string username { get; set; }
        public int password { get; set; }
        public bool isAdmin { get; set; }
        public bool isDoctor { get; set; }



        public New_User(string username, int password, bool isAdmin, bool isDoctor)
        {
            this.password = password;
            this.username = username;
            this.isAdmin = isAdmin;
            this.isDoctor = isDoctor;
        }
    }
}