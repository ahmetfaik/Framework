using System;
using Framework.Core.Common;
using Framework.Core.Data;

namespace Framework.Data.Provider
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings) : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new SiteException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                case "mysql":
                    return new MySqlDataProvider();
                case "sqlce":
                    return new SqlCeDataProvider();
                default:
                    throw new SiteException($"Not supported dataprovider name: {providerName}");
            }
        }

    }
}
