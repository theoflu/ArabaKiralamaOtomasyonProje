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
    public partial class istatislikler : Form
    {
        public istatislikler()
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
        int krdkarclre;
        int krdkarclre2;
        int krdkarclre3;
        int krdkarclre4;
        void krdkarclr()
       {
           baglan();
            OleDbCommand cmd = new OleDbCommand("select count(*) from krdk_arclr", bag);
            krdkarclre = Convert.ToInt32(cmd.ExecuteScalar());
            
           
        }
        void tmkaylrt()
        {
            baglan();
            OleDbCommand cmd = new OleDbCommand("select count(*) from tum_kayıtlar", bag);
            krdkarclre2 = Convert.ToInt32(cmd.ExecuteScalar());


        }
        void tmarclr()
        {
            baglan();
            OleDbCommand cmd = new OleDbCommand("select count(*) from arac_blgsi", bag);
            krdkarclre4 = Convert.ToInt32(cmd.ExecuteScalar());


        }
        void kont()
        {
            baglan();

            OleDbCommand comn = new OleDbCommand("Select count(*) from arac_blgsi Where Durum='Uygun'", bag);
            krdkarclre3 = Convert.ToInt32(comn.ExecuteScalar());

           
            
        }
        private void istatislikler_Load(object sender, EventArgs e)
        {
            krdkarclr();
            tmkaylrt();
            kont();
            tmarclr();
            label1.Text = krdkarclre.ToString();
            label2.Text = krdkarclre2.ToString();
            label3.Text = krdkarclre3.ToString();
            label7.Text = krdkarclre4.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnaSayfa a = new AnaSayfa();
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
    }
}
