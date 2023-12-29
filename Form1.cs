using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Health_Care_Management_System;

namespace LoginFormExample
{
    public partial class Form1 : Form
    {
        public static string connectionString = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form1()
        {
            InitializeComponent(); // Assuming you have a method to initialize components
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string checkquery = "SELECT email, password FROM registrationtb WHERE email=@email AND password=@password";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(checkquery, connection))
                    {
                        // Use parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@email", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", textBox2.Text);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            this.Hide();
                            Form2 form2 = new Form2();
                            form2.Show();
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct username or password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event if needed
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form12 = new Form12();
            form12.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Handle form load event if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
             string checkquery = "SELECT email, password FROM registrationtb WHERE email=@email AND password=@password";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(checkquery, connection))
                    {
                        // Use parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@email", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", textBox2.Text);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            this.Hide();
                            Form2 form2 = new Form2();
                            form2.Show();
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct username or password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        }
    }

