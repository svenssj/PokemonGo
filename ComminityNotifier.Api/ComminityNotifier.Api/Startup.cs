﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CommunityNotifier.Api.Startup))]

namespace CommunityNotifier.Api
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
