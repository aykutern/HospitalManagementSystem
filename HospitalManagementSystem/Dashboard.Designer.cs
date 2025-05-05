namespace HospitalManagementSystem
{
    partial class frmDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPatientManagement = new System.Windows.Forms.Button();
            this.btnDoctorMangement = new System.Windows.Forms.Button();
            this.btnAppointments = new System.Windows.Forms.Button();
            this.btnMedicalRecords = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPatientManagement
            // 
            this.btnPatientManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPatientManagement.Location = new System.Drawing.Point(12, 12);
            this.btnPatientManagement.Name = "btnPatientManagement";
            this.btnPatientManagement.Size = new System.Drawing.Size(150, 40);
            this.btnPatientManagement.TabIndex = 1;
            this.btnPatientManagement.Text = "PatientManagement";
            this.btnPatientManagement.UseVisualStyleBackColor = true;
            this.btnPatientManagement.Click += new System.EventHandler(this.btnPatientManagement_Click);
            // 
            // btnDoctorMangement
            // 
            this.btnDoctorMangement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDoctorMangement.Location = new System.Drawing.Point(168, 12);
            this.btnDoctorMangement.Name = "btnDoctorMangement";
            this.btnDoctorMangement.Size = new System.Drawing.Size(150, 40);
            this.btnDoctorMangement.TabIndex = 2;
            this.btnDoctorMangement.Text = "DoctorManagement";
            this.btnDoctorMangement.UseVisualStyleBackColor = true;
            this.btnDoctorMangement.Click += new System.EventHandler(this.btnDoctorMangement_Click);
            // 
            // btnAppointments
            // 
            this.btnAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAppointments.Location = new System.Drawing.Point(325, 12);
            this.btnAppointments.Name = "btnAppointments";
            this.btnAppointments.Size = new System.Drawing.Size(150, 40);
            this.btnAppointments.TabIndex = 3;
            this.btnAppointments.Text = "Appointments";
            this.btnAppointments.UseVisualStyleBackColor = true;
            this.btnAppointments.Click += new System.EventHandler(this.btnAppointments_Click);
            // 
            // btnMedicalRecords
            // 
            this.btnMedicalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMedicalRecords.Location = new System.Drawing.Point(168, 58);
            this.btnMedicalRecords.Name = "btnMedicalRecords";
            this.btnMedicalRecords.Size = new System.Drawing.Size(150, 40);
            this.btnMedicalRecords.TabIndex = 4;
            this.btnMedicalRecords.Text = "MedicalRecords";
            this.btnMedicalRecords.UseVisualStyleBackColor = true;
            this.btnMedicalRecords.Click += new System.EventHandler(this.btnMedicalRecords_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLogout.Location = new System.Drawing.Point(12, 189);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 40);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnExit.Location = new System.Drawing.Point(325, 189);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 40);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(168, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "Billing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 241);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnMedicalRecords);
            this.Controls.Add(this.btnAppointments);
            this.Controls.Add(this.btnDoctorMangement);
            this.Controls.Add(this.btnPatientManagement);
            this.Name = "frmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPatientManagement;
        private System.Windows.Forms.Button btnDoctorMangement;
        private System.Windows.Forms.Button btnAppointments;
        private System.Windows.Forms.Button btnMedicalRecords;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button1;
    }
}