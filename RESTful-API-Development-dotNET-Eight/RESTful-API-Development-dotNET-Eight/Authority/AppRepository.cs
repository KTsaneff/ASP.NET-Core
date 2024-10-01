namespace RESTful_API_Development_dotNET_Eight.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "fa64dba8-b7d9-4a52-bb4f-5a5ed32957b9",
                Secret = "cdb5f030-1b63-4132-b77a-c5dfe2408554",
                Scopes = "read,write,delete"
            }
        };

       

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(a => a.ClientId == clientId);
        }
    }
}
