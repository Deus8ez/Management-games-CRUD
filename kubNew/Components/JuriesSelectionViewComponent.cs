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
    public class JuriesSelectionViewComponent : ViewComponent
    {
        private readonly string dbstring;
        private IJuryRepository repository;
        public int PageSize = 10;
        public JuriesSelectionViewComponent(IConfiguration configuration, IJuryRepository repo)
        {
            dbstring = configuration.GetConnectionString("connString");
            repository = repo;
        }

        public IViewComponentResult Invoke(int page = 1)
        {

            List<Jury> juries = repository.FetchJuries(dbstring);

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
    }
}
