using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver
{
    public interface IRegisterComponent
    {
        void RegisterType<TFrom, TTo>() where TTo : TFrom;

    }
}
