using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using kubNew.Models.ViewModels;
using kubNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace kubNew.Components
{
    public class PlayerCartViewComponent : ViewComponent
    {
        private PlayerCart Cart;

        public PlayerCartViewComponent(PlayerCart cartService)
        {
            Cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new PlayerCartViewModel
            {
                Cart = Cart
            });
        }
    }
}
