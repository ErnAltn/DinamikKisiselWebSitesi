﻿
using System;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
        string CaptchaGeneratorAsync();
    }
}