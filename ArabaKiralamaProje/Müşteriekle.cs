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
using System.Net.Mail;
namespace ArabaKiralamaProje
{
    public partial class Müşteriekle : Form
    {
        public Müşteriekle()
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
       

        bool durum = true;
        void kayıtkontrol()
        {
            durum = true;
            baglan();
            OleDbCommand co = new OleDbCommand("select * from msteri_tblsu", bag);
            OleDbDataReader dr = co.ExecuteReader();
            while (dr.Read())
            {
                if (textBox2.Text == dr["Tc_Kimlik"].ToString())
                {
                    durum = false;
                }
            }

        }
        void hata()
        {
            for (int a = 0; a <= 100; a++)
            {
                this.Left += 5;
                this.Top += 5;
                this.Left -= 5;
                this.Top -= 5;
            }
        }
        void pstgndr()
        {
            MailMessage mesajım = new MailMessage();
            SmtpClient istemc = new SmtpClient();
            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
            istemc.Port = 587;
            istemc.Host = "smtp.live.com";
            istemc.EnableSsl = true;
            mesajım.To.Add(textBox3.Text);
            mesajım.From = new MailAddress("oflu_c0mpany@hotmail.com");
            mesajım.Subject = "Fatura";
            mesajım.Body = "Alış Tarihi : "+dateTimePicker1.Text+"\n Veriş Tarihi : "+dateTimePicker2.Text+"\n Araç Plaka : "+comboBox4.Text+"\n Toplam Tutar : "+textBox9.Text;
            istemc.Send(mesajım);
            MessageBox.Show("Mail Gönderilmiştir", "UYARI..", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        void kaydet()
        {
            baglan();
            kayıtkontrol();
            if (durum == true)
            {
                if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "" || textBox3.Text == "" || comboBox3.Text == "" || textBox5.Text == "" || maskedTextBox1.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    dataGridView1.Visible = false;
                    groupBox4.Visible = true;
                    textBox7.Text = textBox2.Text +" - "+ textBox1.Text;
                    if (radioButton1.Checked == true)
                    {


                        OleDbCommand co = new OleDbCommand("INSERT INTO msteri_tblsu VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + radioButton1.Text + "','" + dateTimePicker2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + maskedTextBox1.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Text + "','" + comboBox3.Text + "')", bag);
                        MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        co.ExecuteNonQuery();
                    }
                    else if (radioButton2.Checked == true)
                    {
                        OleDbCommand co = new OleDbCommand("INSERT INTO msteri_tblsu VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + radioButton2.Text + "','" + dateTimePicker2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + maskedTextBox1.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Text + "','" + comboBox3.Text + "')", bag);
                        MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        co.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                hata();
                MessageBox.Show("Bir kişiyi sadece bir kere kayıt edebilirsiniz. ", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }

        }
        void plakalar()
        {
            baglan();
            comboBox4.Items.Clear();
            OleDbCommand comn = new OleDbCommand("Select Plaka from arac_blgsi Where Durum='Uygun'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                comboBox4.Items.Add(dr[0].ToString());

            }
        }

        int u;
        bool durum2 = true;
        void mstr_kont()
        {

            baglan();
            OleDbCommand co = new OleDbCommand("select *  from krdk_arclr", bag);
            OleDbDataReader dr = co.ExecuteReader();
            while (dr.Read())
            {
                if (comboBox1.Text.ToUpper() == dr["Musteri"].ToString())
                {
                    durum2 = false;
                }
            }
        }
        void kiralamakontrol()
        {

            baglan();
            OleDbCommand co = new OleDbCommand("select * from krdk_arclr", bag);
            OleDbDataReader dr = co.ExecuteReader();
            while (dr.Read())
            {
                if (comboBox4.Text.ToUpper() == dr["Plaka"].ToString())
                {
                    durum2 = false;
                }
            }

        }
        void kirala()
        {
            baglan();
            kiralamakontrol();
            mstr_kont();
            if (durum2 == true)
            {
                if (textBox7.Text == "" || comboBox4.Text == "")
                {
                    MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    if (radioButton3.Checked == true)
                    {
                        try
                        {
                            DateTime bTarih = Convert.ToDateTime(dateTimePicker3.Text);
                            DateTime kTarih = Convert.ToDateTime(dateTimePicker4.Text);
                            TimeSpan Sonuc = kTarih - bTarih;
                            int z = int.Parse(Sonuc.TotalDays.ToString());
                            OleDbCommand comn = new OleDbCommand("select Fiyat from arac_blgsi where Plaka ='" + comboBox4.Text + "'", bag);
                            OleDbDataReader dr = comn.ExecuteReader();
                            while (dr.Read())
                            {
                                u = int.Parse(dr["Fiyat"].ToString());


                            }
                            double a = ((u + u) * 8) / 100;
                            double son = (a * z) + 100;
                            textBox9.Text = son.ToString();
                            OleDbCommand co = new OleDbCommand("INSERT INTO krdk_arclr VALUES ('" + textBox7.Text + "','" + comboBox4.Text + "','" + dateTimePicker3.Text + "','" + dateTimePicker4.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand ctrl = new OleDbCommand("Update arac_blgsi set Durum='" + "Kiralandı" + "' where Plaka='" + comboBox4.Text + "'", bag);
                            ctrl.ExecuteNonQuery();
                            co.ExecuteNonQuery();
                            plakalar();
                            MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MailMessage mesajım = new MailMessage();
                            SmtpClient istemc = new SmtpClient();
                            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
                            istemc.Port = 587;
                            istemc.Host = "smtp.live.com";
                            istemc.EnableSsl = true;
                            mesajım.To.Add(textBox3.Text);
                            mesajım.From = new MailAddress("oflu_c0mpany@hotmail.com");
                            mesajım.Subject = "Fatura";
                            mesajım.Body = "Alış Tarihi : " + dateTimePicker3.Text + "\n Veriş Tarihi : " + dateTimePicker4.Text + "\n Araç Plaka : " + comboBox4.Text + "\n Toplam Tutar : " + textBox9.Text;
                            istemc.Send(mesajım);
                            MessageBox.Show("Mail Gönderilmiştir", "UYARI..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("" + ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }


                    else if (radioButton4.Checked == true)
                    {
                        try
                        {

                            DateTime bTarih = Convert.ToDateTime(dateTimePicker5.Text);
                            DateTime kTarih = Convert.ToDateTime(dateTimePicker6.Text);
                            TimeSpan Sonuc = kTarih - bTarih;
                            int z = int.Parse(Sonuc.TotalDays.ToString());
                            OleDbCommand comn = new OleDbCommand("select Fiyat  from arac_blgsi where Plaka ='" + comboBox4.Text + "'", bag);
                            OleDbDataReader dr = comn.ExecuteReader();
                            while (dr.Read())
                            {
                                u = int.Parse(dr["Fiyat"].ToString());


                            }
                            double a = ((u + u) * 18) / 100;
                            double son = (a * z) + 100;
                            textBox9.Text = son.ToString();

                            OleDbCommand co = new OleDbCommand("INSERT INTO krdk_arclr VALUES ('" + textBox7.Text + "','" + comboBox4.Text + "','" + dateTimePicker5.Text + "','" + dateTimePicker6.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand ctrl = new OleDbCommand("Update arac_blgsi set Durum='" + "Kiralandı" + "' where Plaka='" + comboBox4.Text + "'", bag);
                            ctrl.ExecuteNonQuery();
                            co.ExecuteNonQuery();
                            plakalar();
                            MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MailMessage mesajım = new MailMessage();
                            SmtpClient istemc = new SmtpClient();
                            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
                            istemc.Port = 587;
                            istemc.Host = "smtp.live.com";
                            istemc.EnableSsl = true;
                            mesajım.To.Add(textBox3.Text);
                            mesajım.From = new MailAddress("oflu_c0mpany@hotmail.com");
                            mesajım.Subject = "Fatura";
                            mesajım.Body = "Alış Tarihi : " + dateTimePicker5.Text + "\n Veriş Tarihi : " + dateTimePicker6.Text + "\n Araç Plaka : " + comboBox4.Text + "\n Toplam Tutar : " + textBox9.Text;
                            istemc.Send(mesajım);
                            MessageBox.Show("Mail Gönderilmiştir", "UYARI..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("" + ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (radioButton5.Checked == true)
                    {
                        try
                        {
                            DateTime bTarih = Convert.ToDateTime(maskedTextBox2.Text);
                            DateTime kTarih = Convert.ToDateTime(maskedTextBox3.Text);
                            TimeSpan Sonuc = kTarih - bTarih;
                            double z = double.Parse(Sonuc.TotalHours.ToString());
                            OleDbCommand comn = new OleDbCommand("select Gnlk_fyt  from arac_blgsi where Plaka ='" + comboBox4.Text + "'", bag);
                            OleDbDataReader dr = comn.ExecuteReader();
                            while (dr.Read())
                            {
                                u = int.Parse(dr["Gnlk_fyt"].ToString());


                            }
                            double a = ((u + u) * 8) / 100;
                            double son = (a * z) + 100;
                            textBox9.Text = son.ToString();

                            OleDbCommand co = new OleDbCommand("INSERT INTO krdk_arclr VALUES ('" + textBox7.Text + "','" + comboBox4.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand ctrl = new OleDbCommand("Update arac_blgsi set Durum='" + "Kiralandı" + "' where Plaka='" + comboBox4.Text + "'", bag);
                            ctrl.ExecuteNonQuery();
                            co.ExecuteNonQuery();
                            plakalar();
                            MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MailMessage mesajım = new MailMessage();
                            SmtpClient istemc = new SmtpClient();
                            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
                            istemc.Port = 587;
                            istemc.Host = "smtp.live.com";
                            istemc.EnableSsl = true;
                            mesajım.To.Add(textBox3.Text);
                            mesajım.From = new MailAddress("oflu_c0mpany@hotmail.com");
                            mesajım.Subject = "Fatura";
                            mesajım.Body = "Alış Tarihi : " + maskedTextBox2.Text + "\n Veriş Tarihi : " + maskedTextBox3.Text + "\n Araç Plaka : " + comboBox4.Text + "\n Toplam Tutar : " + textBox9.Text;
                            istemc.Send(mesajım);
                            MessageBox.Show("Mail Gönderilmiştir", "UYARI..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("" + ex.Message, "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                  

                }
                

            }
            else
            {
                hata();
                MessageBox.Show("Bir plakayı ve Müşteriye sadece bir araba kiralayabilir ve kayıt edebilirsiniz. ", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private void Müşteriekle_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kirala();
           
           
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                button1.Visible = true;
                dateTimePicker5.Visible = false;
                dateTimePicker6.Visible = false;
                dateTimePicker3.Visible = true;
                dateTimePicker4.Visible = true;
                textBox9.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = false;
                label19.Visible = false;
                maskedTextBox2.Visible = false;
                maskedTextBox3.Visible = false;
            }
           
          
        }
        public void a()
        {
            dateTimePicker5.Visible = false;
            dateTimePicker6.Visible = false;
            dateTimePicker3.Visible = false;
            dateTimePicker4.Visible = false;
            textBox9.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            button1.Visible = false;
           
            groupBox4.Visible = false;
           
            maskedTextBox2.Visible = false;
            maskedTextBox3.Visible = false;
            label18.Visible =false;
            label19.Visible = false;
            
        }
        void listele()
        {
            baglan();
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT *  FROM msteri_tblsu", bag);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Müşteriekle_Load(object sender, EventArgs e)
        {
            ToolTip Aciklama = new ToolTip();
            Aciklama.ToolTipTitle = "Dikkat!";
            Aciklama.ToolTipIcon = ToolTipIcon.Warning;
            Aciklama.IsBalloon = true;

            Aciklama.SetToolTip(button6, "Güncelleme İşlemi Tc Kimliğe göre yapılmaktadır.");
            Aciklama.SetToolTip(button3, "Güncelleme İşlemi Tc Kimliğe göre yapılmaktadır.");
            Aciklama.SetToolTip(button6, "Güncelleme İşlemi Tc Kimliğe göre yapılmaktadır.");
            Aciklama.SetToolTip(button3, "Güncelleme İşlemi Tc Kimliğe göre yapılmaktadır.");
            button4.Visible = false; 
            textBox1.Focus();
            dataGridView1.Visible = false;
            button6.Visible = false;
            a();
            plakalar();
            listele();
           
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {

                button1.Visible = true;
                dateTimePicker3.Visible = false;
                dateTimePicker4.Visible = false;
                textBox9.Visible = false;
                textBox9.Visible = true;
                label15.Visible = false;
                label16.Visible = false;
                label15.Visible = true;
                label16.Visible =true;
                label17.Visible = false;
                label17.Visible = true;
                dateTimePicker5.Visible = true;
                dateTimePicker6.Visible = true;
                label18.Visible = false;
                label19.Visible = false;
                maskedTextBox2.Visible = false;
                maskedTextBox3.Visible = false;
            }

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
      
        }
       
        

        private void button2_Click(object sender, EventArgs e)
        {
            plakalar();
            kaydet();
           
        
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AnaSayfa grdn = new AnaSayfa();
            grdn.Show();
            this.Close();
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if(textBox2.TextLength<11||textBox2.TextLength>11)
            {
                MessageBox.Show("Tc kimlik 11 haneden eksik olamaz!","Uyarı...",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textBox2.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar); 
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToolTip Aciklama = new ToolTip();
            Aciklama.ToolTipTitle = "Dikkat!";
            Aciklama.ToolTipIcon = ToolTipIcon.Warning;
            Aciklama.IsBalloon = true;
            Aciklama.SetToolTip(button1, "Kodzilla.wordpress.com");
            groupBox4.Visible = false;
            dataGridView1.Visible = true;
            button3.Visible = false;
            button6.Visible = true;
            
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                button1.Visible = true;
                dateTimePicker5.Visible = false;
                dateTimePicker6.Visible = false;
                dateTimePicker3.Visible = false;
                dateTimePicker4.Visible = false;
                textBox9.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                maskedTextBox2.Visible = true;
                maskedTextBox3.Visible =true;

            }
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
   
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if ( e.KeyCode == Keys.Escape)
            {
                DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    this.Close();
                    AnaSayfa c = new AnaSayfa();
                    c.Show();
                }
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
                }
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void araçİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AracEkle a = new AracEkle();
            a.Show();
            this.Hide();
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Focus();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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
                    plakalar();
                    kaydet();
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

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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
        private void dateTimePicker3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                   kaydet();
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

        private void dateTimePicker6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void dateTimePicker4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kaydet();
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

        private void dateTimePicker5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                   kaydet();
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

        private void button6_Click(object sender, EventArgs e)
        {
           
          
            try
            {
                if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "" || textBox3.Text == "" || comboBox3.Text == "" || textBox5.Text == "" || maskedTextBox1.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        baglan();
                        textBox7.Text = textBox2.Text + " - " + textBox1.Text;
                        DialogResult a = MessageBox.Show("Eminmisiniz ?? ", "Uyarı...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (a == DialogResult.Yes)
                        {
                            
                            OleDbCommand cmd = new OleDbCommand("Update msteri_tblsu set Ad_Soyad= '" + textBox1.Text + "',Cinsiyet= '" + radioButton1.Text + "',Dgm_Tarihi= '" + dateTimePicker2.Text + "',Dgm_Yeri= '" + comboBox1.Text + "',E_Mail= '" + textBox3.Text + "',Tel= '" + maskedTextBox1.Text + "',Adres= '" + textBox5.Text + "',Ehliyet_No= '" + textBox6.Text + "',Ehliyet_tipi= '" + comboBox2.Text + "',Ehliyet_tarhi='" + dateTimePicker1.Text + "',Ehlyt_vrln_yer='" + comboBox3.Text + "'where Tc_Kimlik='" + textBox2.Text + "'", bag);
                            cmd.ExecuteNonQuery();
                            listele();
                            MessageBox.Show("Güncelleme işlemi  gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.Visible = false;
                            groupBox4.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme işlemi  gerçekleştirilmemiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                       

                    }
                    if (radioButton2.Checked == true)
                    {
                        DialogResult a = MessageBox.Show("Eminmisiniz ?? ", "Uyarı...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (a == DialogResult.Yes)
                        {

                            OleDbCommand cmd = new OleDbCommand("Update msteri_tblsu set Ad_Soyad= '" + textBox1.Text + "',Cinsiyet= '" + radioButton2.Text + "',Dgm_Tarihi= '" + dateTimePicker2.Text + "',Dgm_Yeri= '" + comboBox1.Text + "',E_Mail= '" + textBox3.Text + "',Tel= '" + maskedTextBox1.Text + "',Adres= '" + textBox5.Text + "',Ehliyet_No= '" + textBox6.Text + "',Ehliyet_tipi= '" + comboBox2.Text + "',Ehliyet_tarhi='" + dateTimePicker1.Text + "',Ehlyt_vrln_yer='" + comboBox3.Text + "'where Tc_Kimlik='" + textBox2.Text + "'", bag);
                            cmd.ExecuteNonQuery();
                            listele();
                            dataGridView1.Visible = false;
                            groupBox4.Visible = true;
                            MessageBox.Show("Güncelleme işlemi  gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme işlemi  gerçekleştirilmemiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            if(dataGridView1.Rows[secili].Cells[2].Value.ToString()=="Bayan")
            {
                radioButton2.Checked = true;
            }
            if (dataGridView1.Rows[secili].Cells[2].Value.ToString() == "Bay")
            {
                radioButton1.Checked = true;
            }
           dateTimePicker2.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
        textBox3.Text = dataGridView1.Rows[secili].Cells[5].Value.ToString();
        maskedTextBox1.Text = dataGridView1.Rows[secili].Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.Rows[secili].Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.Rows[secili].Cells[8].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[secili].Cells[9].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[secili].Cells[10].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[secili].Cells[11].Value.ToString();
        }
        void sil()
        {
            OleDbCommand komut = new OleDbCommand("delete from msteri_tblsu where Tc_Kimlik  = '" + textBox2.Text + "'", bag);
            komut.ExecuteNonQuery();
           


        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "" || textBox3.Text == "" || comboBox3.Text == "" || textBox5.Text == "" || maskedTextBox1.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    baglan();
                    textBox7.Text = textBox2.Text + " - " + textBox1.Text;
                    DialogResult a = MessageBox.Show("Eminmisiniz ?? ", "Uyarı...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (a == DialogResult.Yes)
                    {
                        sil();
                        MessageBox.Show("Silme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                      
                        textBox1.Text = "";
                        textBox2.Text ="";
                        
                            radioButton2.Checked = false;
                      
                            radioButton1.Checked =false;
                       
                        dateTimePicker2.Text =DateTime.Today.ToLongDateString();
                        comboBox1.Text ="";
                        textBox3.Text = "";
                        maskedTextBox1.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        comboBox2.Text = "";
                        dateTimePicker1.Text = DateTime.Today.ToLongDateString();
                        comboBox3.Text = "";
                        dataGridView1.Visible = false;
                        groupBox4.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi  gerçekleştirilmemiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "...", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
            button4.Visible = true;
            dataGridView1.Visible = true;
            button7.Visible = false;
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}
