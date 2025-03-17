using AutoMapper;
using BloodsyncAPI.DTOs.BloodGroupsDTO;
using BloodsyncAPI.DTOs.DonorsDTO;
using BloodsyncAPI.DTOs.HospitalsDTO;
using BloodsyncAPI.DTOs.HospitalsDTOs;
using BloodsyncAPI.DTOs.InventorysDTO;
using BloodsyncAPI.DTOs.LoginDTOs;
using BloodsyncAPI.DTOs.PatientWaitlistsDTO;
using BloodsyncAPI.DTOs.PriorityDTO;
using BloodsyncAPI.DTOs.UsersDTO;
using BloodsyncAPI.DTOs.UserTypesDTO;
using BloodsyncAPI.DTOs.WardRepresentativesDTO;
using BloodsyncAPI.Models;

namespace BloodsyncAPI.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Hospital, HospitalReadDTO>();
            CreateMap<HospitalCreateDTO, Hospital>();
            CreateMap<HospitalUpdateDTO, Hospital>();
            CreateMap<Hospital, HospitalUpdateDTO>();
            CreateMap<Hospital, HospitalShortReadDTO>();

            CreateMap<Inventory, InventoryReadDTO>();
            CreateMap<InventoryCreateDTO, Inventory>();
            CreateMap<InventoryUpdateDTO, Inventory>();
            CreateMap<Inventory, InventoryUpdateDTO>();

            CreateMap<Priority, PriorityReadDTO>();
            CreateMap<PriorityCreateDTO, Priority>();
            CreateMap<PriorityUpdateDTO, Priority>();
            CreateMap<Priority, PriorityUpdateDTO>();

            CreateMap<PatientWaitlist, PatientWaitListReadDTO>();
            CreateMap<PatientWaitListCreateDTO, PatientWaitlist>();
            CreateMap<PatientWaitListUpdateDTO, PatientWaitlist>();
            CreateMap<PatientWaitlist, PatientWaitListUpdateDTO>();

            CreateMap<UserType, UserTypeReadDTO>();
            CreateMap<UserTypeCreateDTO, UserType>();
            CreateMap<UserTypeUpdateDTO, UserType>();
            CreateMap<UserType, UserTypeUpdateDTO>();

            CreateMap<User, UsersReadDTO>();
            CreateMap<UsersCreateDTO, User>();
            CreateMap<UsersUpdateDTO, User>();
            CreateMap<User, UsersUpdateDTO>();

            CreateMap<Donor, DonorReadDTO>();
            CreateMap<DonorCreateDTO, Donor>();
            CreateMap<DonorUpdateDTO, Donor>();
            CreateMap<Donor, DonorUpdateDTO>();

            CreateMap<BloodGroup, BloodGroupReadDTO>();
            CreateMap<BloodGroupCreateDTO, BloodGroup>();
            CreateMap<BloodGroupUpdateDTO, BloodGroup>();
            CreateMap<BloodGroup, BloodGroupUpdateDTO>();

            CreateMap<UserDonorCreateDTO, User>();
            CreateMap<UserDonorCreateDTO, Donor>();
            CreateMap<UserDonorUpdateDTO, User>();
            CreateMap<UserDonorUpdateDTO, Donor>();
            CreateMap<Donor, UserDonorUpdateDTO>();
            CreateMap<User, UserDonorUpdateDTO>();

            CreateMap<User, LoginDisplayDTO>();

            CreateMap<WardRepresentatives, WardRepresentativesReadDTO>();
            CreateMap<WardRepresentativesCreateDTO, WardRepresentatives>();
            CreateMap<WardRepresentativesUpdateDTO, WardRepresentatives>();
            CreateMap<WardRepresentatives, WardRepresentativesUpdateDTO>();




            CreateMap<PasswordChangeDTO, User>();



        }
    }
}
