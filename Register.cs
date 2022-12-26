using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;


namespace Psihov.net
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(245, 245, 245);
            textBox2.BackColor = Color.FromArgb(245, 245, 245);


            label1.BackColor = Color.FromArgb(169, 198, 213);
            label2.BackColor = Color.FromArgb(181, 206, 220);


            button1.BackColor = Color.FromArgb(245, 245, 245);
            GraphicsPath gp = new GraphicsPath();
            Graphics g = CreateGraphics();
            // Создадим новый прямоугольник с размерами кнопки 
            Rectangle smallRectangle = button1.ClientRectangle;
            // уменьшим размеры прямоугольника 
            smallRectangle.Inflate(-3, -3);
            // создадим эллипс, используя полученные размеры 
            gp.AddEllipse(smallRectangle);
            button1.Region = new Region(gp);
            // рисуем окантовоку для круглой кнопки 
            g.DrawEllipse(new Pen(Color.Gray, 2),
            button1.Left + 1,
            button1.Top + 1,
            button1.Width - 3,
            button1.Height - 3);
            // освобождаем ресурсы 
            g.Dispose();
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
                textBox1.Text = "Логин:";
                textBox1.ForeColor = Color.Gray;
                textBox1.Enter += textBox1_Enter;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
            textBox2.Enter -= textBox2_Enter;
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Пароль:";
                textBox2.ForeColor = Color.Gray;
                textBox2.Enter += textBox2_Enter;
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login G = new Login();
            G.Show();
            G.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
                MessageBox.Show("Произошла ошибка! Введите логин!", "Psihov.net");
            else if (textBox2.Text == string.Empty)
                MessageBox.Show("Произошла ошибка! Введите пароль!", "Psihov.net");
            else if ((textBox1.Text != string.Empty) && (textBox2.Text != string.Empty))
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");
                var db = client.GetDatabase("Psixov_net");
                var logins = db.GetCollection<BsonDocument>("login");
                try
                {
                    var filter = new BsonDocument { { "username", textBox1.Text } };
                    IFindFluent<BsonDocument, BsonDocument> login = logins.Find(filter);
                    string username = login.First().GetValue("username").AsString;
                    int password = login.First().GetValue("password").ToInt32();
                    if (textBox1.Text == username)
                        MessageBox.Show("Такой пользователь существует!", "Psihov.net");
                }
                catch (System.InvalidOperationException)
                {
                    try
                    {
                        if (textBox1.Text != "Логин:" && textBox2.Text != "Пароль:")
                        {
                            New_User user = new New_User(textBox1.Text, int.Parse(textBox2.Text), false);
                            logins.InsertOne(user.ToBsonDocument());
                            MessageBox.Show("Вы успешно зарегистрированны!", "Psihov.net");
                            Login l = new Login();
                            l.Show();
                            l.Location = new Point(this.Location.X, this.Location.Y);
                            this.Hide();
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
        }
    }
}
