using Microsoft.Extensions.Configuration;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private IConfiguration Configuration { get; }
        public UnitOfWork(IConfiguration config)
        {
            Configuration = config;
        }


        public RegisterService RegisterService => new RegisterService();
        public LoginService LoginService => new LoginService();
        public ChngpwdService ChngpwdService => new ChngpwdService();
        public UserService UserService => new UserService();
        public AuthorsService AuthorsService => new AuthorsService();
        public BooksService BooksService =>  new BooksService();
    }
}
