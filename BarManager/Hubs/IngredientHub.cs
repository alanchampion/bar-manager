using BarManager.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BarManager.Hubs
{
    public class IngredientHub : Hub
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger _logger;
        private readonly Util _util;

        public IngredientHub(BarManager.Models.BarManagerContext context, ILogger<IngredientHub> logger)
        {
            _context = context;
            _logger = logger;
            _util = new Util(logger);
        }

        public async Task UpdateIngredientOwned(string user, string stringId, string stringOwned)
        {
            // await Clients.All.SendAsync("UpdateIngredientOwned", user, id, owned);
            try
            {
                var id = Int32.Parse(stringId);
                var owned = Boolean.Parse(stringOwned);
                System.Console.WriteLine(user + ", " + id + ", " + owned);
                Ingredient ingredient = await _context.Ingredient.FirstOrDefaultAsync(m => m.IngredientID == id);
                ingredient.Owned = owned;

                _context.Attach(ingredient).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            } 
            catch (FormatException)
            {
                _logger.LogError("Unable to mark ingredient id " + stringId + " as owned. Unable to parse id.");
            } 
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("Error trying to access database for ingredient id " + stringId);
            }
        }
    }
}

