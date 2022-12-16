using Epsilon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Epsilon.Data.Common.DataValidation;

namespace Epsilon.Services.Data.Contracts
{
    public interface IOrderService
    {
        Task<string> CreateAsync(string customerId, string address);

        Task AddComputerToOrderAsync(string orderId, int computerId);
    }
}
