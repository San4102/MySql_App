using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySql_App
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string selectedValue = comboBox1.SelectedItem.ToString();
            if (selectedValue == "Лікар")
            {
                label4.Visible = true;
                textBox1.Visible = true;
            }
            else
            {
                label4.Visible = false;
                textBox1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(comboBox1.Text !="")
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                string name = txtUsername.Text;
                string pass = txtPassword.Text;
                string connString = "Server=localhost;port=3306;Database=polyclinic_reception;username=monty;password=some_pass";
                switch (selectedValue)
                {
                    case "Лікар":
                        if (textBox1.Text == "doctorcode")
                        {
                            
                            UserRegistrationHelper registrationHelper = new UserRegistrationHelper(connString);
                            registrationHelper.RegisterUser(txtUsername.Text, txtPassword.Text, comboBox1.Text);

                        }
                        else
                        {
                            MessageBox.Show("incorrect passcode");
                        }
                        break;

                    case"Пацієнт":
                        UserRegistrationHelper registrationHelper1 = new UserRegistrationHelper(connString);
                        registrationHelper1.RegisterUser(txtUsername.Text, txtPassword.Text, comboBox1.Text);
                        break;
                }
               
            }
            else
            {
                MessageBox.Show("Поле не может быть пустым");
            }
           
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        

        //for draging app

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
             if (e.Button == MouseButtons.Left)
             {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
             }
        }


        //bar buttons

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void exitbttn_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }


        //to form2
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 main = new Form2();
            main.Show();
        }

       

      

        
    }
}
