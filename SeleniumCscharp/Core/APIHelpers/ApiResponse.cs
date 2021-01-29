using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeleniumCSharp.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public class ApiResponse<TDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public TDto Dto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Uri LocationHeader { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<TDto>> Map(HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return new ApiResponse<TDto>
            {
                Dto = JsonConvert.DeserializeObject<TDto>(content),
                StatusCode = httpResponseMessage.StatusCode,
                LocationHeader = httpResponseMessage.Headers?.Location
            };
        }
    }
}