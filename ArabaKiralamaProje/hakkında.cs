using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArabaKiralamaProje
{
    public partial class hakkında : Form
    {
        public hakkında()
        {
            InitializeComponent();
        }

        private void hakkında_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {


            if (this.Opacity > 0.0)    
            {

                this.Opacity -= 0.1;   

            }



            else
            {

                timer1.Enabled = false; 

                this.Close();
             

            }
 
        }

        private void hakkında_Load(object sender, EventArgs e)
        {
            timer3.Enabled = true;
            this.Opacity = 0.0; 
            timer2.Enabled = true;
            toolTip1.SetToolTip(this,"Çıkmak için forma tıklayınız."); //mesajın yazıldığı yer
            toolTip1.ToolTipTitle = "Uyarı..";
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.IsBalloon = true;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)  
            {

                this.Opacity += 0.1;   

            }



            else
            {

              timer2.Enabled = false; 

            }
        }

        private void hakkında_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void hakkında_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void hakkında_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void hakkında_Leave(object sender, EventArgs e)
        {
          
        }
      
        

        private void hakkında_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
          
        }
        int a = 3;
        private void timer3_Tick(object sender, EventArgs e)
        {
            a--;
            if (a==0)
            {
                toolTip1.Active = false;
            }
        }
    }
}
