using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HotelReservation
{
    public partial class RoomFOrm : Form
    {
        SqlConnection cn;
        public RoomFOrm()
        {
            InitializeComponent();
        }

        private void RoomFOrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotelDataSet10.Room' table. You can move, or remove it, as needed.
            this.roomTableAdapter2.Fill(this.hotelDataSet10.Room);
            // TODO: This line of code loads data into the 'hotelDataSet9.Reservation' table. You can move, or remove it, as needed.
            this.reservationTableAdapter.Fill(this.hotelDataSet9.Reservation);
            // TODO: This line of code loads data into the 'hotelDataSet8.Room' table. You can move, or remove it, as needed.
            this.roomTableAdapter1.Fill(this.hotelDataSet8.Room);
            // TODO: This line of code loads data into the 'hotelDataSet7.Room' table. You can move, or remove it, as needed.
            this.roomTableAdapter.Fill(this.hotelDataSet7.Room);

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu menu = new MainMenu();
            menu.ShowDialog();
            this.Close();
        }

        private void EDIT_Click(object sender, EventArgs e)
        {

            if (AvailDrop.Text != null || RoomDrop.Text != null)
            {
                using (cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Hotel.mdf;Integrated Security=True"))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Room where RoomNumber ='" + RoomDrop + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("update Room set isOpen = '" + AvailDrop.SelectedValue.ToString() +
                            "' where Room ='" + RoomDrop.SelectedValue.ToString() + "'", cn);
                        cmd.ExecuteNonQuery();
                        reloadData();
                        MessageBox.Show("Room Has Been Updated.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                RoomDrop.Text = row.Cells[0].Value.ToString();
                AvailDrop.Text = row.Cells[1].Value.ToString();
            }
        }
    void reloadData()
    {
        // TODO: This line of code loads data into the 'hotelDataSet10.Room' table. You can move, or remove it, as needed.
        this.roomTableAdapter2.Fill(this.hotelDataSet10.Room);
        // TODO: This line of code loads data into the 'hotelDataSet9.Reservation' table. You can move, or remove it, as needed.
        this.reservationTableAdapter.Fill(this.hotelDataSet9.Reservation);
        // TODO: This line of code loads data into the 'hotelDataSet8.Room' table. You can move, or remove it, as needed.
        this.roomTableAdapter1.Fill(this.hotelDataSet8.Room);
        // TODO: This line of code loads data into the 'hotelDataSet7.Room' table. You can move, or remove it, as needed.
        this.roomTableAdapter.Fill(this.hotelDataSet7.Room);
    }
    }
}
