using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;



[assembly: OwinStartup(typeof(ShareTrader.Startup))]

namespace ShareTrader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new ShareTrader.Providers.MyIdProvider());
            app.MapSignalR();

        }

    }
}
