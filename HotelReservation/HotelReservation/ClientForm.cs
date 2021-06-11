using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HotelReservation
{
    public partial class ClientForm : Form
    {
        SqlConnection cn;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            // TODO: This line of code loads data into the 'hotelDataSet1.Client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.hotelDataSet1.Client);

        }

        private void Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            this.Close();
        }

        private void poisonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void poisonDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = poisonDataGridView1.Rows[e.RowIndex];
                ClientIDtxt.Text = row.Cells[0].Value.ToString();
                ClientName.Text = row.Cells[1].Value.ToString();
                ClientNumber.Text = row.Cells[2].Value.ToString();
            }
        }
        void reloadData()
        {
            this.clientTableAdapter.Fill(this.hotelDataSet1.Client);
        }
        private void ADD_Click(object sender, EventArgs e)
        {
            if (ClientIDtxt.Text != null || ClientName.Text != null || ClientNumber.Text != null )
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Client where Id='" + ClientIDtxt.Text + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("ID Already exist please try another ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("insert into Client values(@Id, @ClientName, @ClientPhoneNumber)", cn);
                        cmd.Parameters.AddWithValue("Id", ClientIDtxt.Text);
                        cmd.Parameters.AddWithValue("ClientName", ClientName.Text);
                        cmd.Parameters.AddWithValue("ClientPhoneNumber", ClientNumber.Text);
                        cmd.ExecuteNonQuery();
                        reloadData();
                        MessageBox.Show("New Client Has Been Added.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DELETE_Click(object sender, EventArgs e)
        {

            if (ClientIDtxt.Text != null || ClientName.Text != null || ClientNumber.Text != null)
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Client where Id ='" + ClientIDtxt.Text + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("delete from Client where Id='" + ClientIDtxt.Text + "'", cn);
                        cmd.ExecuteNonQuery();
                        reloadData();
                        MessageBox.Show("Client Has Been Deleted.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("Invalid ID. Please try another ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EDIT_Click(object sender, EventArgs e)
        {
            if (ClientIDtxt.Text != null || ClientName.Text != null || ClientNumber.Text != null)
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Client where Id ='" + ClientIDtxt.Text + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("update Client set ClientName = '" + ClientName.Text + "', ClientPhoneNumber = '" + ClientNumber.Text + "' where Id ='" + ClientIDtxt.Text + "'", cn);
                        cmd.ExecuteNonQuery();
                        reloadData();
                        MessageBox.Show("Client Has Been Updated.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("Invalid ID. Please try another ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientNumber_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ClientNumber.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                ClientNumber.Text = ClientNumber.Text.Remove(ClientNumber.Text.Length - 1);
            }
        }

        private void clientBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ClientIDtxt_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(ClientIDtxt.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                ClientIDtxt.Text = ClientIDtxt.Text.Remove(ClientIDtxt.Text.Length - 1);
            }
        }

        private void nightForm1_Click(object sender, EventArgs e)
        {

        }
    }
}
