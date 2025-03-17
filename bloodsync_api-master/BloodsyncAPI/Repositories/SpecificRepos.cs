using BloodsyncAPI.Data;
using BloodsyncAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace BloodsyncAPI.Repositories
{
    public class SpecificRepos : ISpecificRepos
    {
        private readonly BloodsyncAPIContext _context;

        public static class FilterKeys
        {
            public const string BloodGroup = "bloodgroup";
            public const string Municipality = "municipality";
            public const string WardNo = "wardno";
        }

        public SpecificRepos(BloodsyncAPIContext context)
        {
            _context = context;
        }

        public async Task<(List<Donor> Donors, int count)> GetAllDonors(List<Filter> filters, int pageNumber = 1, int pageSize = 100)
        {

            var donors = _context.Donor
                    .Include(donor => donor.BloodGroup)
                    .Include(donor => donor.Hospital)
                    .Where(donor => donor.DateDeleted == null)
                    .AsQueryable();
            var count = await _context.Donor
      .Where(donor => donor.DateDeleted == null)
      .CountAsync();

            // Filtering
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Key) && !string.IsNullOrWhiteSpace(filter.Value))
                    {
                        switch (filter.Key.ToLower())
                        {
                            case FilterKeys.BloodGroup:
                                var encodedFilterQuery = HttpUtility.UrlEncode(filter.Value);
                                donors = donors.Where(donor => donor.BloodGroup.BloodGroupName.Equals(encodedFilterQuery));
                                count = donors.Count();
                                break;
                            case FilterKeys.Municipality:
                                donors = donors.Where(donor => donor.Municipality.Equals(filter.Value));
                                count = donors.Count();
                                break;
                            case FilterKeys.WardNo:
                                int.TryParse(filter.Value, out int wardNo);
                                donors = donors.Where(donor => donor.WardNo.Equals(wardNo));
                                count = donors.Count();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            var donorsList = await donors.Skip(skipResults).Take(pageSize).ToListAsync();
            return (donorsList, count);
        }


        public async Task<Donor?> GetDonorById(Guid id)
        {
            var donor = await _context.Donor.Include(donor => donor.BloodGroup).Include(donor => donor.Hospital).Where(donor => donor.DonorId == id && donor.DateDeleted == null).FirstOrDefaultAsync();
            if (donor == null)
            {
                return null;
            }
            return donor;
        }

        public async Task<List<PatientWaitlist>> GetAllPatient()
        {
            return await _context.PatientWaitlist.Include(patient => patient.BloodGroup)
               .Include(patient => patient.Hospital)
                .Where(patient => patient.DateDeleted == null
                && patient.BloodGroup.DateDeleted == null
                && patient.Hospital.DateDeleted == null)
                .ToListAsync();
        }
        public async Task<PatientWaitlist> GetPatientById(Guid id)
        {
            var patient = await _context.PatientWaitlist.Include(patient => patient.BloodGroup)
               .Include(patient => patient.Hospital)
                .Where(patient => patient.PatientId == id &&
                patient.DateDeleted == null).FirstOrDefaultAsync();
            if (patient == null)
            {
                return null;
            }
            return patient;
        }


        public async Task<IQueryable<User>> GetAllUsers()
        {
            var users = await _context.User
                .Include(user => user.Donor)
                .Include(user => user.UserType)
                .Include(user => user.Hospital)
                .Where(user => user.DateDeleted == null)
                .ToListAsync();

            return users.AsQueryable();
        }



        public async Task<User?> GetUserById(Guid id)
        {
            var user = await _context.User.Include(user => user.Donor).Include(user => user.Donor.BloodGroup).Include(user => user.UserType).Include(user => user.Hospital).Where(user => user.UserId == id && user.DateDeleted == null).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<Inventory>> GetAllInventory()
        {
            return await _context.Inventory.
                Include(inventory => inventory.BloodGroup).
                Include(inventory => inventory.Hospital).Where(data => data.DateDeleted == null
                && data.BloodGroup.DateDeleted == null).ToListAsync();
        }

        public async Task<List<Inventory>> GetAllInventoryByHospitalId(Guid id)
        {
            return await _context.Inventory.
                Include(inventory => inventory.BloodGroup).
                Include(inventory => inventory.Hospital).Where(data => data.DateDeleted == null
                && data.BloodGroup.DateDeleted == null && data.HospitalId == id).ToListAsync();
        }



        public async Task<Inventory?> GetInventoryById(Guid id)
        {
            var inventory = await _context.Inventory.Include(inventory => inventory.BloodGroup).Include(inventory => inventory.Hospital).Where(inventory => inventory.InventoryId == id && inventory.DateDeleted == null).FirstOrDefaultAsync();
            if (inventory == null)
            {
                return null;
            }
            return inventory;
        }

        public async Task<Hospital?> GetHospitalById(Guid id)
        {
            var hospital = await _context.Hospital.Where(hospital => hospital.HospitalId == id && hospital.DateDeleted == null).FirstOrDefaultAsync();
            if (hospital == null)
            {
                return null;
            }
            return hospital;
        }

        public async Task DeleteRelatedUserByDonorId(Guid id)

        {
            var users = await _context.User.Where(user => user.DonorId == id).ToListAsync();
            foreach (var user in users)
            {
                user.DateDeleted = DateTime.Now;
                _context.User.Update(user);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<User?> GetUserByDonorID(Guid id)
        {
            var user = await _context.User.Where(user => user.DonorId == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.User.Include(user => user.UserType).Where(user => user.Email == email && user.DateDeleted == null).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return !await _context.User.AnyAsync(user => user.Email == email);
        }

        public async Task<List<WardRepresentatives>> GetAllWardRepresentatives()
        {
            return await _context.WardRepresentatives.Include(wardRep => wardRep.Hospital).Where(wardRep => wardRep.DateDeleted == null).ToListAsync();
        }

        public async Task<WardRepresentatives?> GetWardRepById(Guid id)
        {
            var wardRep = await _context.WardRepresentatives.Include(wardRep => wardRep.Hospital).Where(wardRep => wardRep.WardRepID == id && wardRep.DateDeleted == null).FirstOrDefaultAsync();
            if (wardRep == null)
            {
                return null;
            }
            return wardRep;
        }

        public async Task<List<Donor>> GetFilteredDonors(string bloodGroup, string municipality, int wardNo)
        {
            DateTime currentDate = DateTime.Now;
            DateTime thresholdDate = currentDate.AddDays(-90);
            var filteredDonors = await _context.Donor
    .Include(donor => donor.BloodGroup)
    .Where(donor => donor.DateDeleted == null && EF.Functions.DateDiffDay(donor.LastDonated, currentDate) > 90 && donor.BloodGroup.BloodGroupName.ToLower() == bloodGroup.ToLower()
    && donor.Municipality.ToLower() == municipality.ToLower() && donor.WardNo.Equals(wardNo))
    .ToListAsync();

            if (filteredDonors == null || !filteredDonors.Any())
            {
                filteredDonors = await _context.Donor
                    .Include(donor => donor.BloodGroup)
    .Where(donor => donor.DateDeleted == null && EF.Functions.DateDiffDay(donor.LastDonated, currentDate) > 90 && donor.BloodGroup.BloodGroupName.ToLower() == bloodGroup.ToLower()).Take(5)
                    .ToListAsync();
            }

            if (filteredDonors == null || !filteredDonors.Any())
            {
                filteredDonors = await _context.Donor
                    .Include(donor => donor.BloodGroup)
                    .Where(donor =>
                        donor.DateDeleted == null && EF.Functions.DateDiffDay(donor.LastDonated, currentDate) > 90 &&
                        donor.BloodGroup.BloodGroupName.ToLower() == bloodGroup.ToLower()).Take(5)
                    .ToListAsync();
            }

            return filteredDonors;

        }

        public async Task<(int DonorCount, int PatientCount, int InventoryCount)> GetAllDonorsByHospitalId(Guid id)
        {
            int donors = await _context.Donor.Where(donor => donor.DateDeleted == null && donor.HospitalId == id).CountAsync();
            int patients = await _context.PatientWaitlist.Where(patient => patient.DateDeleted == null && patient.HospitalId == id).CountAsync();
            int inventoryCount = await _context.Inventory.Where(donor => donor.DateDeleted == null && donor.HospitalId == id).SumAsync(inventory => inventory.Quantity);
            return (donors, patients, inventoryCount); 
        }

    }
}