using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts
{
    public interface IUserContextService
    {
        Guid GetUserId();
        string GetUserName();
        string GetUserEmail();
    }
}
