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
    public partial class DoctorMainMenu3 : Form
    {
        public DoctorMainMenu3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();


            string value = dateTimePicker1.Text;
            string[] array = value.Split(".");
            string[] array2 = dateTimePicker2.Text.Split(".");

            int[] DataArray =
            {
                int.Parse(array[0]),
                int.Parse(array[1]),
                int.Parse(array[2]),
                int.Parse(array2[0]),
                int.Parse(array2[1]),
                int.Parse(array2[2])
            };

            for (int i = 0; i < DataArray.Length; i++)
            {
                if (i != 2 && i != 5)
                {
                    string buffer = DataArray[i].ToString();
                    if (buffer[0] == '0')
                    {
                        DataArray[i] = buffer[1];
                    }
                }
            }

            //label1.Text = $"День: {DataArray[0]} Месяц: {DataArray[1]} Год: {DataArray[2]}";
            //label2.Text = $"День: {DataArray[3]} Месяц: {DataArray[4]} Год: {DataArray[5]}";

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("Psixov_net");
            var data = db.GetCollection<BsonDocument>("ClientData");
            var Date = data.Find(Builders<BsonDocument>.Filter.Empty).ToList();


            int[] ClientDataArray = new int[Date.Count];

            foreach (var item in Date)
            {
                // Удаление 0 в дате
                string v = item[5].ToString();
                string[] b = v.Split(".");
                int d;
                int m;
                int y;

                if (b[0][0] == '0')
                {
                    d = int.Parse(b[0][1].ToString());
                }
                else
                {
                    d = int.Parse(b[0].ToString());
                }


                if (b[1][0] == '0')
                {
                    m = int.Parse(b[1][1].ToString());
                }
                else
                {
                    m = int.Parse(b[1].ToString());
                }
                y = int.Parse(b[2].ToString());

                // Проверка на вход в диапазон
                if (DataArray[0] <= DataArray[3] && DataArray[1] <= DataArray[4] && DataArray[2] <= DataArray[5])
                {
                    if (DataArray[0] <= d && DataArray[1] <= m && DataArray[2] <= y
                    && DataArray[3] >= d && DataArray[4] >= m && DataArray[5] >= y)
                    {
                        //label3.Text += item[5].ToString() + "   ";
                        listBox1.Items.Add($"{item[2].ToString()} {item[1].ToString()}" +
                            $" {item[3].ToString()}");
                    }
                }
                else
                {
                    MessageBox.Show("Начало диапазона не может быть больше конца", "Psihov.net");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string value = listBox1.Text;

            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");
                var db = client.GetDatabase("Psixov_net");
                var data = db.GetCollection<BsonDocument>("ClientData");
                var Date = data.Find(Builders<BsonDocument>.Filter.Empty).ToList();
                foreach (var item in Date)
                {
                    value = value.ToLower().Replace(" ", "");

                    string Buf2 = item[2].ToString() + item[1].ToString() + item[3].ToString();
                    Buf2 = Buf2.Replace(" ", "").ToLower();

                    if (value == Buf2)
                    {
                        MessageBox.Show($"Фамилия: {item[2]}\nИмя: {item[1]}" +
                            $"\nОтчество: {item[3]}\nДиагноз: {item[4]}" +
                            $"\nДата посещения: {item[5]}\nЛечащий врач: {item[6]}", "Psihov.net");
                    }
                }
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Данный диагноз не найден. Проверьте правильность написания!", "Psihov.net");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            l.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DoctorMainMenu1 l = new DoctorMainMenu1();
            l.Show();
            l.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DoctorMainMenu2 l = new DoctorMainMenu2();
            l.Show();
            l.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void dateTimePicker1_MouseHover(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void dateTimePicker1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void dateTimePicker2_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
        }

        private void dateTimePicker2_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            label5.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;
        }
    }
}
