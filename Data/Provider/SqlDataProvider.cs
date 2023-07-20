using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using System.Data;
using System.Data.SqlClient;

namespace Intelequia.Modules.DNNPulse.Data
{
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;

        #endregion

        #region Properties

        public string ConnectionString => _connectionString;
        public string ProviderPath { get; }
        public string ObjectQualifier { get; }
        public string DatabaseOwner { get; }

        #endregion

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            var objProvider = (Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

            if (objProvider != null)
            {
                // Read the attributes for this provider

                //Get Connection string from web.config
                _connectionString = Config.GetConnectionString();

                if (_connectionString == string.Empty)
                    // Use connection string specified in provider
                    _connectionString = objProvider.Attributes["connectionString"];

                ProviderPath = objProvider.Attributes["providerPath"];

                ObjectQualifier = objProvider.Attributes["objectQualifier"];

                if (!(string.IsNullOrEmpty(ObjectQualifier)) && ObjectQualifier.EndsWith("_") == false)
                    ObjectQualifier += "_";

                DatabaseOwner = objProvider.Attributes["databaseOwner"];

                if (!(string.IsNullOrEmpty(DatabaseOwner)) && DatabaseOwner.EndsWith(".") == false)
                    DatabaseOwner += ".";
            }
        }
        #region DBQuery

        public override Model.DNNPulse GetDNNPulse()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataReader reader;
            Model.DNNPulse query = new Model.DNNPulse();
            // Here we call the stored procedure on the database.
            using (SqlCommand cmd = new SqlCommand("int_GetDNNPulse", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string type = reader.GetString(reader.GetOrdinal("Type"));
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    if (type == "Modules")
                    {
                        string[] splits = name.Split('-');
                        query.ModulesName.Add(splits[0].Trim());
                        query.ModulesVersion.Add(splits[1].Trim());
                    }
                    else
                    {
                        if (type == "PortalAlias")
                        {
                            query.PortalAlias.Add(name);
                        }
                        else
                        {
                            if (type == "DNNVersion")
                            {
                                query.DNNVersion = name;
                            }
                            else
                            {
                                if (type == "DatabaseSize")
                                {
                                    query.DatabaseSize = name;
                                }
                                else
                                {
                                    query.DatabaseTier = name;
                                }
                            }
                        }
                    }
                }

                connection.Close();
                return query;
            }

        }
        #endregion
    }
}