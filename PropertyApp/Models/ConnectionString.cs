namespace PropertyAPI.Models
{
    public class ConnectionString
    {
        public string Value { get; }

        public ConnectionString(string pgHostName, string pgPort, string pgUser, string pgPassword, string pgDatabase)
        {
            var connectionString  =
                "data source=localhost;initial catalog = DevTest; persist security info = True;Integrated Security = SSPI; ";
            Value = connectionString;
        }
    }
}
