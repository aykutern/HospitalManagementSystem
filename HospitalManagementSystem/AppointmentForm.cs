using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class frmAppointmentForm : Form
    {
        String frmRole = "Staff";
        public frmAppointmentForm()
        {
            InitializeComponent();
            LoadPatientandDoctorComboBox();
            this.AcceptButton = btnAdd;
            this.frmRole = frmRole;
        }

        private void frmAppointmentForm_Load(object sender, EventArgs e)
        {
            //
            // : This line of code loads data into the 'hospitalDBDataSet.Patients' table. You can move, or remove it, as needed.
            this.patientsTableAdapter.Fill(this.hospitalDBDataSet.Patients);
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Doctors' table. You can move, or remove it, as needed.
            this.doctorsTableAdapter.Fill(this.hospitalDBDataSet.Doctors);
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.hospitalDBDataSet.Appointments);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbPatient.SelectedValue.ToString() == "" || cbDoctor.SelectedValue.ToString() == "" || !dtpDate.Checked || !dtpTime.Checked)
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int patientID = int.Parse(cbPatient.SelectedValue.ToString());
                int doctorID = int.Parse(cbDoctor.SelectedValue.ToString());
                DateTime date = dtpDate.Value;
                TimeSpan time = dtpTime.Value.TimeOfDay;
                string query = $"INSERT INTO Appointments (PatientID, DoctorID, Date, Time) VALUES ('{patientID}', '{doctorID}', '{date}', '{time}')";
                if (DatabaseHandler.ExecuteNonQuery(query))
                {
                    MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            ClearFields();
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cbPatient.SelectedValue.ToString() == "" || cbDoctor.SelectedValue.ToString() == "" || !dtpDate.Checked || !dtpTime.Checked)
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int patientID = int.Parse(cbPatient.SelectedValue.ToString());
                int doctorID = int.Parse(cbDoctor.SelectedValue.ToString());
                DateTime date = dtpDate.Value;
                TimeSpan time = dtpTime.Value.TimeOfDay;
                string notes = txtNotes.Text;
                if (dataGridView1.SelectedRows.Count == 0) return;
                int appointmentID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                string query = $"Update Appointments Set PatientID = '{patientID}', DoctorID = '{doctorID}', Notes = '{notes}', Date = '{date}', Time = '{time}' WHERE AppointmentID = '{appointmentID}'";

                if (DatabaseHandler.ExecuteNonQuery(query))
                {
                    MessageBox.Show("Appointment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }

                /*if (dataGridView1.SelectedRows.Count == 0) return;
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string query = $"UPDATE MedicalRecords SET PatientID = '{patientID}',Diagnosis='{diagnosis}', Treatment='{treatment}', Prescriptions='{prescriptions}' WHERE recordID='{id}'";
                if (DatabaseHandler.ExecuteNonQuery(query))
                    LoadMedicalRecords();*/
                else
                {
                    MessageBox.Show("Failed to update appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
                if (dataGridView1.SelectedRows.Count == 0) return;
                int appointmentID = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                string query = $"DELETE FROM Appointments WHERE AppointmentID = '{appointmentID}'";
                if (DatabaseHandler.ExecuteNonQuery(query))
                {

                    MessageBox.Show("Appointment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void LoadPatientandDoctorComboBox()

        {
            string query = "SELECT PatientID, Name FROM Patients";
            string query2 = "SELECT DoctorID, Name FROM Doctors";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            DataTable dt2 = DatabaseHandler.ExecuteSelect(query2);
            cbPatient.DataSource = dt;
            cbDoctor.DataSource = dt2;

            /*string connectionString = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT PatientID FROM Patients", connection);
                SqlCommand cmd2 = new SqlCommand("SELECT DoctorID FROM Doctors", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                adapter.Fill(dt);
                adapter2.Fill(dt2);
                cbPatient.DataSource = dt;
                cbPatient.DisplayMember = "PatientID";
                cbPatient.ValueMember = "PatientID";
                cbDoctor.DataSource = dt2;
                cbDoctor.DisplayMember = "DoctorID";
                cbDoctor.ValueMember = "DoctorID";



            }*/
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                cbPatient.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                cbDoctor.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                txtNotes.Text = dataGridView1.SelectedRows[0].Cells[5].Value?.ToString();

                // Fix for CS0029: Convert the string value to DateTime using DateTime.Parse or DateTime.TryParse
                if (DateTime.TryParse(dataGridView1.SelectedRows[0].Cells[3].Value?.ToString(), out DateTime dateTime))
                {
                    dtpDate.Value = dateTime;
                }


                //todo  /looking for an easy way
                // Fix for CS0029: Convert TimeSpan to DateTime by combining it with a base date
                if (TimeSpan.TryParse(dataGridView1.SelectedRows[0].Cells[4].Value?.ToString(), out TimeSpan times))
                {
                    dtpTime.Value = DateTime.Today.Add(times); // Combine TimeSpan with today's date
                }
            }
        }

        private void ClearFields()
        {
            dtpDate.Value = DateTime.Now;
            dtpTime.Value = DateTime.Now;
            LoadPatientandDoctorComboBox();
            LoadAppointments();

        }
        private void LoadAppointments()
        {
            string query = "SELECT * FROM Appointments";
            DataTable dt = DatabaseHandler.ExecuteSelect(query);
            dataGridView1.DataSource = dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDashboard dashboard = new frmDashboard(frmRole);
            dashboard.Show();
        }
    }
}
