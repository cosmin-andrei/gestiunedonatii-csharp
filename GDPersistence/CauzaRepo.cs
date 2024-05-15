using System.Data;
using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository
{
    public class CauzaRepo : ICauzaRepo
    {
        IDictionary<String, string> props;

        public CauzaRepo(IDictionary<String, string> props)
        {
            this.props = props;
        }

        public Cauza? findOne(long id)
        {
            
            IDbConnection connection = DBUtils.GetConnection(props);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cauza WHERE id = @id";

                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                command.Parameters.Add(idParameter);

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        long id1 = dataReader.GetInt64(0);
                        string nume = dataReader.GetString(1);
                        string descriere = dataReader.GetString(2);
                        Cauza cauza = new Cauza(id1, nume, descriere);
                        return cauza;
                    }
                }
            }

            return null;
        }

        public IEnumerable<Cauza> findAll()
        {
            IList<Cauza> cauze = new List<Cauza>();
            IDbConnection connection = DBUtils.GetConnection(props);
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cauza";

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        long id = dataReader.GetInt64(0);
                        string nume = dataReader.GetString(1);
                        string descriere = dataReader.GetString(2);
                        Cauza cauza = new Cauza(id, nume, descriere);
                        cauze.Add(cauza);
                    }
                }
            }

            return cauze;
        }

        public Cauza? save(Cauza Entity)
        {
            throw new NotImplementedException();
        }

        public Cauza? delete(long id)
        {
            throw new NotImplementedException();
        }

        public Cauza? update(Cauza Entity)
        {
            throw new NotImplementedException();
        }

        public Cauza findByNume(string nume)
        {
            
            IDbConnection connection = DBUtils.GetConnection(props);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Cauza where nume = @Nume";

                var usernameParameter = command.CreateParameter();
                usernameParameter.ParameterName = "@Nume";
                usernameParameter.Value = nume;
                command.Parameters.Add(usernameParameter);

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        long id1 = dataReader.GetInt64(0);
                        string nume1 = dataReader.GetString(1);
                        string descriere = dataReader.GetString(2);
                        Cauza cauza = new Cauza(id1, nume1, descriere);
                      
                        return cauza;
                    }
                }
            }

            return null;
        }
    }
    
}