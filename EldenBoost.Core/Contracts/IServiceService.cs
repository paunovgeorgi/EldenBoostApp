using EldenBoost.Core.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IServiceService
    {
        Task<AllServicesFilteredAndPagedModel> AllAsync(AllServicesQueryModel model);
    }
}
