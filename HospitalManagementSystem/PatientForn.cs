using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class frmPatientForm : Form
    {
        
        String frmRole = "Staff";
        public frmPatientForm(String frmRole)
        {
            InitializeComponent();
            this.AcceptButton = btnAdd;
            this.frmRole = frmRole;
            LoadGenderComboBox();

        }

        private void frmPatientForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Patients' table. You can move, or remove it, as needed.
            this.patientsTableAdapter.Fill(this.hospitalDBDataSet.Patients);

        }
        private void LoadPatients()
        {
            string query = "SELECT * FROM Patients";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            dataGridView1.DataSource = dt;
            LoadGenderComboBox();
        }
        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAge.Text = "";
            txtAddress.Text = "";
            //LoadGenderComboBox();
            cbGender.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || cbGender.SelectedIndex == -1 || txtEmail.Text == "" || txtContact.Text == "" || txtAge.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string name = txtName.Text;
                string gender = cbGender.SelectedValue.ToString();
                string age = txtAge.Text;
                string contact = txtContact.Text;
                string address = txtAddress.Text;
                string email = txtEmail.Text;
                
                string query = $"INSERT INTO Patients (Name, Gender, Age, Contact, Address, Email) VALUES ('{name}','{gender}','{age}','{contact}','{address}','{email}')";
                if (DatabaseHandler.ExecuteNonQuery(query))
                {
                    LoadPatients();
                    MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add patient. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to update patient records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtName.Text == "" || cbGender.SelectedIndex == -1 || txtEmail.Text == "" || txtContact.Text == "" || txtAge.Text == "" || txtAddress.Text == "")
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string name = txtName.Text;
                    string gender = cbGender.SelectedValue.ToString();
                    string age = txtAge.Text;
                    string contact = txtContact.Text;
                    string address = txtAddress.Text;
                    string email = txtEmail.Text;
                    if (dataGridView1.SelectedRows.Count == 0) return;

                    int patientId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    string query = $"UPDATE Patients SET Name = '{name}', Gender = '{gender}', Age = '{age}', Contact = '{contact}', Address = '{address}', Email = '{email}' WHERE PatientID = '{patientId}'";
                    if (DatabaseHandler.ExecuteNonQuery(query))
                    {
                        LoadPatients();
                    }
                MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to delete patient records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataGridView1.SelectedRows.Count == 0) return;
                int patientId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string query = $"DELETE FROM Patients WHERE PatientID = {patientId}";
                try
                {
                    if (DatabaseHandler.ExecuteNonQuery(query))
                    {
                        LoadPatients();
                        MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete patient. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete patient. Please check Appointment and Medical records and try again later!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ClearFields();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                cbGender.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtAge.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtContact.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtAddress.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtEmail.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDashboard dashboard = new frmDashboard(frmRole);
            dashboard.Show();
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LoadGenderComboBox()
        {
            Dictionary<string, string> genderDict = new Dictionary<string, string>()
             {
                { "E", "Erkek" },
                { "K", "Kadın" }
              };

            cbGender.DataSource = new BindingSource(genderDict, null);
            cbGender.DisplayMember = "Value"; // Kullanıcıya gösterilen
            cbGender.ValueMember = "Key";     // Veritabanına kaydedilecek olan
        }
    }
}
