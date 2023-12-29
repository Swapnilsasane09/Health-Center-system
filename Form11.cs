using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Health_Care_Management_System
{
    public partial class Form11 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form11()
        {
            InitializeComponent();

            // Call a method to populate the patient IDs in the ComboBox
            PopulatePatientIds();

            // Subscribe to the SelectedIndexChanged event for comboBox1
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }

        // Method to populate patient IDs in the ComboBox
        private void PopulatePatientIds()
        {
            string patientIdQuery = "SELECT PatientId FROM tb2_patient";

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand(patientIdQuery, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add patient IDs to the ComboBox
                                comboBox1.Items.Add(reader["PatientId"].ToString());
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure the ComboBox has a selected item
            if (comboBox1.SelectedItem != null)
            {
                // Fetch data based on the selected patient ID
                string selectedPatientId = comboBox1.SelectedItem.ToString();

                string selectQuery = @"SELECT p.patientname, d.DiagnosisTest, d.Testresult, A.AppointmentDate, Dt.Doctorname
                               FROM tb2_patient p
                               INNER JOIN tb4_diagnosis d ON d.PatientId = p.PatientId
                               INNER JOIN tb5_Appointment A ON A.PatientId = d.PatientId
                               INNER JOIN tb1_Doctor Dt ON Dt.DoctorId = A.DoctorId
                               WHERE p.PatientId = @PatientId";

                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@PatientId", selectedPatientId);

                        try
                        {
                            connection.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                dataGridView1.Rows.Clear();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string patientName = reader["patientname"].ToString();
                                        string diagnosisTest = reader["DiagnosisTest"].ToString();
                                        string testResult = reader["Testresult"].ToString();
                                        string appointmentDate = reader["AppointmentDate"].ToString();
                                        string doctorName = reader["Doctorname"].ToString();

                                        dataGridView1.Rows.Add(patientName, diagnosisTest, testResult, appointmentDate, doctorName);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No data found for the selected patient.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}