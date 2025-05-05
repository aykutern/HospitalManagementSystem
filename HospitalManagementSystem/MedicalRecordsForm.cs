using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class frmMedicalRecordsForm : Form
    {
        String frmRole = "Staff";
        public frmMedicalRecordsForm(string frmRole)
        {
            InitializeComponent();
            LoadUserstoComboBox();
            this.frmRole = frmRole;
        }

        private void frmMedicalRecordsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalDBDataSet.MedicalRecords' table. You can move, or remove it, as needed.
            this.medicalRecordsTableAdapter.Fill(this.hospitalDBDataSet.MedicalRecords);
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Patients' table. You can move, or remove it, as needed.
            this.patientsTableAdapter.Fill(this.hospitalDBDataSet.Patients);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (frmRole.ToLower() == "staff")
            {
                MessageBox.Show("You do not have permission to update medical records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (cbPatientID.SelectedValue.ToString() == "" || txtDiagnosis.Text == "" || txtTreatment.Text == "" || !dtpDate.Checked)  
                {
                    MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int patientID = int.Parse(cbPatientID.SelectedValue.ToString());
                    string diagnosis = txtDiagnosis.Text;
                    string treatment = txtTreatment.Text;
                    string prescriptions = txtPrescriptions.Text;
                    DateTime date = dtpDate.Value;     
                    if (dataGridView1.SelectedRows.Count == 0) return;
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    string query = $"UPDATE MedicalRecords SET Date = '{date}', PatientID = '{patientID}',Diagnosis='{diagnosis}', Treatment='{treatment}', Prescriptions='{prescriptions}' WHERE recordID='{id}'";
                    if (DatabaseHandler.ExecuteNonQuery(query))
                        LoadMedicalRecords();
                    MessageBox.Show("Medical record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            ClearFields();
        }

        private void LoadMedicalRecords()
        {
            string query = "SELECT * FROM MedicalRecords";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            dataGridView1.DataSource = dt;
        }
        private void ClearFields()
        {
            txtDiagnosis.Text = "";
            txtTreatment.Text = "";
            dtpDate.Value = DateTime.Now; // Set to current date
            txtPrescriptions.Text = "";
            
            LoadMedicalRecords();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDashboard dashboardForm = new frmDashboard(frmRole);
            dashboardForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDiagnosis.Text == "" || txtTreatment.Text == "" || cbPatientID.SelectedValue.ToString() == "" || !dtpDate.Checked)  
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //string patientID = cbPatientID.Text;
                int patientID = int.Parse(cbPatientID.SelectedValue.ToString());
                string diagnosis = txtDiagnosis.Text;
                string treatment = txtTreatment.Text;
                string prescription = txtPrescriptions.Text;
                DateTime date = dtpDate.Value;
                
                string query = $"INSERT INTO MedicalRecords (PatientID, Diagnosis, Treatment, Prescriptions, Date) VALUES ('{patientID}', '{diagnosis}', '{treatment}', '{prescription}', '{date}')";
                if (DatabaseHandler.ExecuteNonQuery(query))
                    LoadMedicalRecords();
                MessageBox.Show("Medical record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearFields();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                cbPatientID.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                txtDiagnosis.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                txtTreatment.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
                txtPrescriptions.Text = dataGridView1.SelectedRows[0].Cells[4].Value?.ToString();

                // Fix for CS0029: Convert the string value to DateTime using DateTime.Parse or DateTime.TryParse
                if (DateTime.TryParse(dataGridView1.SelectedRows[0].Cells[5].Value?.ToString(), out DateTime dateTime))
                {
                    dtpDate.Value = dateTime.Date; // Set the date part
                       
                }
            }
        }

        private void LoadUserstoComboBox()
        {
           
            string query = "SELECT PatientID, Name FROM Patients";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            cbPatientID.DataSource = dt;
            cbPatientID.DisplayMember = "Name";  // Display the PatientID
            cbPatientID.ValueMember = "PatientID";    // Use PatientID as the value



        }

        private void txtRecordID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
