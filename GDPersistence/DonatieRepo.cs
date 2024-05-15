using System.Data;
using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository;

public class DonatieRepo : IDonatieRepo
{
    
    IDictionary<String, string> props;
    
    public DonatieRepo(IDictionary<String, string> props)
    {
        this.props = props;
    }
    
    public Donatie? findOne(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Donatie> findAll()
    {
        IList<Donatie> donations = new List<Donatie>();
        IDbConnection connection = DBUtils.GetConnection(props);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Donatie";

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    long id = dataReader.GetInt64(0);
                    long id_cauza = dataReader.GetInt64(1);
                    long id_donator = dataReader.GetInt64(2);
                    String data = dataReader.GetString(3);
                    float sum = dataReader.GetFloat(4);
                    Cauza cauza = new Cauza(id_cauza, "", "");
                    Donator? donator = new Donator(id_donator, "", 0, "");
                    Donatie donatie = new Donatie(id, donator, cauza, sum);
                    donatie.Data = data;
                    donations.Add(donatie);
                }
            }
        }

        return donations;
    }

    public Donatie? save(Donatie Entity)
    {
        IDbConnection connection = DBUtils.GetConnection(props);
        using (var command = connection.CreateCommand())
        {
            command.CommandText =
                "INSERT INTO Donatie (id, id_cauza, id_donator, data, suma) VALUES (@id, @id_cauza, @id_donator, @data, @suma)";

            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = Entity.Id;
            command.Parameters.Add(idParameter);

            var cauzaParameter = command.CreateParameter();
            cauzaParameter.ParameterName = "@id_cauza";
            cauzaParameter.Value = Entity.Cauza.Id;
            command.Parameters.Add(cauzaParameter);

            var donatorParameter = command.CreateParameter();
            donatorParameter.ParameterName = "@id_donator";
            donatorParameter.Value = Entity.Donator.Id;
            command.Parameters.Add(donatorParameter);

            var dataParameter = command.CreateParameter();
            dataParameter.ParameterName = "@data";
            dataParameter.Value = Entity.Data;
            command.Parameters.Add(dataParameter);
            
            var sumaParameter = command.CreateParameter();
            sumaParameter.ParameterName = "@suma";
            sumaParameter.Value = Entity.Suma;
            command.Parameters.Add(sumaParameter);
            
            var result = command.ExecuteNonQuery();
        }

        return Entity;
    }

    public Donatie? delete(long id)
    {
        throw new NotImplementedException();
    }

    public Donatie? update(Donatie Entity)
    {
        throw new NotImplementedException();
    }
}