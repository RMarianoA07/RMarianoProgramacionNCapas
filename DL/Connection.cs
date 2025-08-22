using System.Configuration;

namespace DL
{
    public class Connection
    {
        public static string Get()
        {
            return ConfigurationManager.ConnectionStrings["RMarianoProgramacionNCapas"].ToString();
        }
    }
}
