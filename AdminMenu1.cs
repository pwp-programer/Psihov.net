using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Psihov.net
{
    public partial class AdminMenu1 : Form
    {
        public AdminMenu1()
        {
            InitializeComponent();

            textBox1.BackColor = Color.FromArgb(245, 245, 245);
            textBox2.BackColor = Color.FromArgb(245, 245, 245);
            textBox3.BackColor = Color.FromArgb(245, 245, 245);
            textBox4.BackColor = Color.FromArgb(245, 245, 245);
            textBox5.BackColor = Color.FromArgb(245, 245, 245);


            label2.BackColor = Color.FromArgb(220, 237, 245);


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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
            textBox1.Enter -= textBox1_Enter;
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


        private void button1_Click(object sender, EventArgs e)
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
                var doctors = db.GetCollection<BsonDocument>("doctor");
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
                    // Добавление в коллекцию login
                    New_User user = new New_User(textBox1.Text, int.Parse(textBox2.Text), false);
                    logins.InsertOne(user.ToBsonDocument());

                    //  Оповещение об успешной регистрации
                    MessageBox.Show("Вы успешно зарегистрированны!", "Psihov.net");
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.Show();
            r.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            label1.BackColor = Color.FromArgb(171, 205, 221);
            label1.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminMenu2 a = new AdminMenu2();
            a.Show();
            a.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
                textBox3.Text = "Номер кабинета:";
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
                textBox4.Text = "Начало рабочего дня:";
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
                textBox5.Text = "Конец рабочего дня:";
                textBox5.ForeColor = Color.Gray;
                textBox5.Enter += textBox5_Enter;
            }
        }
    }
}
