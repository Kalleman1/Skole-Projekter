using BlazorIdentity.Server.Data;
using BlazorIdentity.Server.Models;
using BlazorIdentity.Shared;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorIdentity.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuperHeroController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var user = await _ctx.Users
            //    .Include(u => u.SuperHeroes)
            //    .FirstOrDefaultAsync(u=>u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user.SuperHeroes);
            }
        }
    }
}
