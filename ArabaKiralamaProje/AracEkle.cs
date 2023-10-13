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
    public partial class AracEkle : Form
    {
        public AracEkle()
        {
            InitializeComponent();
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
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }

        private void hele()
        {
          
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from markalar ORDER BY id ASC ", bag);
            da.Fill(dt);
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "marka";
            comboBox2.DataSource = dt;
        }
       
        private void AracEkle_Load(object sender, EventArgs e)
        {
            hele();
            comboBox2.Text = "Marka Seçiniz";
            comboBox7.Text = "Model Seçiniz";
            plakalar();
            listele();
            textBox5.Focus();
           
           
        }
        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox2.SelectedIndex != -1)
            {
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from modeller where marka_no = " + comboBox2.SelectedValue, bag);
                da.Fill(dt);
                comboBox7.ValueMember = "id";
                comboBox7.DisplayMember = "model";
                comboBox7.DataSource = dt;
            }
        }

        private void volvo_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM arac_blgsi", bag);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void kaydet()
        {
           

              

                    baglan();
                    OleDbCommand co = new OleDbCommand("INSERT INTO arac_blgsi VALUES ('" + comboBox2.Text + "','" + textBox5.Text.ToUpper().ToUpper() + "','" + comboBox1.Text + "','" + comboBox6.Text + "','" + comboBox4.Text + "','" + comboBox5.Text + "','" + comboBox3.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','" + textBox1.Text + "','" + "Uygun" + "')", bag);
                    co.ExecuteNonQuery();
                    listele();





            
        }
        void plakalar()
        {
            baglan();
            comboBox1.Items.Clear();
            OleDbCommand comn = new OleDbCommand("select Marka +'-'+Plaka from arac_blgsi ", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0].ToString());

            }
        }
        bool durum = true;
        void kayıtkontrol()
        {
            durum = true;
            baglan();
            OleDbCommand co = new OleDbCommand("select * from arac_blgsi", bag);
            OleDbDataReader dr = co.ExecuteReader();
            while (dr.Read())
            {
                if (textBox5.Text.ToUpper() == dr["Plaka"].ToString())
                {
                    durum = false;
                }
            }

        }
        
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            kayıtkontrol();
            try
            {


                if (durum == true)
                {

                    if (textBox5.TextLength < 7 || textBox3.Text == "" || textBox4.Text == "" || textBox3.Text == "" || comboBox2.Text == "" || comboBox6.Text == "" || comboBox4.Text == "" || comboBox5.Text == "" || comboBox3.Text == "")
                    {
                        hata();
                        MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }
                    else
                    {

                        kaydet();
                        MessageBox.Show("Kaydetme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        plakalar();
                        
                    }
                }
                else
                {
                    hata();
                    MessageBox.Show("Bir plakayı sadece bir kere kayıt edebilirsiniz. ", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                }
            }
            catch
            {
                hata();
                MessageBox.Show("Lüfen! Boşlukları Dikkatli Doldurunuz..", "Hata...", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
            
            }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa grdn = new AnaSayfa();
            grdn.Show();
            this.Close();
        }
        void sil()
        {
            OleDbCommand komut = new OleDbCommand("delete from arac_blgsi where Marka+'-'+Plaka  = '" + comboBox1.Text +  "'", bag);
            komut.ExecuteNonQuery();
            comboBox1.Text = "";
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglan();
               DialogResult a= MessageBox.Show("Eminmisiniz ?? ", "Uyarı...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (a == DialogResult.Yes)
               {
                   sil();
                   plakalar();
                   MessageBox.Show("Silme işlemi başarı ile gerçekleştirilmiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   listele();
               }
               else
               {
                   MessageBox.Show("Silme işlemi  gerçekleştirilmemiştir.", "Bildiri...", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "...", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void comboBox7_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Visible = true;
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 
        }

        private void volvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void comboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
           && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnaSayfa a = new AnaSayfa();
            a.Show();
            this.Hide();
        }

        private void kullanıcıİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kulbilgileri a = new kulbilgileri();
            a.Show();
            this.Hide();
        }

        private void müşteriİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Müşteriekle a = new Müşteriekle();
            a.Show();
            this.Hide();
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

        private void comboBox7_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBox6_KeyDown(object sender, KeyEventArgs e)
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

        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            textBox5.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            comboBox7.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();
            comboBox6.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            comboBox5.Text = dataGridView1.Rows[secili].Cells[5].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[secili].Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secili].Cells[7].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secili].Cells[8].Value.ToString();
            textBox1.Text = dataGridView1.Rows[secili].Cells[9].Value.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglan();
            OleDbCommand cmd = new OleDbCommand("Delete from arac_blgsi  where Plaka= '" + textBox5.Text + "'", bag);
            cmd.ExecuteNonQuery();
            listele();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglan();
                OleDbCommand cmd = new OleDbCommand("Update arac_blgsi set Marka= '" + comboBox2.Text + "', Plaka ='" + textBox5.Text + "',Model= '" + comboBox7.Text + "',Yıl= '" + comboBox6.Text + "',Yakıt_Tipi= '" + comboBox4.Text + "',Vites_Tipi= '" + comboBox5.Text + "',Renk= '" + comboBox3.Text + "',Km= '" + textBox4.Text + "',Fiyat= '" + textBox3.Text + "',Gnlk_fyt= '" + textBox4.Text + "'where plaka='" + textBox5.Text + "'", bag);
                cmd.ExecuteNonQuery();
                listele();
            } 
            catch(Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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