using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BloodsyncAPI.Models;

namespace BloodsyncAPI.Data
{
    public class BloodsyncAPIContext : DbContext
    {
        public BloodsyncAPIContext(DbContextOptions<BloodsyncAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BloodsyncAPI.Models.BloodGroup>? BloodGroup { get; set; }

        public DbSet<BloodsyncAPI.Models.PatientWaitlist>? PatientWaitlist { get; set; }

        public DbSet<BloodsyncAPI.Models.Hospital>? Hospital { get; set; }

        public DbSet<BloodsyncAPI.Models.Inventory>? Inventory { get; set; }
        public DbSet<BloodsyncAPI.Models.Donor>? Donor { get; set; }

        public DbSet<BloodsyncAPI.Models.Priority>? Priority { get; set; }

        public DbSet<BloodsyncAPI.Models.User>? User { get; set; }

        public DbSet<BloodsyncAPI.Models.UserType>? UserType { get; set; }

        public DbSet<BloodsyncAPI.Models.WardRepresentatives>? WardRepresentatives { get; set; }




    }
}
