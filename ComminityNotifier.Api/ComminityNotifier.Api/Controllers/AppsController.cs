﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommunityNotifier.Api.Controllers
{
    public class AppsController : Controller
    {
        // GET: Apps
        public ActionResult Index()
        {
            return View();
        }
    }
}