using DnsClient;
using Microsoft.VisualBasic.Logging;
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
                comboBox2.Items.Add(item[1]);
                if (comboBox1.FindString(item[2].ToString()) == -1)
                    comboBox1.Items.Add(item[2]); // Номер кабинета
            }


            // Добавление времени приёмов
            MongoClient test = new MongoClient("mongodb://localhost:27017");
            var db_1 = test.GetDatabase("Psixov_net");
            var day = db_1.GetCollection<BsonDocument>("TimePriemov");
            var day_list = day.Find(Builders<BsonDocument>.Filter.Empty).ToList();



            for (int i = 0; i <= 27; i++)
            {
                foreach (var item in day_list)
                {
                    if (item[2][i][1] == true) // Проверка на занятость времени
                    {
                        comboBox3.Items.Add(item[2][i][0]); // Добавление времени
                        if (comboBox4.FindString(item[1].ToString()) == -1)
                        {
                            comboBox4.Items.Add(item[1] + ".12.2022");
                        }
                    }

                }
            }
        }


        // Настройка всплывающих подсказок для ComboBox-ов
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

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("Psixov_net");
            var doctors = db.GetCollection<BsonDocument>("doctor");
            var doctors_list = doctors.Find(Builders<BsonDocument>.Filter.Empty).ToList();
            foreach (var item in doctors_list)
                if (comboBox1.Text == item[2])
                    comboBox2.Items.Add(item[1]);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
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


        private void comboBox3_MouseHover(object sender, EventArgs e)
        {
            label3.Visible = true;
        }

        private void comboBox3_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            label3.Visible = false;
        }
        //


        // Настройка пассивного текстка в textbox-сах
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
            textBox1.Enter -= textBox1_Enter;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Фамилия:";
                textBox1.ForeColor = Color.Gray;
                textBox1.Enter += textBox1_Enter;
            }
        }



        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            label5.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            l.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_MouseHover(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void comboBox4_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        //
    }
}


// TODO: Изменить при создании врача поля с временем работы на ФИО (Хз)
// TODO: Фикс бага для combobox ввода кабинета и врача (label не прозрачен)
// TODO: Добавить привязку combobox с временем к combobox даты
// TODO: Добавить остальные дни в бд + проверять при добавление в combobox дату на (не меньше текущей)
// TODO: Реализовать запись в бд (коллекция talon)
// TODO: Меню выдача талона