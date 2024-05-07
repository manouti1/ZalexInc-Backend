using Newtonsoft.Json.Serialization;

namespace ZalexInc.Certification.API.Resolvers
{
    public class CustomContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
