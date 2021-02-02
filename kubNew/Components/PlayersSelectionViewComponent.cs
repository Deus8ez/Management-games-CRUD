using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using kubNew.Models;
using Microsoft.Extensions.Configuration;
using kubNew.Models.ViewModels;

namespace kubNew.Components
{
    public class PlayersSelectionViewComponent : ViewComponent
    {
        private readonly string dbstring;
        private IPlayerRepository repository;
        public int PageSize = 10;
        public PlayersSelectionViewComponent(IConfiguration configuration, IPlayerRepository repo)
        {
            dbstring = configuration.GetConnectionString("connString");
            repository = repo;
        }

        public IViewComponentResult Invoke(int page = 1)
        {

            List<Players> players = repository.FetchPlayers(dbstring);

            return View(new PlayerListViewModel
            {
                Players = players.OrderBy(p => p.ID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = players.Count()
                }
            });

        }
    }
}
