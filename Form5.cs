using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Care_Management_System
{
    public partial class Form5 : Form
    {
        public string con = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";

        public Form5()
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
            string insertQuery = @"INSERT INTO tb3_Staff ( Staffname, contact, gender, age) 
                                   VALUES 
                                   ('" + textBox2.Text + "', '" + textBox3.Text + "','" + comboBox1.Text + "','" + textBox5.Text + "');";

            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = insertQuery;

                try
                {
                    connection.Open();
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();

                    if (numberOfRowsAffected > 0)
                    {
                        MessageBox.Show("Data Inserted Successfully");

                        //textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
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
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            comboBox1.Text = " ";
            textBox5.Text = " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();

                SqlDataAdapter sqlda = new SqlDataAdapter("select * from tb3_Staff", sqlcon);
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

                    // Extract StaffId from the selected row
                    string staffIdToDelete = selectedRow.Cells["StaffId"].Value.ToString();

                    // Execute the DELETE query
                    string deleteQuery = "DELETE FROM tb3_Staff WHERE StaffId = @StaffId";

                    using (SqlConnection connection = new SqlConnection(con))
                    {
                        SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                        cmd.Parameters.AddWithValue("@StaffId", staffIdToDelete);

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

