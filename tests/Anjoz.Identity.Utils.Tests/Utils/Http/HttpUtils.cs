using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anjoz.Identity.Utils.Tests.Utils.Http
{
    public class HttpUtils
    {
        public static StringContent ConvertObjectToStringContent(object o)
        {
            return new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8,
                MediaTypeNames.Application.Json);
        }


        public static async Task<T> ConvertHttpResponseToAsync<T>(HttpResponseMessage response)
        {
            var readAsStringAsync = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<T>(readAsStringAsync);
            }
            catch
            {
                return (T) Convert.ChangeType(readAsStringAsync, typeof(T));
            }
        }
    }
}