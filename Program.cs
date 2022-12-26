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
        public string Name { get; set; }
        public User(string name)
        {
            this.Name = Name;
        }
    }


    class New_User : User
    {
        public int password { get; set; }
        public bool isAdmin { get; set; }



        public New_User(string name, int password, bool isAdmin) : base(name)
        {
            this.Name = name;
            this.password = password;
            this.isAdmin = isAdmin;
        }
    }


    class NewClientData : User
    {
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Diagnosis { get; set; }
        public string Date { get; set; }
        public string DoctorName { get; set; }

        public NewClientData(string surname, string name, string middleName, string diagnosis, string date, string doctorName)
            : base(name)
        {
            Surname = surname;
            this.Name = name;
            MiddleName = middleName;
            Diagnosis = diagnosis;
            Date = date;
            DoctorName = doctorName;
        }
    }


    static class DoctorName
    {
        public static string Name;
    }
}