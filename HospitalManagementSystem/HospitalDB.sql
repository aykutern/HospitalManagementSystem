CREATE DATABASE [HospitalDB];

use HospitalDB;

Create Table Users (
	UserID INT Primary Key IDENTITY(1,1),
	Username NVARCHAR(100),
	Password NVARCHAR(100),
	Role NVARCHAR(100)
	);

Create Table Patients(
	PatientID INT Primary Key IDENTITY(1,1),
	Name NVARCHAR(100),
	Gender NVARCHAR(100),
	Age INT,
	Contact NVARCHAR(10),
	Address NVARCHAR(255),
	Email NVARCHAR(100)
);


Create Table Doctors(
	DoctorID INT Primary Key IDENTITY(1,1),
	Name NVARCHAR(100),
	Specialization NVARCHAR(100),
	Contact INT,
	Email NVARCHAR(100),
	AvailableDays INT,
);

CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT,
    DoctorID INT,
    Date DATE,
    Time TIME,
    Notes TEXT,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

CREATE TABLE MedicalRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT,
    Diagnosis TEXT,
    Treatment TEXT,
    Prescriptions TEXT,
    Date DATE,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
); 

create table Billing (
BillID INT Primary Key Identity(1,1),
PatientID INT,
Amount INT,
Date DATE,
Description TEXT,
FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);
select * from Patients

insert into Patients values ('Cihan Eren', 'Erkek', 39, 535, 'Ýstanbul', 'cihan@eren.com')

insert into MedicalRecords values (1,'asd', 'asd', 'asd', '2025-04-17');

select * from MedicalRecords

select * from Billing

select * from Users

INSERT INTO [dbo].[Users] ([Username],[Password],[Role]) VALUES ('admin', 'admin', 'admin'),('aykut', 'test', 'Staff')


