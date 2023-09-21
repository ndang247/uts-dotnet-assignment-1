namespace HospitalManagementSystem
{
    public class Doctor : User
    {
        private string address, email, phone;
        public Doctor(string id, string password, string fullName, string address, string email, string phone, string role) : base(id, password, fullName, role)
        {
            this.address = address;
            this.email = email;
            this.phone = phone;
        }

        private void MyDetails()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│                                      │");
            Console.WriteLine("│   DOTNET Hospital Managment System   │");
            Console.WriteLine("│──────────────────────────────────────│");
            Console.WriteLine("│              My Details              │");
            Console.WriteLine("│                                      │");
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.WriteLine();

            string[] labels = { "Name", "Email Address", "Phone", "Address" };
            string headers = $"{labels[0],-20} | {labels[1],-20} | {labels[2],-10} | {labels[3],-20}";
            // Divider matches the length of the headers
            string divider = new('─', headers.Length + 20);
            Console.WriteLine(headers);
            Console.WriteLine(divider);

            Console.WriteLine(this);
            Console.ReadKey();
            Menu();
        }

        private void MyPatients()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│                                      │");
            Console.WriteLine("│   DOTNET Hospital Managment System   │");
            Console.WriteLine("│──────────────────────────────────────│");
            Console.WriteLine("│              My Patients             │");
            Console.WriteLine("│                                      │");
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.WriteLine();
            Console.WriteLine($"Patients assigned to {fullName}");
            Console.WriteLine();

            string[] labels = { "Name", "Doctor", "Email Address", "Phone", "Address" };
            Utils.DisplayHeader(labels, "─");

            if (File.Exists($"Doctors\\RegisteredPatients\\{id}.txt"))
            {
                string[] registeredPatients = File.ReadAllLines($"Doctors\\RegisteredPatients\\{id}.txt");
                foreach (string registeredPatient in registeredPatients)
                {
                    string[] patient = File.ReadAllLines($"Patients\\{registeredPatient}.txt");
                    string[] patientInfo = patient[0].Split(';');
                    Patient p = new Patient(patientInfo[0], patientInfo[1], patientInfo[2], patientInfo[3], patientInfo[4], patientInfo[5], "Patient");
                    Console.WriteLine(p);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void ListAppointments()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│                                      │");
            Console.WriteLine("│   DOTNET Hospital Managment System   │");
            Console.WriteLine("│──────────────────────────────────────│");
            Console.WriteLine("│           All Appointments           │");
            Console.WriteLine("│                                      │");
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.WriteLine();

            string[] labels = { "Doctor", "Patient", "Description" };
            Utils.DisplayHeader(labels, "─");

            if (File.Exists($"Appointments\\Doctors\\{id}.txt"))
            {
                string[] appointments = File.ReadAllLines($"Appointments\\Doctors\\{id}.txt");
                foreach (string appointment in appointments)
                {
                    string[] appointmentInfo = appointment.Split('|');
                    Appointment app = new Appointment(appointmentInfo[0], appointmentInfo[1], appointmentInfo[2]);
                    Console.WriteLine(app);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void CheckParticularPatient()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│                                      │");
            Console.WriteLine("│   DOTNET Hospital Managment System   │");
            Console.WriteLine("│──────────────────────────────────────│");
            Console.WriteLine("│         Check Patient Details        │");
            Console.WriteLine("│                                      │");
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.WriteLine();
            Console.Write("Enter the ID of the patient to check: ");
            try
            {
                string patientID = Console.ReadLine();
                if (File.Exists($"Patients\\{patientID}.txt"))
                {
                    string[] patient = File.ReadAllLines($"Patients\\{patientID}.txt");
                    string[] patientInfo = patient[0].Split(';');

                    Console.WriteLine();

                    string[] labels = { "Patient", "Doctor", "Email Address", "Phone", "Address" };
                    Utils.DisplayHeader(labels, "─");

                    Patient p = new Patient(patientInfo[0], patientInfo[1], patientInfo[2], patientInfo[3], patientInfo[4], patientInfo[5], "Patient");
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
                        CheckParticularPatient();
                        break;
                }
            }
        }

        private void ListAppointmentsWithPatient()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│                                      │");
            Console.WriteLine("│   DOTNET Hospital Managment System   │");
            Console.WriteLine("│──────────────────────────────────────│");
            Console.WriteLine("│           Appointment With           │");
            Console.WriteLine("│                                      │");
            Console.WriteLine("└──────────────────────────────────────┘");

            Console.WriteLine();
            Console.Write("Enter the ID of the patient you would like to view appointments for: ");

            try
            {
                string patientID = Console.ReadLine();
                if (File.Exists($"Patients\\{patientID}.txt") && File.Exists($"Patients\\RegisteredDoctors\\{patientID}.txt"))
                {
                    // Check if the patient is registered with the current logged doctor
                    string[] registeredDoctor = File.ReadAllLines($"Patients\\RegisteredDoctors\\{patientID}.txt");
                    if (id == registeredDoctor[0])
                    {
                        Console.WriteLine();

                        string[] labels = { "Doctor", "Patient", "Description" };
                        Utils.DisplayHeader(labels, "─");

                        if (File.Exists($"Appointments\\Patients\\{patientID}.txt"))
                        {
                            string[] appointments = File.ReadAllLines($"Appointments\\Patients\\{patientID}.txt");
                            foreach (string appointment in appointments)
                            {
                                string[] appointmentInfo = appointment.Split('|');
                                Appointment app = new Appointment(appointmentInfo[0], appointmentInfo[1], appointmentInfo[2]);
                                Console.WriteLine(app);
                            }
                        }
                        Console.ReadKey();
                        Menu();
                    }
                    else
                    {
                        Console.WriteLine();
                        string[] labels = { "Doctor", "Patient", "Description" };
                        Utils.DisplayHeader(labels, "─");
                        throw new Exception("Patient not registered with you, press any key to return to menu");
                    }
                }
                else
                {
                    throw new Exception("Patient not found or currently not registered with any doctor, press any key to return to menu");
                }
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Patient not found or currently not registered with any doctor, press any key to return to menu":
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    case "Patient not registered with you, press any key to return to menu":
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        ListAppointmentsWithPatient();
                        break;
                }
            }
        }

        public override void Menu()
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│                                      │");
            Console.WriteLine("│   DOTNET Hospital Managment System   │");
            Console.WriteLine("│──────────────────────────────────────│");
            Console.WriteLine("│              Doctor Menu             │");
            Console.WriteLine("│                                      │");
            Console.WriteLine("└──────────────────────────────────────┘");

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
                        MyDetails();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        MyPatients();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ListAppointments();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        CheckParticularPatient();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        ListAppointmentsWithPatient();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.Clear();
                        Program.Main();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
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
            Console.ReadKey();
        }

        public override string ToString()
        {
            return $"{fullName,-20} | {email,-20} | {phone,-5} | {address,-20}";
        }
    }
}
