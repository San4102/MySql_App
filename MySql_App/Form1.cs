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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
       


        private void button1_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
           DatabaseHelper dbHelper = new DatabaseHelper();
            string script = "SELECT * FROM "+selectedValue;
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string script1 = textBox1.Text;
            string selectedValue = comboBox1.SelectedItem.ToString();
            DatabaseHelper dbHelper = new DatabaseHelper();
            string script = script1;
            DataTable resultTable = dbHelper.ExecuteQuery(script);
            dataGridView1.DataSource = resultTable;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            string connString = "Server=localhost;port=3306;Database=polyclinic_reception;username=monty;password=some_pass";
            MySqlConnection connection = new MySqlConnection(connString);
            string tableName = comboBox1.SelectedItem.ToString();

            // Создайте команду SELECT для выбранной таблицы
            string selectQuery = $"SELECT * FROM {tableName}";
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            // Создайте адаптер данных и настройте команду UPDATE
            MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM {tableName}", connection);
            MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(adapter);

            // Сохраните изменения в базе данных
            adapter.Update(dataTable);
            connection.Close();
        }


        //for moving 
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Visible = true;
                button2.Visible = true;
            }
            else
            {
                textBox1.Visible=false;
                button2.Visible=false;
            }
        }
    }
    public class DatabaseHelper
    {
        private static string connString = "Server=localhost;port=3306;Database=polyclinic_reception;username=monty;password=some_pass";

        public DataTable ExecuteQuery(string query)
        {
            DataTable table = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(table);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return table;
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------
    public class UserRegistrationHelper
    {
        private string connString;

        public UserRegistrationHelper(string connectionString)
        {
            connString = connectionString;
        }

        public void RegisterUser(string username, string password, string role)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO пользователи (Username, Password, Role)" + $"VALUES('{username}','{password}','{role}'); ", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Вы успешно зарегистрированы!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message);
            }
        }
    }
}
