using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spam.Properties;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Spam
{
    public partial class HK_Emu : Form
    {
        private int Interval;
        Point StartPoint;
        public HK_Emu()
        {
            InitializeComponent();
            this.MouseMove += Spam_MouseMove;
            this.MouseDown += Spam_MouseDown;
            this.Icon = Resources.ico;
            checkBox1.Checked = true;
            label1.Focus();
            label1.Select();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    timer1.Interval = Interval;
                    if (textBox1.Text == "") { timer1.Stop(); MessageBox.Show("Вы не указали сообщение."); }
                    else { SendKeys.Send(textBox1.Text); SendKeys.Send("{ENTER}"); }
                }
                if (checkBox1.Checked == false) 
                {
                    for (int i = 1; i < Convert.ToInt32(textBox2.Text); i++)
                    {
                        if (textBox1.Text == "") { timer1.Stop(); MessageBox.Show("Вы не указали сообщение."); }
                        else { SendKeys.Send(textBox1.Text); SendKeys.Send("{ENTER}"); }
                        Thread.Sleep(Interval);
                    }
                    timer1.Stop();
                }
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(Convert.ToInt32(textBox3.Text));
            if (textBox4.Text != "") { Interval = Convert.ToInt32(textBox4.Text); }
            else { Interval = 500; }
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Spam_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point point = new Point(e.X - StartPoint.X, e.Y - StartPoint.Y);
                this.Location = new Point(this.Location.X + point.X, this.Location.Y + point.Y);
            }
        }
        private void Spam_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StartPoint = new Point(e.X, e.Y);
            }
        }
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.Text = "X";
            label2.BorderStyle = BorderStyle.Fixed3D;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Text = "";
            label2.BorderStyle = BorderStyle.None;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введите текст или событие.") { textBox1.Text = ""; }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            Thread.Sleep(50);
            pictureBox1.BorderStyle = BorderStyle.None;
            MessageBox.Show("Для ввода текста просто введите его в текстовое поле. А затем кликните на то текстовое поле где этот текст должен появляться.\n" +
                "Если же вы хотите чтобы выполнялось какое-то событие(например изменить язык Alt+Shift), то необходимо вписать клавиши для нажатия в фигурные скабки, например: {F1}, {SHIFT}, {ENTER}");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) | (e.KeyChar == (char)Keys.Back)) { e.Handled = false; }
            else { e.Handled = true; }
        }
    }
}
