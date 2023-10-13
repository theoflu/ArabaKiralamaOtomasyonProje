using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace ArabaKiralamaProje
{
    public partial class kulbilgileri : Form
    {
        public kulbilgileri()
        {
            InitializeComponent();
        }
         OleDbConnection bag = new OleDbConnection("provider=microsoft.ace.oledb.12.0; Data source =arckrlmdb.accdb");
         void baglan()
         {
             if (bag.State == ConnectionState.Closed)
             {
                 bag.Open();
             }
         }
        void getir()
         {
             baglan();
             OleDbCommand comn = new OleDbCommand("select kul_adi , sifre from kul_sif ", bag);
             OleDbDataReader dr = comn.ExecuteReader();
             while (dr.Read())
             {
                 MessageBox.Show("Kullanıcı Adı = " + dr["kul_adi"].ToString() + "\nŞifre              =" + dr["sifre"].ToString(), "HATIRLATMA", MessageBoxButtons.OK, MessageBoxIcon.Information);

             }
         }
        void guncelle()
        { 
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;
            int sif = textBox2.TextLength;
            int kul = textBox1.TextLength;
            if(textBox2.Text==textBox3.Text)
            {
                if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Boş alan bırakmayınız", "UYARI!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (kul < 5 || kul > 20 || sif < 8 || sif > 20)
                    {
                        MessageBox.Show("Şifreniz Değiştirilmedi!!", "UYARI!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        baglan();
                        OleDbCommand komut = new OleDbCommand("update kul_sif set kul_adi ='" + textBox1.Text + "', sifre ='" + textBox2.Text + "'", bag);
                        OleDbDataReader dr = komut.ExecuteReader();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        MessageBox.Show("Şifreniz Sorunsuz Değiştirilmiştir", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                }
            else
                MessageBox.Show("Şifreleriniz Uyuşmuyor.","UYARI..",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

            
           
        
        private void kulbilgileri_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            groupBox2.Enabled = false;
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            this.Text = "Kullanıcı Bilgileri";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            guncelle();
                
               
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            getir();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnaSayfa grdn = new AnaSayfa();
            grdn.Show();
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    guncelle();
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
                    guncelle();
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
                    guncelle(); ;
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
                    guncelle();
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

        private void kulbilgileri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    guncelle();
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
        }
    }

