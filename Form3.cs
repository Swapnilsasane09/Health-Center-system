using Health_Care_Management_System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class Form3 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Your existing code for inserting data into the database
        }




        private void button2_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            // Add any initialization tasks or code you want to run when the form loads
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            // Validate input before proceeding
            if ( string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill in all the fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string insertQuery = @"INSERT INTO tb1_Doctor (Doctorname, contact, Experience, Gender, age) 
                           VALUES 
                           (@DoctorName, @Contact, @Experience, @Gender, @Age)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = insertQuery;

                // Add parameters
               // cmd.Parameters.AddWithValue("@DoctorId", textBox1.Text);
                cmd.Parameters.AddWithValue("@DoctorName", textBox2.Text);
                cmd.Parameters.AddWithValue("@Contact", textBox3.Text);
                cmd.Parameters.AddWithValue("@Experience", textBox4.Text);
                cmd.Parameters.AddWithValue("@Gender", comboBox1.Text);
                cmd.Parameters.AddWithValue("@Age", textBox5.Text);

                try
                {
                    connection.Open();
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();

                    if (numberOfRowsAffected > 0)
                    {
                        MessageBox.Show("Data Inserted Successfully");

                        // Clear the textboxes and combo box
                      //  textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = "";
                        textBox5.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Error: Something went wrong! Data not Inserted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "\nQuery: " + cmd.CommandText);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs ex)
                {


                }

        private void button4_Click(object sender, EventArgs e)
        {
           // textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";           
            comboBox1.Text = "";
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from tb1_Doctor", sqlcon);
                DataTable Dt1 = new DataTable();
                sqlda.Fill(Dt1);

                // Set the DataGridView DataSource
                dataGridView1.DataSource = Dt1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          {
                // Ensure a row is selected
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Extract DoctorId from the selected row
                    string doctorIdToDelete = selectedRow.Cells["DoctorId"].Value.ToString();

                    // Execute the DELETE query
                    string deleteQuery = "DELETE FROM tb1_Doctor WHERE DoctorId = @DoctorId";

                    using (SqlConnection connection = new SqlConnection(con))
                    {
                        SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                        cmd.Parameters.AddWithValue("@DoctorId", doctorIdToDelete);

                        try
                        {
                            connection.Open();
                            int numberOfRowsAffected = cmd.ExecuteNonQuery();

                            if (numberOfRowsAffected > 0)
                            {
                                MessageBox.Show("Data Deleted Successfully");

                                // Refresh the DataGridView after deletion
                                button5_Click_1(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Error: Something went wrong! Data not Deleted.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message + "\nQuery: " + cmd.CommandText);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
    }
        

    

