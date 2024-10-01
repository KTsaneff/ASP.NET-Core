namespace RESTful_API_Development_dotNET_Eight.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredClaimAttribute : Attribute
    {
        public string ClaimType { get; }

        public string ClaimValue { get; }

        public RequiredClaimAttribute(string claimType, string claimValue)
        {
            this.ClaimType = claimType;
            this.ClaimValue = claimValue;
        }
    }
}
