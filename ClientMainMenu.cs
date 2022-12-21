using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Psihov.net
{
    public partial class ClientMainMenu : Form
    {
        public ClientMainMenu()
        {
            InitializeComponent();

            //  Задание цветов
            comboBox1.BackColor = Color.FromArgb(229, 245, 254);
            comboBox2.BackColor = Color.FromArgb(229, 245, 254);
            comboBox3.BackColor = Color.FromArgb(229, 245, 254);


            textBox1.BackColor = Color.FromArgb(229, 245, 254);
            textBox2.BackColor = Color.FromArgb(229, 245, 254);
            textBox3.BackColor = Color.FromArgb(229, 245, 254);


            button1.BackColor = Color.FromArgb(229, 245, 254);


            label1.BackColor = Color.FromArgb(229, 245, 254);
            label2.BackColor = Color.FromArgb(229, 245, 254);
            //


            // Добавление значений в comboBox 
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("Psixov_net");
            var doctors = db.GetCollection<BsonDocument>("doctor");
            var doctors_list = doctors.Find(Builders<BsonDocument>.Filter.Empty).ToList();


            foreach (var item in doctors_list)
            {
                comboBox1.Items.Add(item[2]); // Номер кабинета
                comboBox2.Items.Add(item[1]); // Имя врача

            }
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void comboBox2_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
        }

        private void comboBox2_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            label2.Visible = false;
        }
    }
}
