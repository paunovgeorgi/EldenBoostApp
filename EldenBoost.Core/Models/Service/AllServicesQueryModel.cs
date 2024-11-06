using EldenBoost.Core.Models.Service.Enums;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.GeneralApplicationConstants;

namespace EldenBoost.Core.Models.Service
{
    public class AllServicesQueryModel
    {
        [Display(Name = "Search")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Services By")]
        public ServiceSorting ServiceSorting { get; set; }

        public int CurrentPage { get; set; } = DefaultPage;

        [Display(Name = "Show Services On Page")]
        public int ServicesPerPage { get; set; } = EntitiesPerPage;

        public int TotalServices { get; set; }

        public IEnumerable<ServiceCardViewModel> Services { get; set; } = new List<ServiceCardViewModel>();
    }
}
