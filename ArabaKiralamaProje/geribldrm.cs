using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using Microsoft.VisualBasic;

namespace ArabaKiralamaProje
{
    public partial class geribldrm : Form
    {
        public geribldrm()
        {
            InitializeComponent();
        }
        string rstglkod()
        {
            String karakterler = "0123456789QWERTYUIOPASDFGHJKLZXCVBNM";
            Random rnd = new Random();
            String pano = "";
            for (int i = 0; i < 6; i++)
            {
                pano += karakterler[rnd.Next(karakterler.Length)];
            }


            MailMessage mesajım = new MailMessage();
            SmtpClient istemc = new SmtpClient();
            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
            istemc.Port = 587;
            istemc.Host = "smtp.live.com";
            istemc.EnableSsl = true;
            mesajım.To.Add(textBox1.Text);
            mesajım.From = new MailAddress("oflu_c0mpany@hotmail.com");
            mesajım.Subject = "Onay Kodu";
            mesajım.Body = "Onay Kodunuz = " + pano;
            istemc.Send(mesajım);
            return pano;
        }
        void pstgndr()
        {
            MailMessage mesajım = new MailMessage();
            SmtpClient istemc = new SmtpClient();
            istemc.Credentials = new System.Net.NetworkCredential(textBox1.Text, textBox2.Text);
            istemc.Port = 587;
            istemc.Host = "smtp.live.com";
            istemc.EnableSsl = true;
            mesajım.To.Add("oflu_c0mpany@hotmail.com");
            mesajım.From = new MailAddress(textBox1.Text);
            mesajım.Subject = "Geri Bildirim";
            mesajım.Body = textBox3.Text;
            istemc.Send(mesajım);
            MessageBox.Show("Geribildiriminiz için Teşekkürler", "UYARI..", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
      
        private void geribldrm_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        void gonder()
        {
            try
            {





                string a = rstglkod();
                string b = Interaction.InputBox("Postanıza Gelen Onay Kodunu Giriniz ", " Doğrulama", "", 150, 150);

                if (a == b)
                {
                    pstgndr();

                }
                else if (a != b)
                {

                    MessageBox.Show("Kodunuzu Hatalı Girdiniz Yeni Kod İsteyip Tekrar Deneyiniz ", "Uyarı!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }


            catch
            {
                MessageBox.Show("Lütfen Boşlukları Dikkatli Doldurunuz", "UYARI...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            gonder();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
                textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa a = new AnaSayfa();
            a.Show();
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    gonder();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    this.Close();
                    AnaSayfa c = new AnaSayfa();
                    c.Show();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    gonder();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    this.Close();
                    AnaSayfa c = new AnaSayfa();
                    c.Show();
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    gonder();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    this.Close();
                    AnaSayfa c = new AnaSayfa();
                    c.Show();
                }
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    gonder();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    this.Close();
                    AnaSayfa c = new AnaSayfa();
                    c.Show();
                }
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    gonder();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    this.Close();
                    AnaSayfa c = new AnaSayfa();
                    c.Show();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            button9.BackColor = Color.Tomato;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.BackColor = Color.Transparent;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            button8.BackColor = Color.Gray;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.BackColor = Color.Transparent;
        }

        private void geribldrm_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
