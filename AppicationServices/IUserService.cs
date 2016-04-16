using ApplicationEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppicationServices
{
    public interface IUserService
    {
        int Authenticate(string userName, string userPassword);

    }
}
