using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class Billing : Form
    {
        String frmRole;
        public Billing(string frmRole)
        {
            InitializeComponent();
            LoadUserstoComboBox();
            this.AcceptButton = btnAdd;
            this.frmRole = frmRole;
        }
        private void LoadUserstoComboBox()
        {
            string query = "SELECT PatientID, Name FROM Patients";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            cbPatient.DataSource = dt;
            cbPatient.DisplayMember = "Name";
            cbPatient.ValueMember = "PatientID"; 


        }

        private void Billing_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Patients' table. You can move, or remove it, as needed.
            this.patientsTableAdapter.Fill(this.hospitalDBDataSet.Patients);
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Billing' table. You can move, or remove it, as needed.
            this.billingTableAdapter.Fill(this.hospitalDBDataSet.Billing);

        }
        private void LoadBillingData()
        {
            string query = "SELECT * FROM Billing";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            dataGridView1.DataSource = dt;
        }
        private void ClearFields()
        {
            txtAmount.Text = "";
            txtDescription.Text = "";
            dateTimePicker1.Checked = false;
            LoadBillingData();
            LoadUserstoComboBox();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtAmount.Text == "" || txtDescription.Text == "" || !dateTimePicker1.Checked || cbPatient.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int patientID = int.Parse(cbPatient.SelectedValue.ToString());
                string description = txtDescription.Text;
                int amount = int.Parse(txtAmount.Text);
                DateTime date = dateTimePicker1.Value;
                string query = $"INSERT INTO Billing (PatientID, Amount, Date, Description) VALUES ('{patientID}', '{amount}', '{date}', '{description}')";
                if (DatabaseHandler.ExecuteNonQuery(query))
                    LoadBillingData();
                MessageBox.Show("Billing record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearFields();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to update billing records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtAmount.Text == "" || txtDescription.Text == "" || !dateTimePicker1.Checked || cbPatient.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int patientID = int.Parse(cbPatient.SelectedValue.ToString());
                    string description = txtDescription.Text;
                    int amount = int.Parse(txtAmount.Text);
                    DateTime date = dateTimePicker1.Value;
                    if (dataGridView1.SelectedRows.Count == 0) return;
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    string query = $"UPDATE Billing SET PatientID = '{patientID}', Amount = '{amount}', Date = '{date}', Description = '{description}' WHERE BillID = '{id}'";
                    if (DatabaseHandler.ExecuteNonQuery(query))
                        LoadBillingData();
                    MessageBox.Show("Billing record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to delete billing records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataGridView1.SelectedRows.Count == 0) return;
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string query = $"DELETE FROM Billing WHERE BillID = '{id}'";
                if (DatabaseHandler.ExecuteNonQuery(query))
                    LoadBillingData();
                MessageBox.Show("Billing record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDashboard dashboardForm = new frmDashboard(frmRole);
            dashboardForm.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtAmount.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value);
                txtDescription.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                cbPatient.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.patientsTableAdapter.FillBy(this.hospitalDBDataSet.Patients);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
