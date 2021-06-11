using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HotelReservation
{
    public partial class ReservationForm : Form
    {
        SqlConnection cn;
        int id;
        public ReservationForm()
        {
            InitializeComponent();
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotelDataSet6.Reservation' table. You can move, or remove it, as needed.
            this.reservationTableAdapter1.Fill(this.hotelDataSet6.Reservation);
            // TODO: This line of code loads data into the 'hotelDataSet5.Room' table. You can move, or remove it, as needed.
            this.roomTableAdapter.Fill(this.hotelDataSet5.Room);//
            // TODO: This line of code loads data into the 'hotelDataSet4.Client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.hotelDataSet4.Client);//
            // TODO: This line of code loads data into the 'hotelDataSet3.Reservation' table. You can move, or remove it, as needed.
            this.reservationTableAdapter.Fill(this.hotelDataSet3.Reservation);//

            this.MaximumSize = this.Size;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
        }

        void reloadData()
        {
            this.reservationTableAdapter1.Fill(this.hotelDataSet6.Reservation);
        }
        private void ADD_Click(object sender, EventArgs e)
        {
            if (ClientDrop.Text != null || RoomDrop.Text != null)
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Reservation where RoomNumber='" + RoomDrop.Text + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("Someone already reserved the room number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (DatePickerIN.Value < DatePickerOut.Value)
                        {
                            dr.Close();
                            cmd = new SqlCommand("insert into Reservation values(@ClientName, @RoomNumber, @DateIn, @dateOut)", cn);
                            cmd.Parameters.AddWithValue("ClientName", ClientDrop.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("RoomNumber", RoomDrop.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("DateIn", DatePickerIN.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("dateOut", DatePickerOut.Value.ToString("yyyy-MM-dd"));
                            cmd.ExecuteNonQuery();
                            reloadData();
                            MessageBox.Show("New Client Has Been Added.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Input a correct date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            this.Close();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            if (ClientDrop.Text != null || RoomDrop.Text != null)
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Reservation where RoomNumber ='" + RoomDrop.SelectedValue.ToString() + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("delete from Reservation where RoomNumber='" + RoomDrop.SelectedValue.ToString() + "'", cn);
                        cmd.ExecuteNonQuery();
                        reloadData();
                        MessageBox.Show("Reservation Has Been Deleted.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("Invalid Room Number. Please try another Room Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void poisonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = poisonDataGridView1.Rows[e.RowIndex];

                ClientDrop.Text = row.Cells[0].Value.ToString();
                RoomDrop.Text = row.Cells[3].Value.ToString();
                id = Convert.ToInt32(row.Cells[4].Value);
            }
        }

        private void EDIT_Click(object sender, EventArgs e)
        {

            if (ClientDrop.Text != null || RoomDrop.Text != null)
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Reservation where Id ='" + id + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("update Reservation set ClientName = '" + ClientDrop.SelectedValue.ToString() +
                            "', RoomNumber = '" + RoomDrop.SelectedValue.ToString() +
                            "', DateIn = '" + DatePickerIN.Value.ToString("yyyy-MM-dd") +
                            "', dateOut = '" + DatePickerOut.Value.ToString("yyyy-MM-dd") +
                            "' where Id ='" + id + "'", cn);
                        cmd.ExecuteNonQuery();
                        reloadData();
                        MessageBox.Show("Reservation Has Been Updated.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("Invalid Room Number. Please try another Room Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
