﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class ServiceRepository : EfEntityRepositoryBase<Service, ProjectDbContext>, IServiceRepository
    {
        public ServiceRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
