using Newtonsoft.Json;
using Application.Common.Interfaces;

namespace Application.Common.Helper
{
    public static class ObjectSerializerHelper
    {
        public static string Serialize(this IPayLoadObject obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
