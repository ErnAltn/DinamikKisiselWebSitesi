﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class HomePageRepository : EfEntityRepositoryBase<HomePage, ProjectDbContext>, IHomePageRepository
    {
        public HomePageRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
