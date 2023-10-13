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
    public partial class tm_mustrlr : Form
    {
        public tm_mustrlr()
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
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM msteri_tblsu", bag);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void tm_mustrlr_Load(object sender, EventArgs e)
        {
            listele();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglan();
            DataTable dtt = new DataTable();
            OleDbDataAdapter dt = new OleDbDataAdapter("Select * from msteri_tblsu where Tc_Kimlik LIKE  '%" + textBox1.Text + "%'", bag);
            dt.Fill(dtt);
            dataGridView1.DataSource = dtt;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa a = new AnaSayfa();
            a.Show();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Müşteriekle ar = new Müşteriekle();
             int secili = dataGridView1.SelectedCells[0].RowIndex;
            ar.textBox1.Text = dataGridView1.Rows[secili].Cells[0].Value.ToString();
            ar.textBox2.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            if(dataGridView1.Rows[secili].Cells[2].Value.ToString()=="Bayan")
            {
                ar.radioButton2.Checked = true;
            }
            if (dataGridView1.Rows[secili].Cells[2].Value.ToString() == "Bay")
            {
                ar.radioButton1.Checked = true;
            }
            ar.dateTimePicker2.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            ar.comboBox1.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            ar.textBox3.Text = dataGridView1.Rows[secili].Cells[5].Value.ToString();
            ar.maskedTextBox1.Text = dataGridView1.Rows[secili].Cells[6].Value.ToString();
            ar.textBox5.Text = dataGridView1.Rows[secili].Cells[7].Value.ToString();
            ar.textBox6.Text = dataGridView1.Rows[secili].Cells[8].Value.ToString();
            ar.comboBox2.Text = dataGridView1.Rows[secili].Cells[9].Value.ToString();
            ar.dateTimePicker1.Text = dataGridView1.Rows[secili].Cells[10].Value.ToString();
            ar.comboBox3.Text = dataGridView1.Rows[secili].Cells[11].Value.ToString();
            ar.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }
    }
}
