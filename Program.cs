using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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


    class User
    {
        public string username { get; set; }
        public User(string name)
        {
            this.username = username;
        }
    }


    class New_User : User
    {
        public string username { get; }
        public int password { get; set; }
        public bool isAdmin { get; set; }
        public bool isDoctor { get; set; }



        public New_User(string username, int password, bool isAdmin, bool isDoctor) : base(username)
        {
            this.password = password;
            this.username = username;
            this.isAdmin = isAdmin;
            this.isDoctor = isDoctor;
        }
    }


    class New_Doctor : User
    {
        public string kabina { get; set; }
        public int StartWorkingTime { get; set; }
        public int EndWorkingTime { get; set; }
        string username { get; set; }


        public New_Doctor(string username, string kabina, int StartWorkingTime, int EndWorkingTime) : base(username)
        {
            this.kabina = kabina;
            this.StartWorkingTime = StartWorkingTime;
            this.EndWorkingTime = EndWorkingTime;
            this.username = username;
        }
    }
}