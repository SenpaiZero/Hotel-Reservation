using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelReservation
{
    public partial class Register : Form
    {
        SqlConnection cn;
        public Register()
        {
            InitializeComponent();
        }
        private void Register_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            this.Hide();
            menu.ShowDialog();
            cn.Close();
            this.Close();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {

            if (Password2.Text != string.Empty || Password.Text != string.Empty || Username.Text != string.Empty)
            {
                if (Password.Text == Password2.Text)
                {
                    using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\staffLoginDB.mdf;Integrated Security=True"))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("select * from staffLogin where username='" + Username.Text + "'", cn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dr.Close();
                            cmd = new SqlCommand("insert into staffLogin values(@username,@password)", cn);
                            cmd.Parameters.AddWithValue("username", Username.Text);
                            cmd.Parameters.AddWithValue("password", Password.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Your Account is created . Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter both password same ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
