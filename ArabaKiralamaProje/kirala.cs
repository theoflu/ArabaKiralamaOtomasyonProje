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
    public partial class kirala : Form
    {
        public kirala()
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

        void listele()
        {
            baglan();
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM krdk_arclr", bag);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        bool durum = true;
        void mstr_kont()
        {
           
            baglan();
            OleDbCommand co = new OleDbCommand("select *  from krdk_arclr", bag);
            OleDbDataReader dr = co.ExecuteReader();
            while (dr.Read())
            {
                if (comboBox1.Text.ToUpper() == dr["Musteri"].ToString())
                {
                    durum = false;
                }
            }
        }
        void kayıtkontrol()
        {
            
            baglan();
            OleDbCommand co = new OleDbCommand("select * from krdk_arclr", bag);
            OleDbDataReader dr = co.ExecuteReader();
            while (dr.Read())
            {
                if (comboBox4.Text.ToUpper() == dr["Plaka"].ToString())
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
        private void button1_Click(object sender, EventArgs e)
        {
            kiralama();
            listele();


            
        }
        
        void musteriler()
        {
            baglan();
            comboBox1.Items.Clear();  
            OleDbCommand comn = new OleDbCommand("Select Tc_Kimlik +' - '+Ad_Soyad from msteri_tblsu ", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0].ToString());
               

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
       int p;
       string mail;
       void kiralama()
        {
            baglan();
            kayıtkontrol();
            mstr_kont();
            if (durum == true)
            {
                if (comboBox1.Text == "" || comboBox4.Text == "")
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
                                p = int.Parse(dr["Fiyat"].ToString());


                            }
                            
                            double a = ((z + p) * 8) / 100;
                            double son =( a * z)+100;
                            textBox9.Text = son.ToString();
                            OleDbCommand co2= new OleDbCommand("INSERT INTO tum_kayıtlar VALUES ('" + comboBox1.Text + "','" + comboBox4.Text.ToUpper() + "','" + dateTimePicker3.Text + "','" + dateTimePicker4.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand comn2 = new OleDbCommand("Select E_mail from msteri_tblsu where Tc_Kimlik +' - '+Ad_Soyad ='" + comboBox1.Text + "'", bag);
                            OleDbDataReader dre = comn2.ExecuteReader();
                            while(dre.Read())
                            {
                                mail = dre["E_mail"].ToString();
                            }
                            
                            co2.ExecuteNonQuery();
                            OleDbCommand co = new OleDbCommand("INSERT INTO krdk_arclr VALUES ('" + comboBox1.Text + "','" + comboBox4.Text.ToUpper() + "','" + dateTimePicker3.Text + "','" + dateTimePicker4.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand ctrl = new OleDbCommand("Update arac_blgsi set Durum='" + "Kiralandı" + "' where Plaka='" + comboBox4.Text + "'", bag);
                            ctrl.ExecuteNonQuery();
                            co.ExecuteNonQuery();
                            plakalar(); tslmal();
                            MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MailMessage mesajım = new MailMessage();
                            SmtpClient istemc = new SmtpClient();
                            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
                            istemc.Port = 587;
                            istemc.Host = "smtp.live.com";
                            istemc.EnableSsl = true;
                            mesajım.To.Add(mail);
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
                                p = int.Parse(dr["Fiyat"].ToString());


                            }
                            double a = ((z + p) * 18) / 100;
                            double son = (a * z) + 100;
                            textBox9.Text = son.ToString();
                            plakalar();
                            OleDbCommand co2 = new OleDbCommand("INSERT INTO tum_kayıtlar VALUES ('" + comboBox1.Text + "','" + comboBox4.Text.ToUpper() + "','" + dateTimePicker5.Text + "','" + dateTimePicker6.Text + "','" + textBox9.Text + "')", bag);
                            co2.ExecuteNonQuery();
                            OleDbCommand co = new OleDbCommand("INSERT INTO krdk_arclr VALUES ('" + comboBox1.Text + "','" + comboBox4.Text.ToUpper() + "','" + dateTimePicker5.Text + "','" + dateTimePicker6.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand ctrl = new OleDbCommand("Update arac_blgsi set Durum='" + "Kiralandı" + "' where Plaka='" + comboBox4.Text + "'", bag);
                            ctrl.ExecuteNonQuery();
                            co.ExecuteNonQuery();
                            OleDbCommand comn2 = new OleDbCommand("Select E_mail from msteri_tblsu where Tc_Kimlik +' - '+Ad_Soyad ='" + comboBox1.Text + "'", bag);
                            OleDbDataReader dre = comn2.ExecuteReader();
                            while (dre.Read())
                            {
                                mail = dre["E_mail"].ToString();
                            }
                            plakalar(); tslmal();
                            MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MailMessage mesajım = new MailMessage();
                            SmtpClient istemc = new SmtpClient();
                            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
                            istemc.Port = 587;
                            istemc.Host = "smtp.live.com";
                            istemc.EnableSsl = true;
                            mesajım.To.Add(mail);
                            mesajım.From = new MailAddress("oflu_c0mpany@hotmail.com");
                            mesajım.Subject = "Fatura";
                            mesajım.Body = "Alış Tarihi : " + dateTimePicker5.Text + "\n Veriş Tarihi : " + dateTimePicker6.Text + "\n Araç Plaka : " + comboBox4.Text.ToUpper() + "\n Toplam Tutar : " + textBox9.Text;
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
                                p = int.Parse(dr["Gnlk_fyt"].ToString());


                            }
                            double a = ((z + p) * 8) / 100;
                            double son = (a * z) + 100;
                            textBox9.Text = son.ToString();
                            plakalar();
                            OleDbCommand co2 = new OleDbCommand("INSERT INTO tum_kayıtlar VALUES ('" + comboBox1.Text + "','" + comboBox4.Text.ToUpper() + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + textBox9.Text + "')", bag);
                            co2.ExecuteNonQuery();
                            OleDbCommand co = new OleDbCommand("INSERT INTO krdk_arclr VALUES ('" + comboBox1.Text + "','" + comboBox4.Text.ToUpper() + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + textBox9.Text + "')", bag);
                            OleDbCommand ctrl = new OleDbCommand("Update arac_blgsi set Durum='" + "Kiralandı" + "' where Plaka='" + comboBox4.Text + "'", bag);
                            ctrl.ExecuteNonQuery();
                            co.ExecuteNonQuery();
                            OleDbCommand comn2 = new OleDbCommand("Select E_mail from msteri_tblsu where Tc_Kimlik +' - '+Ad_Soyad ='" + comboBox1.Text + "'", bag);
                            OleDbDataReader dre = comn2.ExecuteReader();
                            while (dre.Read())
                            {
                                mail = dre["E_mail"].ToString();
                            }
                            plakalar();
                            tslmal();
                            MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MailMessage mesajım = new MailMessage();
                            SmtpClient istemc = new SmtpClient();
                            istemc.Credentials = new System.Net.NetworkCredential("oflu_c0mpany@hotmail.com", "fearlessoflu61_");
                            istemc.Port = 587;
                            istemc.Host = "smtp.live.com";
                            istemc.EnableSsl = true;
                            mesajım.To.Add(mail);
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
        
     

        private void button2_Click(object sender, EventArgs e)
       {
           baglan();
            OleDbCommand co=new OleDbCommand("Update  arac_blgsi set  Durum ='"+"Uygun"+"'where Plaka='"+comboBox2.Text+"'",bag);
            co.ExecuteNonQuery();
            OleDbCommand com = new OleDbCommand("Delete from krdk_arclr where Plaka='" + comboBox2.Text + "'", bag);
            com.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM krdk_arclr", bag);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            tslmal();

            
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
                maskedTextBox3.Visible = true;
                button4.Visible = true;

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                button1.Visible = true;
                button4.Visible = true;
                dateTimePicker5.Visible = false;
                dateTimePicker6.Visible = false;
                dateTimePicker3.Visible = true;
                dateTimePicker4.Visible = true;
                textBox9.Visible =true;
                label13.Visible =true;
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
        void a()
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
            maskedTextBox2.Visible = false;
            maskedTextBox3.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            button4.Visible =false;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {

                button1.Visible = true;
                button4.Visible = true;
                dateTimePicker3.Visible = false;
                dateTimePicker4.Visible = false;
                
                textBox9.Visible = true;
                label15.Visible = false;
                label16.Visible = false;
                label15.Visible = true;
                label16.Visible = true;
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
        void tslmal()
        {

            baglan();
            comboBox2.Items.Clear();
            OleDbCommand comn = new OleDbCommand("Select Plaka  from krdk_arclr ", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                comboBox2.Items.Add(dr[0].ToString());

            }
        }
        private void kirala_Load(object sender, EventArgs e)
        {
           
            a();
            musteriler();
            plakalar();
            
            tslmal();
            listele();
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
          

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnaSayfa a = new AnaSayfa();
            a.Show();
            this.Close();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kiralama();
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

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kiralama();
                    tslmal();
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
                    kiralama();
                    tslmal();
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
                    kiralama();
                    tslmal();
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
                    kiralama() ;
                    tslmal();
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
                    kiralama();
                    tslmal();
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
                    kiralama();
                    tslmal();
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

        private void maskedTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kiralama();
                    tslmal();
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

        private void maskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                DialogResult a = MessageBox.Show("Kaydetme işlemi için emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    plakalar();
                    kiralama();
                    tslmal();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
           
            comboBox1.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            if(radioButton5.Checked)
            {
                maskedTextBox2.Text =  dataGridView1.Rows[secili].Cells[2].Value.ToString();
                maskedTextBox3.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
                textBox9.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            }
            if (radioButton3.Checked)
            {
                dateTimePicker3.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                dateTimePicker4.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
                textBox9.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            } if (radioButton4.Checked)
            {
                dateTimePicker5.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();
                dateTimePicker6.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
                textBox9.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            }
           
           
            
        }

        private void Güncelle(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglan(); if (comboBox1.Text == "" || comboBox4.Text == "")
                {
                    MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    if (radioButton5.Checked)
                    {
                        OleDbCommand cmd = new OleDbCommand("Update krdk_arclr set Musteri= '" + comboBox1.Text + "',Alis_Tarihi= '" + maskedTextBox2.Text + "', Veris_Traihi= '" + maskedTextBox3.Text + "',Tutar= '" + textBox9.Text + "'where plaka='" + comboBox4.Text + "'", bag);
                        cmd.ExecuteNonQuery();
                        listele();


                    }
                    if (radioButton3.Checked)
                    {
                        OleDbCommand cmd = new OleDbCommand("Update krdk_arclr set Musteri= '" + comboBox1.Text + "',Alis_Tarihi= '" + dateTimePicker3.Text + "', Veris_Traihi= '" + dateTimePicker4.Text + "',Tutar= '" + textBox9.Text + "'where plaka='" + comboBox4.Text + "'", bag);
                        cmd.ExecuteNonQuery();
                        listele();

                    } if (radioButton4.Checked)
                    {
                        OleDbCommand cmd = new OleDbCommand("Update krdk_arclr set Musteri= '" + comboBox1.Text + "',Alis_Tarihi= '" + dateTimePicker5.Text + "', Veris_Traihi= '" + dateTimePicker6.Text + "',Tutar= '" + textBox9.Text + "'where plaka='" + comboBox4.Text + "'", bag);
                        cmd.ExecuteNonQuery();
                        listele();
                    }


                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
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

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

            
        }
        
        }
}
