using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models.ViewModels;
using kubNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace kubNew.Components
{
    public class JuryCartViewComponent : ViewComponent
    {
        private JuryCart Cart;

        public JuryCartViewComponent(JuryCart cartService)
        {
            Cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new JuryCartViewModel
            {
                Cart = Cart
            });
        }
    }
}
