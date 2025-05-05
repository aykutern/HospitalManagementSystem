using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HospitalManagementSystem
{
    public partial class frmDoctorForm : Form
    {
        String frmRole = "Staff";
        public frmDoctorForm(string frmRole)
        {
            InitializeComponent();
            this.frmRole = frmRole;
            this.AcceptButton = btnAdd;
        }


        private void frmDoctorForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Doctors' table. You can move, or remove it, as needed.
            this.doctorsTableAdapter.Fill(this.hospitalDBDataSet.Doctors);

        }
        private void LoadDoctors()
        {
            string query = "SELECT * FROM Doctors";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            dataGridView1.DataSource = dt;
        }

        private void ClearFields()
        {
            txtSpecialization.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAvailableDays.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtSpecialization.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAvailableDays.Text == "")
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Email control for '@'.
                int atCount = 0;
                foreach (char c in txtEmail.Text)
                {
                    if (c == '@')
                        atCount++;
                }

                if (atCount != 1)
                {
                    MessageBox.Show("Invalid email address. Please ensure it contains exactly one '@' character.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                string name = txtName.Text;
                string specialization = txtSpecialization.Text;
                string email = txtEmail.Text;
                string contact = txtContact.Text;
                string availableDays = txtAvailableDays.Text;
                string query = $"INSERT INTO Doctors VALUES ('{name}','{specialization}','{contact}','{email}','{availableDays}')";

                if (DatabaseHandler.ExecuteNonQuery(query))
                    LoadDoctors();

                MessageBox.Show("Doctor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to edit employees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtName.Text == "" || txtSpecialization.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAvailableDays.Text == "")
                {
                    MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Email control: there should be only one '@' character.
                    int atCount = 0;
                    foreach (char c in txtEmail.Text)
                    {
                        if (c == '@')
                            atCount++;
                    }
                    if (atCount != 1)
                    {
                        MessageBox.Show("Invalid email address. Please ensure it contains exactly one '@' character.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string name = txtName.Text;
                    string specialization = txtSpecialization.Text;
                    string email = txtEmail.Text;
                    string contact = txtContact.Text;
                    string availableDays = txtAvailableDays.Text;

                    if (dataGridView1.SelectedRows.Count == 0) return;
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    // Email part was missing in the original code. Mistake fixed.
                    string query = $"UPDATE Doctors SET Name = '{name}', Specialization = '{specialization}', Contact = '{contact}', Email = '{email}', AvailableDays = '{availableDays}' WHERE Id = {id}";

                    if (DatabaseHandler.ExecuteNonQuery(query))
                    {
                        LoadDoctors();
                        MessageBox.Show("Doctor edited successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to delete employees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataGridView1.SelectedRows.Count == 0) return;
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string query = $"DELETE FROM Doctors WHERE DoctorID = {id}";
                try
                {
                    if (DatabaseHandler.ExecuteNonQuery(query))
                    {
                        LoadDoctors();
                        MessageBox.Show("Doctor deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete doctor. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete doctor. Please check Appointment records and try again later!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                ClearFields();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtSpecialization.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtContact.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtEmail.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtAvailableDays.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDashboard dashboard = new frmDashboard(frmRole);
            dashboard.Show();
        }

        private void txtAvailableDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
