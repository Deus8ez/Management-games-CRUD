using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using kubNew.Models;
using Microsoft.Extensions.Configuration;

namespace kubNew.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly string dbstring;
        private IEventRepository repository;
        public NavigationMenuViewComponent(IConfiguration configuration, IEventRepository repo)
        {
            dbstring = configuration.GetConnectionString("connString");
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.FetchEvents(dbstring)
            .Select(x => x.Location)
            .Distinct()
            .OrderBy(x => x));
        }

        //public string Invoke()
        //{
        //    return "Hello from the Nav View Component";
        //}
    }
}
