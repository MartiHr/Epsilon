﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Services.Data.Contracts
{
    public interface ICategoryService
    {
        Task CreateAsync(string categoryName);
    }
}
