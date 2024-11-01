using EldenBoost.Core.Models.Service.Contracts;
using System.Text.RegularExpressions;

namespace EldenBoost.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IServiceModel service)
        {
            string info = Regex.Replace(service.Title, @"\s+", "-");

            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            info = info.ToLower() + "-boost";

            return info;
        }
    }
}
