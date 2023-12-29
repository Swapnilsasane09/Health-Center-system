using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Health_Care_Management_System
{
    public partial class Form6 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form6()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insertQuery = @"INSERT INTO Testtb1 (TestId, PatientId, Testname, Testdate, Testresult) 
                                   VALUES 
                                   (@TestId, @PatientId, @TestName, @TestDate, @TestResult)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    // Use parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@TestId", textBox1.Text);
                    cmd.Parameters.AddWithValue("@PatientId", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@TestName", textBox3.Text);
                    cmd.Parameters.AddWithValue("@TestDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@TestResult", textBox5.Text);

                    try
                    {
                        connection.Open();
                        int numberOfRowsAffected = cmd.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            MessageBox.Show("Data Inserted Successfully");

                            textBox1.Text = "";
                            comboBox1.Text = "";
                            textBox3.Text = "";
                            dateTimePicker1.Value = DateTime.Now;
                            textBox5.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Error: Something went wrong! Data not Inserted.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from Testtb1", sqlcon);
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

                    // Extract TestId from the selected row
                    string testIdToDelete = selectedRow.Cells["TestId"].Value.ToString();

                    // Execute the DELETE query
                    string deleteQuery = "DELETE FROM Testtb1 WHERE TestId = @TestId";

                    using (SqlConnection connection = new SqlConnection(con))
                    {
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@TestId", testIdToDelete);

                            try
                            {
                                connection.Open();
                                int numberOfRowsAffected = cmd.ExecuteNonQuery();

                                if (numberOfRowsAffected > 0)
                                {
                                    MessageBox.Show("Data Deleted Successfully");

                                    // Refresh the DataGridView after deletion
                                    button2_Click(sender, e);
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
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
    }

