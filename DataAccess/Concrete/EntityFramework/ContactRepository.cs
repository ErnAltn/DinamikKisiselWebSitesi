
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Utilities;
namespace DataAccess.Concrete.EntityFramework
{
    public class ContactRepository : EfEntityRepositoryBase<Contact, ProjectDbContext>, IContactRepository
    {
        public ContactRepository(ProjectDbContext context) : base(context)
        {
        }
        public string CaptchaGeneratorAsync()
        {
            Tuple<string, byte[]> captcha = CaptchaService.GenerateCaptcha();

            return captcha.Item1;
        }
    }
}
