using System.Data;
using GestiuneDonatii.utils;

namespace GestiuneDonatii.Repository
{
    public static class DBUtils
    {
		

        private static IDbConnection instance = null;


        public static IDbConnection GetConnection(IDictionary<string,string> props)
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                instance = getNewConnection(props);
                instance.Open();
            }
            return instance;
        }

        private static IDbConnection getNewConnection(IDictionary<string,string> props)
        {
			
            return ConnectionFactory.GetInstance().createConnection(props);


        }
    }
}