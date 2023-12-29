using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Care_Management_System
{
    public partial class Form10 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form10()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged; // Subscribe to the selection change event
            PopulateComboBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string insertQuery = @"INSERT INTO tb7_report( PatientId, Doctorname, ReportDate, ReportType, ReportContent) 
                                           VALUES 
                                           ( @PatientId, @Doctorname, @ReportDate, @ReportType, @ReportContent)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        // Use parameters to prevent SQL injection
                        //cmd.Parameters.AddWithValue("@ReportId", textBox1.Text);
                        cmd.Parameters.AddWithValue("@PatientId", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Doctorname", textBox3.Text);
                        cmd.Parameters.AddWithValue("@ReportDate", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@ReportType", textBox5.Text);
                        cmd.Parameters.AddWithValue("@ReportContent", textBox6.Text);

                        int numberOfRowsAffected = cmd.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            MessageBox.Show("Data Inserted Successfully");

                           // textBox1.Text = "";
                            comboBox1.Text = "";
                            textBox3.Text = "";
                            dateTimePicker1.Value = DateTime.Now;
                            textBox5.Text = "";
                            textBox6.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Error: Something went wrong! Data not Inserted.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Check for FOREIGN KEY violation error
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("Error: The specified PatientId or DoctorId does not exist in the patienttb1 or doctortb1 table.");
                }
                else
                {
                    MessageBox.Show("Error: " + sqlEx.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Text = "";
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from tb7_report", sqlcon);
                DataTable Dt1 = new DataTable();
                sqlda.Fill(Dt1);

                // Set the DataGridView DataSource
                dataGridView1.DataSource = Dt1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {     // Ensure a row is selected
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Extract ReportId from the selected row
                    string reportIdToDelete = selectedRow.Cells["ReportId"].Value.ToString();

                    // Execute the DELETE query
                    string deleteQuery = "DELETE FROM tb7_report WHERE ReportId = @ReportId";

                    using (SqlConnection connection = new SqlConnection(con))
                    {
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ReportId", reportIdToDelete);

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void PopulateComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    string sqlQuery = "SELECT patientId, patientname FROM tb2_patient";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open();

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Bind the cow_id column to the ComboBox
                        comboBox1.DisplayMember = "patientId";
                        comboBox1.ValueMember = "patientId";
                        comboBox1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboBox1.SelectedItem;
                // textBox2.Text = selectedRow["patientname"].ToString();
            }
        }
    }
    }

