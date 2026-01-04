using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCMS.Infrastructure.Auth.Dtos;

namespace ShopCMS.Infrastructure.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginAsync(LoginRequest request);
        Task<TokenResponse> RefreshAsync(RefreshRequest request);
    }

}
