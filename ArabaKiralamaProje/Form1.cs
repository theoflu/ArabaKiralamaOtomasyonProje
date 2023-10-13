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
using System.Media;
namespace ArabaKiralamaProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void hg()
        {
            System.Media.SoundPlayer ses = new System.Media.SoundPlayer();
            ses.SoundLocation = "welcome.wav";
            ses.Play();
        }
        OleDbConnection bag = new OleDbConnection("provider=microsoft.ace.oledb.12.0; Data source =arckrlmdb.accdb");
        void baglan()
        {
            if (bag.State == ConnectionState.Closed)
            {
                bag.Open();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.Red;
           
         
        }
        private ContextMenu menu;
        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.Opacity = 0.0;
            textBox2.PasswordChar = '*';
            this.Text = "KULLANICI GİRİŞİ";
        }

           
             protected void kapat_click(object sender, System.EventArgs e)
        {
            Application.Exit();

        }
        protected void hakkında_click(object sender, System.EventArgs e)
        {
           hakkında a = new hakkında();
            a.Show();
            this.Hide();
        }

      
       
        
        
        private void button1_Click(object sender, EventArgs e)
        {
           
            baglan();
            OleDbCommand comn = new OleDbCommand("select * from kul_sif where kul_adi = '" + textBox1.Text + "'and sifre ='" + textBox2.Text + "'", bag);
            OleDbDataReader dr = comn.ExecuteReader();
            if (dr.Read())
            {
                AnaSayfa a = new AnaSayfa();
                a.Show();
                this.Hide();
                hg();
                NotifyIcon icn = new NotifyIcon();//yeni bir NotifyIcon tanımladık "örneğin ismi" mause ile üaerine geldiginde bilgi verir.
                icn.Visible = true;
                icn.Icon = new Icon("favicon.ico");
                menu = new ContextMenu();
                menu.MenuItems.Add(0, new MenuItem("Kapat", new System.EventHandler(kapat_click)));// göster click eventını oluşturuyoruz
                menu.MenuItems.Add(1, new MenuItem("Hakkında", new System.EventHandler(hakkında_click)));
             
                icn.ContextMenu = menu;
            }
            else
                MessageBox.Show("Kullanıcı adı ve şifrenizi  kontrol ediniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
            
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
         
          DialogResult a = MessageBox.Show("Emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (a == DialogResult.Yes)
            {
            }
            else if (a == DialogResult.No)
            {
                e.Cancel = true;

            }
        }
    

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
             
           

            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {  
            if(checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
               
            }
            else
                textBox2.PasswordChar = '*';
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)   
            {

                this.Opacity += 0.1;   

            }



            else
            {

                timer1.Enabled = false; 

            }
        }
      
        }
    }

