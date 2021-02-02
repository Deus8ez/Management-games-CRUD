using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using kubNew.Models;
using kubNew.Models.ViewModels;

namespace kubNew.Controllers
{
    public class JuryController : Controller
    {
        private readonly string dbstring;

        private IJuryRepository _juryService;
        private JuryCart JCart;

        public JuryController(IConfiguration configuration, IJuryRepository juryService, JuryCart jCartService)
        {
            dbstring = configuration.GetConnectionString("connString");
            JCart = jCartService;
            _juryService = juryService;
        }

        public int PageSize = 10;

        public ViewResult Index(int page = 1)
        {
            List<Jury> juries = _juryService.FetchJuries(dbstring);

            return View(new JuryListViewModel
            {
                Juries = juries.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = juries.Count()
                }
            });
        }

        [HttpGet]
        public ViewResult AddJuries()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddJury(Jury participant)
        {

            if (ModelState.IsValid)
            {
                _juryService.AddNewJury(participant, dbstring);
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddJuries");
            }
        }

        [HttpGet]
        public ViewResult GetJury(Guid ID)
        {
            return View(_juryService.GetJury(ID, dbstring));
        }

        [HttpPost]
        public IActionResult UpdateJury(Jury participant)
        {
            _juryService.UpdateJury(participant, dbstring);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteJury(Jury participant)
        {
            _juryService.DeleteJury(participant, dbstring);
            return RedirectToAction("Index");
        }

        public ViewResult JuriesCart(string returnUrl)
        {
            return View(new JuryCartViewModel
            {
                //Cart = GetCart(),
                Cart = JCart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToJuryCart(Guid ID)
        {
            Jury jury = _juryService.GetJury(ID, dbstring);

            if (jury != null)
            {
                //PlayerCart cart = GetCart();
                //cart.AddPlayer(player);
                JCart.AddJury(jury);
                //SaveCart(Cart);
            }

            //return RedirectToAction("JuriesCart", new { returnUrl });
            return RedirectToAction("AddEvent", "Event");
        }

        public RedirectToActionResult RemoveJuryFromCart(Guid ID)
        {
            Jury jury = _juryService.GetJury(ID, dbstring);

            if (jury != null)
            {
                //PlayerCart cart = GetCart();
                //cart.RemoveLine(player);
                JCart.RemoveLine(jury);
                //SaveCart(Cart);
            }

            return RedirectToAction("AddEvent", "Event");
        }
    }
}
