using Health_Care_Management_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Care_Management_System
{
    public partial class Form7 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
        public Form7()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged; // Subscribe to the selection change event
            PopulateComboBox();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insertQuery = @"INSERT INTO tb4_diagnosis( PatientId, Patientname, Symptoms, DiagnosisTest,Testresult) 
                           VALUES 
                           ( @PatientId, @Patientname, @Symptoms, @DiagnosisTest,@Testresult)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    // Use parameters to prevent SQL injection
                  //  cmd.Parameters.AddWithValue("@DiagnosisId", textBox1.Text);
                    cmd.Parameters.AddWithValue("@PatientId", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Patientname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Symptoms", textBox4.Text);
                    cmd.Parameters.AddWithValue("@DiagnosisTest", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Testresult", comboBox3.Text);

                    try
                    {
                        connection.Open();
                        int numberOfRowsAffected = cmd.ExecuteNonQuery();

                        if (numberOfRowsAffected > 0)
                        {
                            MessageBox.Show("Data Inserted Successfully");


                            //textBox1.Text = "";
                            comboBox1.Text = "";
                            textBox2.Text = "";
                            textBox4.Text = "";
                            textBox1.Text = "";
                            comboBox3.Text = "";

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
           // textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox1.Text = "";
            comboBox3.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from tb4_diagnosis", sqlcon);
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

                // Extract DiagnosisId from the selected row
                string diagnosisIdToDelete = selectedRow.Cells["DiagnosisId"].Value.ToString();

                // Execute the DELETE query
                string deleteQuery = "DELETE FROM tb4_diagnosis WHERE DiagnosisId = @DiagnosisId";

                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@DiagnosisId", diagnosisIdToDelete);

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
                textBox2.Text = selectedRow["patientname"].ToString();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

    
