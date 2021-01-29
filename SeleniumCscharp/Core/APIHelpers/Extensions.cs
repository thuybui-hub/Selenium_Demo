using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeleniumCSharp.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequestDto"></typeparam>
        /// <typeparam name="TResponseDto"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<TResponseDto>> PostAsync<TRequestDto, TResponseDto>(this HttpClient client, string requestUri, TRequestDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(requestUri, httpContent);
            var apiResponse = await ApiResponse<TResponseDto>.Map(httpResponse);

            return apiResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<TDto>> GetAsync<TDto>(this HttpClient client, string requestUri)
        {
            var httpResponse = await client.GetAsync(requestUri);
            var apiResponse = await ApiResponse<TDto>.Map(httpResponse);

            return apiResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponseDto"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<TResponseDto>> PostAsync<TResponseDto>(this HttpClient client, string requestUri, object dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await client.PostAsync(requestUri, httpContent);
            var apiResponse = await ApiResponse<TResponseDto>.Map(httpResponse);

            return apiResponse;
        }
    }
}