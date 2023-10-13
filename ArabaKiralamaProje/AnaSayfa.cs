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
using Microsoft.VisualBasic;
using System.IO;

namespace ArabaKiralamaProje
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
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
        
    

        private void AnaSayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void AnaSayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
        void showname()
        {   OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + 1.ToString() + "'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {
               string a= dr[0].ToString();
               button2.Text = "1)" + Path.GetFileName(a);
            }
            OleDbCommand comn2 = new OleDbCommand("Select konum from kısayollar where kimlik='" + 2.ToString() + "'", bag);
            OleDbDataReader dr2 = comn2.ExecuteReader();
            while (dr2.Read())
            {
                string a = dr2[0].ToString();
                button3.Text = "2)" + Path.GetFileName(a);
            } OleDbCommand comn3 = new OleDbCommand("Select konum from kısayollar where kimlik='" + 3.ToString() + "'", bag);
            OleDbDataReader dr3 = comn3.ExecuteReader();
            while (dr3.Read())
            {
                string a = dr3[0].ToString();
                button4.Text = "3)" + Path.GetFileName(a);
            } OleDbCommand comn5 = new OleDbCommand("Select konum from kısayollar where kimlik='" + 4.ToString() + "'", bag);
            OleDbDataReader dr5 = comn5.ExecuteReader();
            while (dr5.Read())
            {
                string a = dr5[0].ToString();
                button5.Text = "4)"+Path.GetFileName(a);
            } 
            OleDbCommand comn6= new OleDbCommand("Select konum from kısayollar where kimlik='" + 5.ToString() + "'", bag);
            OleDbDataReader dr6 = comn6.ExecuteReader();
            while (dr6.Read())
            {
                string a = dr6[0].ToString();
                button6.Text = "5)" + Path.GetFileName(a);
            }

           
          
        }
        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            try
            {
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                timer1.Enabled = true;
                baglan();


                OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + 6.ToString() + "'", bag);
                OleDbDataReader dr = comn.ExecuteReader();
                while (dr.Read())
                {

                    a1 = dr[0].ToString();
                    button1.Enabled = false;

                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                    button7.Visible = true;
                    showname();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void müşteriEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Müşteriekle a = new Müşteriekle();
            a.Show();
            this.Close();
           
        }

        private void arabaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AracEkle ac = new AracEkle();
            ac.Show();
            this.Close();
        }

        private void kullanıcıBilgileriGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kulbilgileri a = new kulbilgileri();
            a.Show();
            this.Close();
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void geriBildirimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardım a = new yardım();
            a.Show();
            this.Close();
        }

        private void öneriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            geribldrm a = new geribldrm();
            a.Show();
            this.Close();
        }

        private void öneriToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            oneri a = new oneri();
            a.Show();
            this.Close();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void kiralaTeslimAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kirala a = new kirala();
            a.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string saat = DateTime.Now.ToLongTimeString();

            label3.Text = saat;
        }

        private void müşteriİşemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Müşteriekle a = new Müşteriekle();
            a.Show();
            this.Close();
        }

        private void kullanıcıİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kulbilgileri a = new kulbilgileri();
            a.Show();
            this.Close();
        }

        private void araçİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AracEkle a = new AracEkle();
            a.Show();
            this.Close();
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

        private void müşteriListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tm_mustrlr a = new tm_mustrlr();
            a.Show();
            this.Hide();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hakkında a = new hakkında();
            a.Show();
            this.Close();
        }

        private void istatikliklerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            istatislikler a = new istatislikler();
            a.Show();
            this.Close();
        }

        private void müşteriRaporlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mstrirplr a = new mstrirplr();
            a.Show();
            this.Close();
        }

        private void araçTakipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            map a = new map();
            a.Show();
            this.Close();

        }
        int kısayol = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                for (int i = kısayol; i < 5; i++)
                {
                    if (kısayol < 5)
                    {
                        kısayol++;
                        MessageBox.Show(kısayol + ". Kısayolu Seçiniz");
                        OpenFileDialog ac = new OpenFileDialog();
                        string a;
                        ac.Filter = "Exe Files|*.exe";
                        ac.ShowDialog();
                        a = ac.FileName;

                        baglan();
                        OleDbCommand co = new OleDbCommand("INSERT INTO kısayollar Values('" + kısayol.ToString() + "','" + a + "')", bag);
                        co.ExecuteNonQuery();
                    }
                }
                OleDbCommand co2 = new OleDbCommand("INSERT INTO kısayollar Values('" + "6" + "','" + "Bitti" + "')", bag);
                co2.ExecuteNonQuery();
                button1.Enabled = false;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                showname();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
            

          
       
        string a1;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglan();
                int s = 1;
                OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + s.ToString() + "'", bag);
                OleDbDataReader dr = comn.ExecuteReader();
                while (dr.Read())
                {

                    a1 = dr[0].ToString();


                }
                button2.Text = Path.GetFileName(a1);
                System.Diagnostics.Process.Start(a1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglan();
                int s = 2;
                OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + s.ToString() + "'", bag);
                OleDbDataReader dr = comn.ExecuteReader();
                while (dr.Read())
                {

                    a1 = dr[0].ToString();


                }
                button3.Text = Path.GetFileName(a1);
                System.Diagnostics.Process.Start(a1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {try {
            baglan();
            int s = 3;
            OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + s.ToString() + "'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                a1 = dr[0].ToString();


            }
            button4.Text = Path.GetFileName(a1);
            System.Diagnostics.Process.Start(a1);
        }
        catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        { try
        {
            baglan();
            int s = 4;
            OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + s.ToString() + "'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                a1 = dr[0].ToString();


            }
            button5.Text = Path.GetFileName(a1);
            System.Diagnostics.Process.Start(a1);
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                baglan();
                int s = 5;
                OleDbCommand comn = new OleDbCommand("Select konum from kısayollar where kimlik='" + s.ToString() + "'", bag);
                OleDbDataReader dr = comn.ExecuteReader();
                while (dr.Read())
                {

                    a1 = dr[0].ToString();


                }
                button6.Text = Path.GetFileName(a1);
                System.Diagnostics.Process.Start(a1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void güncelle()
        {
            int a = int.Parse(Interaction.InputBox("Güncellenecek Kısayol Programı Numarası ?", "Uyarı..", "", 100, 100));
            OpenFileDialog ac = new OpenFileDialog();
            string ad;
            ac.Filter = "Exe Files|*.exe";
            ac.ShowDialog();
            ad = ac.FileName;
            OleDbCommand comn = new OleDbCommand("update kısayollar set konum ='" + ad + "'where kimlik='" + a + "'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                güncelle();
                showname();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           

        }
        
        }
    }

