using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EF.DesignTime;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace Tubs.Module.BusinessObjects {
	public class TubsContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			DbContextInfo contextInfo = new DbContextInfo(typeof(TubsDbContext), new DbProviderInfo(providerInvariantName: "System.Data.SqlClient", providerManifestToken: "2008"));
            return contextInfo.CreateInstance();
		}
	}
	[TypesInfoInitializer(typeof(TubsContextInitializer))]
	public class TubsDbContext : DbContext {
		public TubsDbContext(String connectionString)
			: base(connectionString) {
		}
		public TubsDbContext(DbConnection connection)
			: base(connection, false) {
		}
		public TubsDbContext() {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
		public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
		public DbSet<PermissionPolicyUser> Users { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }

		//public DbSet<Tub> Tubs { get; set; }
	}
	[DomainComponent]
    [DefaultClassOptions]
    [NavigationItem("Config")]
    public class NPTub
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<NPTub> GetData()
        {
            var l = new List<NPTub>();
            for (int i = 0; i < 50; i++)
            {
                var tub = new NPTub { Name = $"name {i}", Id = i };
                l.Add(tub);
            }

            return l.ToList();

        }
    }
}