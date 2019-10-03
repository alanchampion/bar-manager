using BarManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarManager.Hubs
{
    [Authorize]
    public class IngredientHub : Hub
    {
        private readonly BarManager.Models.BarManagerContext _context;
        private readonly ILogger _logger;
        private readonly Util _util;
        private string _userId { get; set; }

        public IngredientHub(BarManager.Models.BarManagerContext context, ILogger<IngredientHub> logger)
        {
            _context = context;
            _logger = logger;
            _util = new Util(logger);
        }

        /*public override Task OnConnectedAsync()
        {
            _userId = Context.UserIdentifier;
            Console.WriteLine(_userId);

            return base.OnConnectedAsync();
        }*/

        public async Task UpdateIngredientOwned(string stringId, string stringOwned)
        {
            // await Clients.All.SendAsync("UpdateIngredientOwned", user, id, owned);
            _userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var id = Int32.Parse(stringId);
                var owned = Boolean.Parse(stringOwned);
                // System.Console.WriteLine(_userId + ", " + id + ", " + owned);
                // Console.WriteLine(userId);
                // Console.WriteLine("User id: " + _userId);
                Ingredient ingredient = await _context.Ingredient.FirstOrDefaultAsync(m => m.IngredientID == id && m.User == _userId);
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

        public async Task UpdateFavorite(string stringId, string stringFavorite)
        {
            // await Clients.All.SendAsync("UpdateIngredientOwned", user, id, owned);
            _userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var id = Int32.Parse(stringId);
                var favorite = Boolean.Parse(stringFavorite);
                // System.Console.WriteLine(_userId + ", " + id + ", " + owned);
                // Console.WriteLine(userId);
                // Console.WriteLine("User id: " + _userId);
                Ingredient ingredient = await _context.Ingredient.FirstOrDefaultAsync(m => m.IngredientID == id && m.User == _userId);
                ingredient.Favorite = favorite;

                _context.Attach(ingredient).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (FormatException)
            {
                _logger.LogError("Unable to mark ingredient id " + stringId + " as un/favorite. Unable to parse id.");
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("Error trying to access database for ingredient id " + stringId);
            }
        }
    }
}

