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
    public partial class AdminMenu2 : Form
    {
        public AdminMenu2()
        {
            InitializeComponent();


            label1.BackColor = Color.FromArgb(220, 237, 245);


            textBox1.BackColor = Color.FromArgb(245, 245, 245);
            textBox2.BackColor = Color.FromArgb(245, 245, 245);


            button1.BackColor = Color.FromArgb(245, 245, 245);

            label2.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AdminMenu1 a = new AdminMenu1();
            a.Show();
            a.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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
            if ((textBox1.Text != string.Empty) && (textBox2.Text != string.Empty))
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
                    bool IsAdmin = login.First().GetValue("isAdmin").ToBoolean();

                    try
                    {
                        if (textBox1.Text != "Логин:" || textBox2.Text != "Пароль:" || textBox1.Text != String.Empty || textBox2.Text != String.Empty)
                        {
                            if (textBox1.Text == username && int.Parse(textBox2.Text) == password
                                && IsAdmin == false)
                            {
                                logins.DeleteOne(login.First());
                                MessageBox.Show("Учётная запись удалена", "Psihov.net");
                            }
                            else
                            {
                                MessageBox.Show("Вы не можете удалить эту учётную запись", "Psihov.net");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поля не могут быть пустыми", "Psihov.net");
                        }
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Ошибка. Попробуйте снова", "Psihov.net");
                    }
                }
                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("такой учётной записи нет", "psihov.net");
                }
            }
        }

        private void AdminMenu2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Register r = new Register();
            r.Show();
            r.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(171, 205, 221);
            label2.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


// TODO: Проверка на админа при удалении, редактирование формы создания уч записей врачей.
// TODO: Добавить формы пациентов.