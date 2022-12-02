using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public partial class Doctor : ObservableItem
    {
        private int doctorId;
        private string name;
        private string lastName;
        private string midName;
        private string phone;
        private string email;
        private string password;

        public int DoctorId { get => doctorId; set { doctorId = value; OnPropertyChanged(nameof(DoctorId)); } }
        public string Name { get => name; set { name = value;  OnPropertyChanged(nameof(Name)); } }
        public string LastName { get => lastName; set { lastName = value; OnPropertyChanged(); } }
        public string MidName { get => midName; set { midName = value; OnPropertyChanged(); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

    }
}
