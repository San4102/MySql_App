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
    public partial class Form2 : Form
    {
        private string connectionString = "Server=localhost;port=3306;Database=polyclinic_reception;username=monty;password=some_pass";
        public Form2()
        {
            InitializeComponent();
        }
        public int userIndex;
        
        

       

        private void button1_Click(object sender, EventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(connectionString);
            int I ;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from пользователи where Username = '" + txtUsername.Text + "'and Password= '" + txtPassword.Text +"'and Role = 'Пацієнт'";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            I = Convert.ToInt32(table.Rows.Count.ToString());
            if (I == 1)
            {
                this.Hide();
                Form4 main = new Form4(this);
                main.Show();
                userIndex = GetUserIDFromDatabase(txtUsername.Text);
            }
            

            int a ;
            MySqlCommand cmd1 = connection.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select * from пользователи where Username = '" + txtUsername.Text + "'and Password= '" + txtPassword.Text + "'and Role = 'Лікар'";
            DataTable table1 = new DataTable();
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
            adapter1.Fill(table1);
            a = Convert.ToInt32(table1.Rows.Count.ToString());
            connection.Close();
            if (a == 1)
            {  
                this.Hide();
                Form1 main = new Form1();
                main.Show();
      
            }
            if (a==0&&I==0)
            {
                MessageBox.Show("Такого користувача не існує!\nПеревірте правильність написання логіну та паролю." );
            }
            connection.Close();


        }
         private int GetUserIDFromDatabase(string username)
         {
            int userID=0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT User_ID FROM пользователи WHERE Username = @Username";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
            }

            return userID;
         }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 main = new Form3();
            main.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void exitbttn_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }




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
    }

}
