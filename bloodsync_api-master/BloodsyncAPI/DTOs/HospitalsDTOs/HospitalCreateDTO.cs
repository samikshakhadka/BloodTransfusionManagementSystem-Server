using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;

namespace BloodsyncAPI.DTOs.HospitalsDTO
{
    public class HospitalCreateDTO
    {
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string? LogoUrl { get; set; }
        public string HospitalDescription { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Municipality { get; set; }
        public int WardNo { get; set; }
        public string HospitalEmail { get; set; }
        public string? Location { get; set; }
    }
}
