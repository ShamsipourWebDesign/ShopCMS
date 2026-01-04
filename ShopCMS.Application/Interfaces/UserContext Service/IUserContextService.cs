using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCMS.Application.Interfaces.UserContext_Service
{
    public interface IUserContextService
    {
        bool IsAuthenticated { get; }

        int? UserId { get; }
        string? Username { get; }
        string? Role { get; }
    }
}
