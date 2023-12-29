using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Care_Management_System
{
    public partial class Form9 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form9()
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

                    string insertQuery = @"INSERT INTO tb6_payment( PatientId, Date, Amount, PaymentStatus) 
                                           VALUES 
                                           ( @PatientId, @Date, @Amount, @PaymentStatus)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        // Use parameters to prevent SQL injection
                       // cmd.Parameters.AddWithValue("@BillId", int.Parse(textBox1.Text)); // Assuming BillId is INT
                        cmd.Parameters.AddWithValue("@PatientId", int.Parse(comboBox1.Text)); // Assuming PatientId is INT
                        cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);

                        // Assuming Amount is DECIMAL(10, 2)
                        if (decimal.TryParse(textBox4.Text, out decimal amount))
                        {
                            cmd.Parameters.AddWithValue("@Amount", amount);
                        }
                        else
                        {
                            MessageBox.Show("Error: Invalid Amount. Please enter a valid decimal number.");
                            return;
                        }

                        cmd.Parameters.AddWithValue("@PaymentStatus", textBox5.Text);

                        int numberOfRowsAffected = cmd.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            MessageBox.Show("Data Inserted Successfully");

                           // textBox1.Text = "";
                            comboBox1.Text = "";
                            dateTimePicker1.Value = DateTime.Now;
                            textBox4.Text = "";
                            textBox5.Text = "";
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
                    MessageBox.Show("Error: The specified PatientId does not exist in the patienttb1 table.");
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
           // textBox1.Text = "";
            comboBox1.Text = "";
           dateTimePicker1.Text = "";
            textBox4.Text = "";
           textBox5.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from tb6_payment", sqlcon);
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

                    // Extract BillId from the selected row
                    int billIdToDelete;
                    if (int.TryParse(selectedRow.Cells["BillId"].Value.ToString(), out billIdToDelete))
                    {
                        // Execute the DELETE query
                        string deleteQuery = "DELETE FROM tb6_payment WHERE BillId = @BillId";

                        using (SqlConnection connection = new SqlConnection(con))
                        {
                            using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@BillId", billIdToDelete);

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
                        MessageBox.Show("Error: Invalid BillId format.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
