using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Health_Care_Management_System
{
    public partial class Form4 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form4()
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
            string insertQuery = @"insert into tb2_patient( patientname, patientcontact, patientage, patientaddress, patientgender, bloodgroup, majordisease) 
                                    values 
                                    (@PatientName, @PatientContact, @PatientAge, @PatientAddress, @PatientGender, @BloodGroup, @MajorDisease)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand(insertQuery, connection);

                // Add parameters
               // cmd.Parameters.AddWithValue("@PatientId", textBox1.Text);
                cmd.Parameters.AddWithValue("@PatientName", textBox2.Text);
                cmd.Parameters.AddWithValue("@PatientContact", textBox3.Text);
                cmd.Parameters.AddWithValue("@PatientAge", textBox4.Text);
                cmd.Parameters.AddWithValue("@PatientAddress", textBox8.Text);
                cmd.Parameters.AddWithValue("@PatientGender", comboBox2.Text);
                cmd.Parameters.AddWithValue("@BloodGroup", comboBox1.Text);
                cmd.Parameters.AddWithValue("@MajorDisease", textBox7.Text);

                try
                {
                    connection.Open();
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();

                    if (numberOfRowsAffected > 0)
                    {
                        MessageBox.Show("Data Inserted Successfully");

                        // Clear the input fields
                       // textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox8.Text = "";
                        comboBox2.Text = "";
                        comboBox1.Text = "";
                        textBox7.Text = "";
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

        private void Form4_Load(object sender, EventArgs e)
        {
            // Any additional initialization code can be placed here
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Clear the input fields
           // textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox7.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from tb2_patient", sqlcon);
                DataTable Dt1 = new DataTable();
                sqlda.Fill(Dt1);

                // Set the DataGridView DataSource
                dataGridView1.DataSource = Dt1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
               // Ensure a row is selected
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Extract PatientId from the selected row
                    string patientIdToDelete = selectedRow.Cells["patientId"].Value.ToString();

                    // Execute the DELETE query
                    string deleteQuery = "DELETE FROM tb2_patient WHERE patientId = @PatientId";

                    using (SqlConnection connection = new SqlConnection(con))
                    {
                        SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                        cmd.Parameters.AddWithValue("@PatientId", patientIdToDelete);

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }
