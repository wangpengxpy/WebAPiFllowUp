using ApplicationDataModel.UOW;
using Resolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataModel
{

    [Export(typeof(IComponent))]
    public class DependencyResolver : Resolver.IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork>();
        }

    }
}
