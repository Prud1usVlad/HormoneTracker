using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public interface IAidService
    {
        Task<Doctor> LoadDoctorInfo(int userId);
        Task<List<Medicine>> LoadUserMedicines(int userId);
        Task AddMedicine(Medicine medicine);
        Task DeleteMedicine(int id);
        void CallDoctor(Doctor doctor);
        Task UpdateMedicine(Medicine medicine);
    }
}
