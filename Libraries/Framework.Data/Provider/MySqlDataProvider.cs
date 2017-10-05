using System.Data.Common;
using System.Data.Entity;
using Framework.Core.Data;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace Framework.Data.Provider
{
    public class MySqlDataProvider : IDataProvider
    {

        #region Properties

        public virtual bool StoredProceduredSupported
        {
            get { return true; }
        }

        public virtual bool BackupSupported
        {
            get { return true; }
        }

        #endregion

        #region Methods

        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new MySqlConnectionFactory();
#pragma warning disable 618
            Database.DefaultConnectionFactory = connectionFactory;
#pragma warning restore 618
        }


        public virtual void SetDatabaseInitializer()
        {

        }

        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        public virtual DbParameter GetParameter()
        {
            return new MySqlParameter();
        }

        public virtual int SupportedLengthOfBinaryHash()
        {
            return 0;
        }

        #endregion

    }
}
