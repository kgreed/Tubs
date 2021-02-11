using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DevExpress.ExpressApp.EF.DesignTime;
namespace Tubs.Module.BusinessObjects
{
    public class TubsContextInitializer : DbContextTypesInfoInitializerBase {
        protected override DbContext CreateDbContext() {
            DbContextInfo contextInfo = new DbContextInfo(typeof(TubsDbContext), new DbProviderInfo(providerInvariantName: "System.Data.SqlClient", providerManifestToken: "2008"));
            return contextInfo.CreateInstance();
        }
    }
}