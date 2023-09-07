using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Admin : User
    {
        public Admin(string id, string password, string fullName, string role) : base(id, password, fullName, role)
        {
        }

        private void ListAllDoctors()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |              All Doctors             |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();

            Console.WriteLine("All doctors registered to the DOTNET Hospital Management System");
            Console.WriteLine();
            Console.WriteLine("Name | Email Address | Phone | Address");
            Console.WriteLine("--------------------------------------");

            // Get all doctors
            string[] doctors = Directory.GetFiles("Doctors");
            if (doctors.Length > 0)
            {
                foreach (string doctor in doctors)
                {
                    string[] doctorInfo = File.ReadAllLines(doctor);
                    string[] doctorDetails = doctorInfo[0].Split(';');
                    Doctor d = new Doctor(doctorDetails[0], doctorDetails[1], doctorDetails[2], doctorDetails[3], doctorDetails[4], doctorDetails[5], "Doctor");
                    Console.WriteLine(d);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void CheckDoctorDetails()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |            Doctor Details            |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();

            Console.WriteLine("Please enter the ID of the doctor who's details you are checking. Or press n to return to menu");
            try
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.N:
                        Menu();
                        break;
                    case ConsoleKey.Enter:
                        throw new Exception("Doctor not found, press any key to return to menu");
                    default:
                        break;
                }

                string doctorID = Console.ReadLine();

                if (File.Exists($"Doctors\\{key.KeyChar.ToString() + doctorID}.txt"))
                {
                    string[] doctor = File.ReadAllLines($"Doctors\\{key.KeyChar.ToString() + doctorID}.txt");
                    string[] doctorInfo = doctor[0].Split(';');

                    Doctor d = new Doctor(doctorInfo[0], doctorInfo[1], doctorInfo[2], doctorInfo[3], doctorInfo[4], doctorInfo[5], "Doctor");

                    Console.WriteLine();
                    Console.WriteLine($"Details for {d.fullName}");
                    Console.WriteLine();
                    Console.WriteLine("Name | Email Address | Phone | Address");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine(d);

                    Console.ReadKey();
                    Menu();
                }
                else
                {
                    throw new Exception("Doctor not found, press any key to return to menu");
                }
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Doctor not found, press any key to return to menu":
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                }
            }
        }

        private void ListAllPatients()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |             All Patients             |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();

            Console.WriteLine("All patients registered to the DOTNET Hospital Management System");
            Console.WriteLine();
            Console.WriteLine("Name | Doctor | Email Address | Phone | Address");
            Console.WriteLine("-----------------------------------------------");

            // Get all patients
            string[] patients = Directory.GetFiles("Patients");
            if (patients.Length > 0)
            {
                foreach (string patient in patients)
                {
                    string[] patientInfo = File.ReadAllLines(patient);
                    string[] patientDetails = patientInfo[0].Split(';');
                    Patient p = new Patient(patientDetails[0], patientDetails[1], patientDetails[2], patientDetails[3], patientDetails[4], patientDetails[5], "Doctor");
                    Console.WriteLine(p);
                }
            }
            Console.ReadKey();
            Menu();

        }

        private void CheckPatientDetails()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |            Patient Details           |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();

            Console.WriteLine("Please enter the ID of the patient who's details you are checking. Or press n to return to menu");
            try
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.N:
                        Menu();
                        break;
                    case ConsoleKey.Enter:
                        throw new Exception("Patient not found, press any key to return to menu");
                    default:
                        break;
                }

                string patientID = Console.ReadLine();

                if (File.Exists($"Patients\\{key.KeyChar.ToString() + patientID}.txt"))
                {
                    string[] patient = File.ReadAllLines($"Patients\\{key.KeyChar.ToString() + patientID}.txt");
                    string[] patientInfo = patient[0].Split(';');

                    Patient p = new Patient(patientInfo[0], patientInfo[1], patientInfo[2], patientInfo[3], patientInfo[4], patientInfo[5], "Patient");

                    Console.WriteLine();
                    Console.WriteLine($"Details for {p.fullName}");
                    Console.WriteLine();
                    Console.WriteLine("Patient | Doctor | Email Address | Phone | Address");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine(p);

                    Console.ReadKey();
                    Menu();
                }
                else
                {
                    throw new Exception("Patient not found, press any key to return to menu");
                }
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Patient not found, press any key to return to menu":
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                }
            }
        }

        private void AddDoctor()
        {

        }

        private void AddPatient()
        {

        }

        public override void Menu()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |          Administrator Menu          |");
            Console.WriteLine(" |______________________________________|");

            Console.WriteLine();
            Console.WriteLine($"Welcome to DOTNET Hospital Managment System {fullName}");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }

            try
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                switch (info.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ListAllDoctors();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        CheckDoctorDetails();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ListAllPatients();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        CheckPatientDetails();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        AddDoctor();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        AddPatient();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.Clear();
                        Program.Main();
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        Environment.Exit(0);
                        break;
                    default:
                        throw new Exception($"Invalid option, please choose from 1-{options.Length}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Menu();
            }
        }
    }
}
