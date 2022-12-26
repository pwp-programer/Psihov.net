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

namespace Psihov.net
{
    public partial class DoctorMainMenu1 : Form
    {
        public DoctorMainMenu1()
        {
            InitializeComponent();

            // Определиние цветов
            textBox1.BackColor = Color.FromArgb(229, 245, 254);
            textBox2.BackColor = Color.FromArgb(229, 245, 254);
            textBox3.BackColor = Color.FromArgb(229, 245, 254);
            textBox4.BackColor = Color.FromArgb(229, 245, 254);
            textBox5.BackColor = Color.FromArgb(229, 245, 254);
            button1.BackColor = Color.FromArgb(229, 245, 254);
        }

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

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
            textBox2.Enter -= textBox2_Enter;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Имя:";
                textBox2.ForeColor = Color.Gray;
                textBox2.Enter += textBox2_Enter;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
            textBox3.Enter -= textBox3_Enter;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Отчество:";
                textBox3.ForeColor = Color.Gray;
                textBox3.Enter += textBox3_Enter;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
            textBox4.Enter -= textBox4_Enter;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Диагноз:";
                textBox4.ForeColor = Color.Gray;
                textBox4.Enter += textBox4_Enter;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox5.ForeColor = Color.Black;
            textBox5.Enter -= textBox5_Enter;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Дата приёма:";
                textBox5.ForeColor = Color.Gray;
                textBox5.Enter += textBox5_Enter;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("Psixov_net");
            var logins = db.GetCollection<BsonDocument>("ClientData");
            try
            {
                var filter = new BsonDocument { { "Surname", textBox1.Text } };
                IFindFluent<BsonDocument, BsonDocument> data = logins.Find(filter);
                string Surname = data.First().GetValue("Surname").AsString;
                string Date = data.First().GetValue("Date").AsString;
                string Diagnosis = data.First().GetValue("Diagnosis").AsString;
                if (textBox1.Text == Surname && Date == textBox5.Text && Diagnosis == textBox4.Text)
                {
                    MessageBox.Show("Такая запись уже существует!", "Psihov.net");
                }
                else if (textBox1.Text != "Фамилия:" && textBox5.Text != "Дата приёма:" && textBox4.Text != "Диагноз")
                {
                    NewClientData data1 = new NewClientData(textBox1.Text, textBox2.Text, textBox3.Text,
                           textBox4.Text, textBox5.Text, DoctorName.Name);
                    logins.InsertOne(data1.ToBsonDocument());
                    MessageBox.Show("Запись успешно создана!", "Psihov.net");
                }
                else
                {
                    MessageBox.Show("Поля не могут быть пустыми", "Psihov.net");
                }
            }
            catch (System.InvalidOperationException)
            {
                try
                {
                    if (textBox1.Text != "Фамилия:" && textBox5.Text != "Дата приёма:" && textBox4.Text != "Диагноз")
                    {
                        NewClientData data = new NewClientData(textBox1.Text, textBox2.Text, textBox3.Text,
                            textBox4.Text, textBox5.Text, DoctorName.Name);
                        logins.InsertOne(data.ToBsonDocument());
                        MessageBox.Show("Запись успешно создана!", "Psihov.net");
                    }
                    else
                    {
                        MessageBox.Show("Поля не могут быть пустыми", "Psihov.net");
                    }
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Введите данные", "Psihov.net");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            l.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            label3.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DoctorMainMenu2 d = new DoctorMainMenu2();
            d.Show();
            d.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DoctorMainMenu3 d = new DoctorMainMenu3();
            d.Show();
            d.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }
    }
}