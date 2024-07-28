namespace MyFriends.DAL
{
    public class Data
    {
        string ConnectionString = "server=ARIELOHANAPC\\SQLEXPRESS;initial catalog=friends; user id=sa; password=1234; TrustServerCertificate=Yes;";
        static Data _data;
        private  DataLayer DataLayer;
        private Data()
        {
            DataLayer  = new DataLayer(ConnectionString);
        }
        public static DataLayer Get
        {
            get {
                if (_data == null)
                    _data = new Data();
                return _data.DataLayer;
            }
        }
    }
}