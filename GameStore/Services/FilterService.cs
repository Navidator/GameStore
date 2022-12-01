using GameStore.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class FilterService
    {
        private readonly GameStoreContext _context;
        public FilterService(GameStoreContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> FilterByGameName(List<> )
        //{

        //}

        public void ValidateFilter()
        {

        }
    }
}
