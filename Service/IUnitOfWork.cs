using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUnitOfWork
    {
        public RegisterService RegisterService { get; }
        public LoginService LoginService { get; }
        public ChngpwdService ChngpwdService { get; }
        public UserService UserService { get; }
        public AuthorsService AuthorsService { get; }
        public BooksService BooksService { get; }
    }
}
