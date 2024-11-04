using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IAuthorService
    {
        Task<bool> ExistsByUserIdAsync(string userId);
    }
}
