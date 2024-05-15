using System.Data;
using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository;

public class DonatorRepo : IDonatorRepo
{

    IDictionary<String, string> props;

    public DonatorRepo(IDictionary<String, string> props)
    {
       
        this.props = props;
    }

    public Donator? findOne(long id)
    {
      
        IDbConnection connection = DBUtils.GetConnection(props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Donator WHERE id = @id";

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
                    long telefon = dataReader.GetInt64(2);
                    string adresa = dataReader.GetString(3);
                    Donator donator = new Donator(id, nume, telefon, adresa);
                    
                    return donator;
                }
            }
        }

        return null;
    }

    public IEnumerable<Donator> findAll()
    {
      
        IList<Donator> donatori = new List<Donator>();
        IDbConnection connection = DBUtils.GetConnection(props);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Donator";

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    long id = dataReader.GetInt64(0);
                    string nume = dataReader.GetString(1);
                    long telefon = dataReader.GetInt64(2);
                    string adresa = dataReader.GetString(3);
                    Donator donator = new Donator(id, nume, telefon, adresa);
                    donatori.Add(donator);
                }
            }
        }

        return donatori;
    }

    public Donator? save(Donator Entity)
    {
        IDbConnection connection = DBUtils.GetConnection(props);
        using (var command = connection.CreateCommand())
        {
            command.CommandText =
                "INSERT INTO Donator (id, nume, telefon, adresa) VALUES (@id, @nume, @telefon, @adresa)";

            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = Entity.Id;
            command.Parameters.Add(idParameter);

            var numeParameter = command.CreateParameter();
            numeParameter.ParameterName = "@nume";
            numeParameter.Value = Entity.Nume;
            command.Parameters.Add(numeParameter);

            var telefonParameter = command.CreateParameter();
            telefonParameter.ParameterName = "@telefon";
            telefonParameter.Value = Entity.Telefon;
            command.Parameters.Add(telefonParameter);

            var adresaParameter = command.CreateParameter();
            adresaParameter.ParameterName = "@adresa";
            adresaParameter.Value = Entity.Adresa;
            command.Parameters.Add(adresaParameter);

            var result = command.ExecuteNonQuery();
        }

        return Entity;
    }

    public Donator? delete(long id)
    {
        throw new NotImplementedException();
    }

    public Donator? update(Donator Entity)
    {
        throw new NotImplementedException();
    }

    public Donator? findDonator(string nume)
    {
       
        IDbConnection connection = DBUtils.GetConnection(props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Donator WHERE nume=@Nume";

            var nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@Nume";
            nameParameter.Value = nume;
            command.Parameters.Add(nameParameter);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    long id = dataReader.GetInt64(0);
                    string nume1 = dataReader.GetString(1);
                    long telefon = dataReader.GetInt64(2);
                    string adresa = dataReader.GetString(3);
                    Donator? donator = new Donator(id, nume1, telefon, adresa);
                    
                    return donator;
                }
            }
        }

        return null;
    }

    public bool isExist(string nume)
    {
       
        IDbConnection connection = DBUtils.GetConnection(props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Donator WHERE nume=@Nume";

            var nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@Nume";
            nameParameter.Value = nume;
            command.Parameters.Add(nameParameter);

            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    return true;
                }
            }
        }

        return false;
    }
}