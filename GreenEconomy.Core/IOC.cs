using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DryIoc;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;

namespace GreenEconomy.Core
{
    public class IOC
    {
        public IOC()
        {
        }

        public static IOC Current { get; private set; }
        public IContainer Container = new DryIoc.Container(rules => rules.WithoutFastExpressionCompiler());


        public void Initialize()
        {
            Container.Register<IDataStore<Business>, BusinessStore>(Reuse.Singleton);
            Current = this;
        }

    }
}
