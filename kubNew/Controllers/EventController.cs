using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kubNew.Models;
using kubNew.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using kubNew.Infrastructure;

namespace kubNew.Controllers
{
    public class EventController : Controller
    {
        private readonly string dbstring;

        private IEventRepository _eventService;
        private IPlayerRepository _playerService;
        private IJuryRepository _juryService;
        private JuryCart JCart;
        private PlayerCart Cart;

        public EventController(IConfiguration configuration, IEventRepository eventService, IPlayerRepository playerService, IJuryRepository juryService, PlayerCart cartService, JuryCart jcartService)
        {
            dbstring = configuration.GetConnectionString("connString");
            _eventService = eventService;
            _playerService = playerService;
            _juryService = juryService;
            Cart = cartService;
            JCart = jcartService;
        }

        public int PageSize = 10;

        [HttpGet]
        public IActionResult Index(string location, int page = 1)
        {
            return View(new EventListViewModel
            {
                Events = _eventService.FetchEvents(dbstring)
                .Where(p => location == null || p.Location == location)
                .OrderBy(p => p.ID).Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _eventService.FetchEvents(dbstring).Count()
                }
            });
        }

        [HttpGet]
        public ViewResult AddEvent()
        {
            //List<Players> players = _playerService.FetchPlayers(dbstring);
            //List<Jury> juries = _juryService.FetchJuries(dbstring);

            //return View(new MasterListViewModel()
            //{
            //    Event = null,
            //    Players = players.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
            //    Juries = juries.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
            //    PagingInfoPlayers = new PagingInfo
            //    {
            //        CurrentPage = page,
            //        ItemsPerPage = PageSize,
            //        TotalItems = players.Count()
            //    },
            //    PagingInfoJuries = new PagingInfo
            //    {
            //        CurrentPage = page,
            //        ItemsPerPage = PageSize,
            //        TotalItems = juries.Count()
            //    }
            //});
            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _eventService.AddNewEvent(newEvent, dbstring, Cart.Lines, JCart.Lines);
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddEvent");
            }
        }

        [HttpGet]
        public ViewResult GetEvent(Guid ID, int page = 1)
        {
            List<Players> playersList = _playerService.FetchPlayers(dbstring);

            List<Jury> juriesList = _juryService.FetchJuries(dbstring);

            Event anEvent = _eventService.GetEvent(ID, dbstring);

            return View(new MasterListViewModel()
            {
                Event = anEvent,
                Players = playersList.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                Juries = juriesList.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfoPlayers = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = playersList.Count()
                },
                PagingInfoJuries = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = juriesList.Count()
                }
            });
        }


        [HttpPost]
        public IActionResult UpdateEvent(Event anEvent)
        {
            _eventService.UpdateEvent(anEvent, dbstring);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteEvent(Event anEvent)
        {
            _eventService.DeleteEvent(anEvent, dbstring);
            return RedirectToAction("Index");
        }

        

    }
}
