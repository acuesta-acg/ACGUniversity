﻿using Microsoft.AspNetCore.Mvc;

namespace UniversityWeb.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
