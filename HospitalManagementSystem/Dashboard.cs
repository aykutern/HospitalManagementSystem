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
    public partial class frmDashboard : Form
    {
        
        String frmRole;
        public frmDashboard( String role)
        {
            InitializeComponent();
            
            frmRole = role;

        }

        private void btnPatientManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPatientForm patientManagementForm = new frmPatientForm(frmRole);
            patientManagementForm.Show();
        }

        private void btnDoctorMangement_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDoctorForm doctorManagementForm = new frmDoctorForm(frmRole);
            doctorManagementForm.Show();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAppointmentForm appointmentForm = new frmAppointmentForm();
            appointmentForm.Show();
        }

        private void btnMedicalRecords_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMedicalRecordsForm medicalRecordForm = new frmMedicalRecordsForm(frmRole);
            medicalRecordForm.Show();
        }
        //No Confirmation Yet
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Billing billing = new Billing(frmRole);
            billing.Show();
        }
    }
}
