using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCMS.Application.Interfaces.UserContext_Service;

namespace ShopCMS.Application.UseCases
{
    public class SomeUseCase
    {
        private readonly IUserContextService _user;

        public SomeUseCase(IUserContextService user)
        {
            _user = user;
        }

        public void Run()
        {
            var userId = _user.UserId;    
            var role = _user.Role;         
            var username = _user.Username; 
        }
    }
}