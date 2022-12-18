using System.Drawing.Drawing2D;

namespace Psihov.net
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(197, 232, 252); // this should be pink-ish
            timer1.Start();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //D9D9D9
            this.BackColor = Color.FromArgb(217, 217, 217);
        }

        // Скрытие текста на экране загрузки
        public void Loading_Screen()
        {
            Register G = new Register();
            G.Show();
            G.Location = new Point(this.Location.X, this.Location.Y);
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Скрытие текста на экране загрузки
            Loading_Screen();
            timer1.Stop();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}