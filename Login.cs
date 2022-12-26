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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox1.BackColor = Color.FromArgb(245, 245, 245);
            textBox2.BackColor = Color.FromArgb(245, 245, 245);
            button1.BackColor = Color.FromArgb(245, 245, 245);

            label1.BackColor = Color.FromArgb(169, 198, 213);

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


            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.Show();
            r.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }


        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Логин:";
                textBox1.ForeColor = Color.Gray;
                textBox1.Enter += textBox1_Enter_1;
            }
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
            textBox1.Enter -= textBox1_Enter_1;
        }

        private void textBox2_Enter_1(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
            textBox2.Enter -= textBox2_Enter_1;
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox2_Leave_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Пароль:";
                textBox2.ForeColor = Color.Gray;
                textBox2.Enter += textBox2_Enter_1;
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

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
                // получаем из бд коллекцию users 
                var logins = db.GetCollection<BsonDocument>("login");
                try
                {
                    var filter = new BsonDocument { { "username", textBox1.Text } };
                    IFindFluent<BsonDocument, BsonDocument> login = logins.Find(filter);
                    string username = login.First().GetValue("username").AsString;
                    int password = login.First().GetValue("password").ToInt32();
                    bool isAdmin = login.First().GetValue("isAdmin").ToBoolean();
                    if (password != int.Parse(textBox2.Text))
                    {
                        MessageBox.Show("Введен неверный пароль!", "Psihov.net");
                        return;
                    }
                    MessageBox.Show("Вы успешно вошли!", "Psihov.net");
                    if (isAdmin == true)
                    {
                        AdminMenu1 a = new AdminMenu1();
                        a.Show();
                        a.Location = new Point(this.Location.X, this.Location.Y);
                        this.Hide();
                    }
                    else
                    {
                        DoctorMainMenu1 c = new DoctorMainMenu1();
                        c.Show();
                        c.Location = new Point(this.Location.X, this.Location.Y);
                        this.Hide();
                        DoctorName.Name = username;
                    }
                }
                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("Поля не могут быть пустыми!", "Psihov.net");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
