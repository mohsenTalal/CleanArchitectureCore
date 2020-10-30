using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace $safeprojectname$
{
    public class JsonHelper
    {
        public static string Serialize(object obj)
        {
            if (obj == null)
                return string.Empty;
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string serialized)
        {
            if (string.IsNullOrEmpty(serialized))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(serialized,
                new JsonSerializerSettings()
                {
                    ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver()
                });
        }
    }

    public class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            var property = member as PropertyInfo;
            if (property != null)
            {
                var hasPrivateSetter = property.GetSetMethod(true) != null;
                jProperty.Writable = hasPrivateSetter;
            }

            return jProperty;
        }
    }


}
