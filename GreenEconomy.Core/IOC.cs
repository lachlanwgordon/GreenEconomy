using System;
using System.Threading.Tasks;
using DryIoc;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;

namespace GreenEconomy.Core
{
    public class IOC
    {
        public IOC()
        {
        }

        public static IOC Current { get; private set; }
        public IContainer Container = new DryIoc.Container();


        public void Initialize()
        {
            Container.Register<IDataStore<Business>, BusinessStore>();
            Current = this;
        }
    }
}
