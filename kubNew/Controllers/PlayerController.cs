using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using kubNew.Models;
using kubNew.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace kubNew.Controllers
{
    public class PlayerController : Controller
    {
        private readonly string dbstring;
        private IPlayerRepository _playerService;
        private PlayerCart Cart;

        public PlayerController(IConfiguration configuration, PlayerCart cartService, IPlayerRepository playerService)
        {
            dbstring = configuration.GetConnectionString("connString");
            Cart = cartService;
            _playerService = playerService;

        }

        public int PageSize = 10;

        public ViewResult Index(string rank, int page = 1)
        {
            List<Players> players = _playerService.FetchPlayers(dbstring);

            return View(new PlayerListViewModel
            {
                Players = players.Where(p => rank == null || p.ClassicRank == rank).OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = players.Count()
                }
            });
        }

        [HttpGet]
        public ViewResult AddPlayers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPlayer(Players participant)
        {
            if (ModelState.IsValid)
            {
                _playerService.AddNewPlayer(participant, dbstring);
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddPlayers");
            }
        }

        [HttpGet]
        public ViewResult GetPlayer(Guid ID)
        {
            return View(_playerService.GetPlayer(ID, dbstring));
        }

        [HttpPost]
        public IActionResult UpdatePlayer(Players participant)
        {
            _playerService.UpdatePlayer(participant, dbstring);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePlayer(Players participant)
        {
            _playerService.DeletePlayer(participant, dbstring);
            return RedirectToAction("Index");
        }

        public ViewResult PlayersCart(string returnUrl)
        {
            return View(new PlayerCartViewModel
            {
                //Cart = GetCart(),
                Cart = Cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(Guid ID)
        {
            Players player = _playerService.GetPlayer(ID, dbstring);

            if (player != null)
            {
                //PlayerCart cart = GetCart();
                //cart.AddPlayer(player);
                Cart.AddPlayer(player);
                //SaveCart(Cart);
            }
            //return RedirectToAction("PlayersCart", new { returnUrl });
            return RedirectToAction("AddEvent", "Event");
        }

        public RedirectToActionResult RemoveFromCart(Guid ID)
        {
            Players player = _playerService.GetPlayer(ID, dbstring);

            if (player != null)
            {
                //PlayerCart cart = GetCart();
                //cart.RemoveLine(player);
                Cart.RemoveLine(player);
                //SaveCart(Cart);
            }

            return RedirectToAction("AddEvent", "Event");
        }

        //private PlayerCart GetCart()
        //{
        //    PlayerCart cart = HttpContext.Session.GetJson<PlayerCart>("Cart") ?? new PlayerCart();
        //    return cart;
        //}

        //private void SaveCart(PlayerCart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}
    }
}
