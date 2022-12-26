using DnsClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Psihov.net
{
    public partial class DoctorMainMenu2 : Form
    {
        public DoctorMainMenu2()
        {
            InitializeComponent();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DoctorMainMenu1 d = new DoctorMainMenu1();
            d.Show();
            d.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                MongoClient ClientData = new MongoClient("mongodb://localhost:27017");
                var db = ClientData.GetDatabase("Psixov_net");
                var filter = new BsonDocument { { "Diagnosis", textBox1.Text } };
                var ClientDataList = db.GetCollection<BsonDocument>("ClientData");
                IFindFluent<BsonDocument, BsonDocument> CDl = ClientDataList.Find(filter);
                var list = ClientDataList.Find(filter).ToList();
                foreach (var item in list)
                {
                    string value = item[4].ToString().Replace(" ", "").ToLower();
                    string buf = textBox1.Text.ToLower().Replace(" ", "");
                    if (value == buf &&
                        (item[2].ToString() + item[1].ToString()
                        + item[3].ToString() != textBox1.Text.Replace(" ", "")))
                    {
                        listBox1.Items.Add(item[2] + " " + item[1] + " " + item[3]);
                    }
                    else
                    {
                        MessageBox.Show("Данный диагноз не найден. Проверьте правильность написания!", "Psihov.net");
                    }
                }
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Данный диагноз не найден. Проверьте правильность написания!",
                    "Psihov.net");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value = listBox1.Text;

            try
            {
                MongoClient ClientData = new MongoClient("mongodb://localhost:27017");
                var db = ClientData.GetDatabase("Psixov_net");
                var filter = new BsonDocument { { "Diagnosis", textBox1.Text } };
                var ClientDataList = db.GetCollection<BsonDocument>("ClientData");
                IFindFluent<BsonDocument, BsonDocument> CDl = ClientDataList.Find(filter);
                var list = ClientDataList.Find(filter).ToList();
                foreach (var item in list)
                {
                    value = value.ToLower().Replace(" ", "");

                    string Buf2 = item[2].ToString() + item[1].ToString() + item[3].ToString();
                    Buf2 = Buf2.Replace(" ", "").ToLower();

                    if (value == Buf2)
                    {
                        MessageBox.Show($"Фамилия: {item[2]}\nИмя: {item[1]}" +
                            $"\nОтчество: {item[3]}\nДиагноз: {item[4]}" +
                            $"\nДата посещения: {item[5]}\nЛечащий врач: {item[6]}",
                            "Psihov.net");
                    }
                }
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Данный диагноз не найден. Проверьте правильность написания!", "Psihov.net");
            }

        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DoctorMainMenu3 d = new DoctorMainMenu3();
            d.Show();
            d.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }
    }
}
