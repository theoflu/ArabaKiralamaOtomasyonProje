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
    public partial class mstrirplr : Form
    {
        public mstrirplr()
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
       
        void kont()
        {
            baglan();

            OleDbCommand comn = new OleDbCommand("Select count(*) from msteri_tblsu", bag);
            krdkarclre3 = Convert.ToInt32(comn.ExecuteScalar());



        }
        private void mstrirplr_Load(object sender, EventArgs e)
        {
            krdkarclr();
            tmkaylrt();
            kont();
            
            label1.Text = krdkarclre.ToString();
            label2.Text = krdkarclre2.ToString();  
            label7.Text = krdkarclre.ToString();
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
    }
}
