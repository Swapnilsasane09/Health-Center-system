using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using LoginFormExample;

namespace Health_Care_Management_System
{
    public partial class Form12 : Form
    {
        private string connectionString = "Data Source=SWAPNIL\\SQLEXPRESS;Initial Catalog=healthcare;Integrated Security=True";
        bool availability = false;

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {


        }


        private void button1_Click(object sender, EventArgs e)
        {

            string insertquery = @"insert into registrationtb(fname,lname,email,password)
   values('"
             + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "','" + textBox4.Text + "');";

            try
            {
                if (availability)
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = connectionString;
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connection;
                        cmd.CommandText = insertquery;
                        connection.Open();
                        int numberofrowsaffected = cmd.ExecuteNonQuery();
                        if (numberofrowsaffected > 0)
                        {
                            MessageBox.Show("Data Inserted Successfully");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";

                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("Error: Something went wrong! Data not Inserted.");
                        }
                        connection.Close();
                    }
                }
                else
                {
                    if (availability == false)
                    { MessageBox.Show("email is not available"); }
                    else
                    { MessageBox.Show("Password and conform password are not matched."); }
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select email from registrationtb where email like '" + textBox3.Text + "';";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show(textBox3.Text + " already exist please select another email");
                    textBox3.Text = "";
                }
                else
                {
                    MessageBox.Show(textBox3.Text + " Valid username");
                    availability = true;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1= new Form1();
            form1.Show();
            this.Hide();
        }
    }
          
    
         }


           
    

    



