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
    public partial class map : Form
    {
        public map()
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
        void plakalar()
        {
            baglan();
            comboBox1.Items.Clear();
            OleDbCommand comn = new OleDbCommand("Select Plaka from arac_blgsi Where Durum='Uygun'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0].ToString());

            }
        }    string[] dizi = { "map.mp4", "map2.mp4", "map3.mp4",};
        Random rnd = new Random();



        int scm;
        string da = "map2.mp4";
        private void map_Load(object sender, EventArgs e)
        {
            
            plakalar();

            groupBox1.Enabled = false;
        }
        int a = 35;
        private void timer1_Tick(object sender, EventArgs e)
        {
            a--;
            if (a == 2)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                timer1.Stop();
                a = 35;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen Boşlukları Dikkatli Doldurunuz..", "Uyarı..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = 0; i <= dizi.Length; i++)
                {
                    scm = rnd.Next(0, 3);
                }
                timer1.Enabled = true;
                axWindowsMediaPlayer1.URL = dizi[scm].ToString();
                axWindowsMediaPlayer1.Ctlcontrols.play();
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

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa a = new AnaSayfa();
            a.Show();
            this.Close();
        }
    }
}
