using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using MySql.Data.Entity;

namespace Framework.Data.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlContext : DbObjectContext
    {

        public MySqlContext() : base("name=mySqlLocalConnection") { }

        public MySqlContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
