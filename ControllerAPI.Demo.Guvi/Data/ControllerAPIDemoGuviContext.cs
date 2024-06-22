using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControllerAPI.Demo.Guvi.Models;

namespace ControllerAPI.Demo.Guvi.Data
{
    public class ControllerAPIDemoGuviContext : DbContext
    {
        public ControllerAPIDemoGuviContext (DbContextOptions<ControllerAPIDemoGuviContext> options)
            : base(options)
        {
        }
        public DbSet<APIDeprecationSettings> aPIDeprecationSettings { get; set; }
        public DbSet<ControllerAPI.Demo.Guvi.Models.Product> Product { get; set; } = default!;
    }
}
