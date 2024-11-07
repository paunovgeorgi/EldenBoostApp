﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(string userId);
        Task<bool> IsPendingAsync(string userId);
    }
}
