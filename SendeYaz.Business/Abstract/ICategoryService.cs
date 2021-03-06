﻿using SendeYaz.Core.Repositories;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Abstract
{
    public interface ICategoryService:IServiceRepository<CategoryModel>
    {
        Task<List<CategoryModel>> GetAllAsync();
    }
}
