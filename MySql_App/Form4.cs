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
    public partial class Form4 : Form
    {
        
        private Form2 form2;
        public Form4(Form2 form2Instance)
        {
            InitializeComponent();
            form2 = form2Instance;
            
        }
          
        private void button8_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            int user_id = form2.userIndex;
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = $"SELECT пациенты.`Patient_ID` as `Ваш ID`, пациенты.`Name`, пациенты.`Date of Birth`,пациенты.`Gender`, пациенты.`Adress`, пациенты.`Contact Information`FROM пациенты JOIN пользователи ON пациенты.UserID = пользователи.User_ID where пациенты.UserID= {user_id}";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;
        }
        private void button2_Click(object sender, EventArgs e)//card open bttn
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            int user_id = form2.userIndex;
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = $"select амбулаторные_карты.Creation_Date,амбулаторные_карты.Medical_History,амбулаторные_карты.`Treatment and Prescriptions` from пациенты join амбулаторные_карты on пациенты.Patient_ID = амбулаторные_карты.Patient_ID where UserID = {user_id};";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;
        }
        
        private void button4_Click(object sender, EventArgs e)//schedule
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = "SELECT  v.Doctor_Name,v.Specialization, r.Weekday, r.Start_Time, r.End_Time FROM расписание_приёма_врачей r JOIN врачи v ON r.Doctor_ID = v.Doctors_ID;";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;
            
        }
        
        private void button5_Click(object sender, EventArgs e)//appointment
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = "SELECT * FROM услуги;";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;

        }
        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox3.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView3.Visible = true;
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = "SELECT расписание_приёма_врачей.Doctor_ID, расписание_приёма_врачей.Weekday, расписание_приёма_врачей.Start_Time, расписание_приёма_врачей.End_Time, врачи.Doctor_Name  FROM расписание_приёма_врачей join врачи on расписание_приёма_врачей.Doctor_ID = врачи.Doctors_ID ; ";
            string script1 = "SELECT запись_на_приём.Doctor_ID, запись_на_приём.Appointment_Date_and_Time, врачи.Specialization, врачи.Doctor_Name FROM запись_на_приём join врачи on запись_на_приём.Doctor_ID = врачи.Doctors_ID";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            DataTable resultTable1 = dbHelper.ExecuteQuery(script1);
            dataGridView2.DataSource = resultTable;
            dataGridView3.DataSource = resultTable1;
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
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void button3_Click(object sender, EventArgs e)//Minimize bttn
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void button1_Click(object sender, EventArgs e)//EXIT bttn
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
                int user_id = form2.userIndex;
            
                DatabaseHelper dbHelper = new DatabaseHelper();
            string script = "INSERT INTO пациенты (UserID, `Name`, `Date of Birth`, `Gender`, `Adress`, `Contact Information`) " +
            $"VALUES ({user_id}, '{textBox7.Text}', '{textBox8.Text}', '{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')";

            string script1 = $"INSERT INTO `запись_на_приём` (Patient_ID, Doctor_ID,Appointment_Date_and_Time) values('{textBox4.Text}','{textBox5.Text}')";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
                dataGridView1.DataSource = resultTable;
                
            
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int user_id = form2.userIndex;
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = $"UPDATE пациенты JOIN пользователи ON пациенты.UserID = пользователи.User_ID SET пациенты.`Name` = '{textBox9.Text}',пациенты.`Adress` = '{textBox11.Text}',пациенты.`Contact Information` = '{textBox10.Text}' WHERE пользователи.User_ID = {user_id}";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;

            DatabaseHelper dbHelper1 = new DatabaseHelper();
            string script1 = $"SELECT пациенты.`Name`, пациенты.`Date of Birth`,пациенты.`Gender`, пациенты.`Adress`, пациенты.`Contact Information`FROM пациенты JOIN пользователи ON пациенты.UserID = пользователи.User_ID where пациенты.UserID= {user_id}";
            DataTable resultTable1 = dbHelper1.ExecuteQuery(script1);
            dataGridView1.DataSource = resultTable1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = "INSERT INTO `запись_на_приём` (`Patient_ID`, `Doctor_ID`, `Appointment_Date_and_Time`, `Status`, `Service_ID`) " +
            $"VALUES ({textBox12.Text}, '{textBox4.Text}', '{textBox5.Text}', 'запит', '{textBox6.Text}')";
            string script1 = "SELECT запись_на_приём.Doctor_ID, запись_на_приём.Appointment_Date_and_Time, врачи.Specialization, врачи.Doctor_Name FROM запись_на_приём join врачи on запись_на_приём.Doctor_ID = врачи.Doctors_ID";
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            DataTable resultTable1 = dbHelper.ExecuteQuery(script1);
            dataGridView2.DataSource = resultTable;
            dataGridView1.DataSource = resultTable1;
            
        }
    }
}
