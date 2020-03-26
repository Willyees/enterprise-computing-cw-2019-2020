using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace ShareTrader.Providers
{
    public class MyIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            string id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            System.Diagnostics.Debug.WriteLine(id);
            return id;
            
        }
    }
}