using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SYS_SGI.Infrastructure.Data.Implementation.Base
{
    public static class DataBaseManager
    {
        public static Database GetDefaultDataBase()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database oDatabase = factory.CreateDefault();
            return oDatabase;
        }
    }
}