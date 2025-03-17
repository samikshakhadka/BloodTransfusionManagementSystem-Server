using BloodsyncAPI.DTOs;
using BloodsyncAPI.Models;

namespace BloodsyncAPI.Repositories
{
    public interface ISpecificRepos
    {
        Task<(List<Donor> Donors, int count)> GetAllDonors(List<Filter> filters, int pageNumber = 1, int pageSize = 100);
        Task<Donor?> GetDonorById(Guid id);
        Task<List<Donor>> GetFilteredDonors(string bloodGroup, string municipality, int wardNo);
        Task<List<WardRepresentatives>> GetAllWardRepresentatives();

        Task<WardRepresentatives?> GetWardRepById(Guid id);

        Task DeleteRelatedUserByDonorId(Guid id);
        Task<List<PatientWaitlist>> GetAllPatient();
        Task<PatientWaitlist> GetPatientById(Guid id);
        Task<IQueryable<User>> GetAllUsers();
        Task<User?> GetUserById(Guid id);
        Task<List<Inventory>> GetAllInventory();
        Task<List<Inventory>> GetAllInventoryByHospitalId(Guid id);
        Task<(int DonorCount, int PatientCount, int InventoryCount)> GetAllDonorsByHospitalId(Guid id);
        Task<Inventory?> GetInventoryById(Guid id);
        Task<Hospital?> GetHospitalById(Guid id);
        Task<User?> GetUserByDonorID(Guid id);
        Task<User?> GetUserByEmail(string name);
        Task<bool> IsEmailUnique(string email);

    }

}
