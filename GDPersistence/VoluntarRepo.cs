using System.Data;
using GestiuneDonatii.model;

namespace GestiuneDonatii.Repository;

public class VoluntarRepo : IVoluntarRepo
{
    
   
    IDictionary<String, string> props;
    
    public VoluntarRepo(IDictionary<String, string> props)
    {
        
        this.props = props;
    }
    
    public Voluntar? findOne(long id)
    {
        IDbConnection connection = DBUtils.GetConnection(props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Voluntar WHERE id = @id";

            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);
                
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    long id1 = dataReader.GetInt64(0);
                    string username = dataReader.GetString(1);
                    string nume = dataReader.GetString(2);
                    string email = dataReader.GetString(3);
                    long telefon = dataReader.GetInt64(4);
                    string parola = dataReader.GetString(5);
                    Voluntar voluntar = new Voluntar(id, username,  parola);
                    voluntar.Nume = nume;
                    voluntar.Email = email;
                    voluntar.Telefon = telefon;
                    
                   
                    return voluntar;
                }
            }
        }

        return null;
    }

    public IEnumerable<Voluntar> findAll()
    {
        IList<Voluntar> voluntari = new List<Voluntar>();
        IDbConnection connection = DBUtils.GetConnection(props);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Voluntar";

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    long id = dataReader.GetInt64(0);
                    string username = dataReader.GetString(1);
                    string nume = dataReader.GetString(2);
                    string email = dataReader.GetString(3);
                    long telefon = dataReader.GetInt64(4);
                    string parola = dataReader.GetString(5);
                    Voluntar voluntar = new Voluntar(id,  username,  parola);
                    voluntar.Nume = nume;
                    voluntar.Email = email;
                    voluntar.Telefon = telefon;
                    voluntari.Add(voluntar);
                }
            }
        }

        return voluntari;
    }

    public Voluntar? save(Voluntar Entity)
    {
        throw new NotImplementedException();
    }

    public Voluntar? delete(long id)
    {
        throw new NotImplementedException();
    }

    public Voluntar? update(Voluntar Entity)
    {
        throw new NotImplementedException();
    }

    public Voluntar VerifyLogin(string username, string hashPassword)
    {
        IDbConnection connection = DBUtils.GetConnection(props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from Voluntar where username=@Username and parola=@Password";

            var usernameParameter = command.CreateParameter();
            usernameParameter.ParameterName = "@Username";
            usernameParameter.Value = username;
            command.Parameters.Add(usernameParameter);
            
            var passwordParameter = command.CreateParameter();
            passwordParameter.ParameterName = "@Password";
            passwordParameter.Value = hashPassword;
            command.Parameters.Add(passwordParameter);
                
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    long id = dataReader.GetInt64(0);
                    string username1 = dataReader.GetString(1);
                    string nume = dataReader.GetString(2);
                    string email = dataReader.GetString(3);
                    long telefon = dataReader.GetInt64(4);
                    string parola = dataReader.GetString(5);
                    Voluntar voluntar = new Voluntar(id,  username, parola);
                    voluntar.Nume = nume;
                    voluntar.Email = email;
                    voluntar.Telefon = telefon;
                    
                   
                    return voluntar;
                }
            }
        }

        return null;
    }

    public Voluntar FindByUsername(string username)
    {
        IDbConnection connection = DBUtils.GetConnection(props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from Voluntar where username=@Username";

            var usernameParameter = command.CreateParameter();
            usernameParameter.ParameterName = "@Username";
            usernameParameter.Value = username;
            command.Parameters.Add(usernameParameter);
                
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    long id = dataReader.GetInt64(0);
                    string username1 = dataReader.GetString(1);
                    string nume = dataReader.GetString(2);
                    string email = dataReader.GetString(3);
                    long telefon = dataReader.GetInt64(4);
                    string parola = dataReader.GetString(5);
                    Voluntar voluntar = new Voluntar(id, username1, parola);
                    voluntar.Nume = nume;
                    voluntar.Email = email;
                    voluntar.Telefon = telefon;
                    
                    return voluntar;
                }
            }
        }

        return null;
    }
}