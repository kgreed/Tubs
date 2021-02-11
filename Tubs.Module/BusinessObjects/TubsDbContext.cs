using System;
using System.Data;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace Tubs.Module.BusinessObjects {
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
}