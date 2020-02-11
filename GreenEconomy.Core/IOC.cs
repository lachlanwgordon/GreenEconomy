using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DryIoc;
using GreenEconomy.Core.Models;
using GreenEconomy.Core.Services;
using GreenEconomy.Core.ViewModels;

namespace GreenEconomy.Core
{
    public class IOC
    {
        public IOC(HttpClient httpClient)
        {
            Container.Register<IDataStore<Business>, BusinessStore>(Reuse.Singleton);
            Container.Register<IWebClient, WebClient>(Reuse.Singleton);
            Container.RegisterInstance(httpClient);

            Current = this;
        }

        public static IOC Current { get; private set; }
        public IContainer Container = new DryIoc.Container(rules => rules.WithoutFastExpressionCompiler());


    }
}
