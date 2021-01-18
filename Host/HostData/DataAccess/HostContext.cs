using HostData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HostData.DataAccess
{
    public class HostContext : DbContext
    {
        public HostContext(DbContextOptions options) : base(options) { }
        public DbSet<PluginScript> Scripts { get; set; }
    }
}
