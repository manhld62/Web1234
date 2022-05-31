using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RaZorWebxxx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RaZorWebxxx.Security.Requirement
{
    public class AppAuthorizehandle : IAuthorizationHandler
    {
        private readonly ILogger<AppAuthorizehandle> _logger;
        private readonly UserManager<AppUser> _usermanager;
        public AppAuthorizehandle(ILogger<AppAuthorizehandle>logger ,UserManager<AppUser> usermanager)
        {
            _logger = logger;
            _usermanager = usermanager;
        }
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var requirements = context.PendingRequirements.ToList();
            foreach(var requirement in requirements)
            {
                if(requirement is Genzrequirement)
                {
                    if (Isgenz(context.User, (Genzrequirement)requirement))
                        context.Succeed(requirement);

                }
                if (requirement is Articlerequirement)
                {
                   bool canupdate= CanUpdateArticle(context.User, context.Resource, (Articlerequirement)(requirement));

                }

            }
            return Task.CompletedTask;
        }

        private bool CanUpdateArticle(ClaimsPrincipal user, object resource, Articlerequirement requirement)
        {
            if (user.IsInRole("Admin"))
            {
                _logger.LogInformation("Admin cap nhat");
                return true;
            }
            var article = resource as Article;
            var datecreated = article.Created;
            var datecanupdate = new DateTime(requirement.Year,requirement.Mounth, requirement.Date);
            if (datecreated <= datecanupdate)
            {
                _logger.LogInformation("qua ngay cap nhat");
                return false;
            }
            return true;
        }

        private bool Isgenz(ClaimsPrincipal user, Genzrequirement requirement)
        {
            var appUserTask = _usermanager.GetUserAsync(user);
            Task.WaitAll(appUserTask);
            var appUser = appUserTask.Result;

            if (appUser.BirthDate == null)
            {
                _logger.LogInformation($"{appUser.UserName} khong co ngay sinh ,khong thoa man genzrequirement");
                return false;
            }
            int year = appUser.BirthDate.Value.Year;
            var success = (year >= requirement.FromYear && year <= requirement.Toyear);
            if (success)
            {
                _logger.LogInformation($"{appUser.UserName} thoa man genzrequirement");

            }
            else
            {
                _logger.LogInformation($"{appUser.UserName} khong thoa man genzrequirement");
            }
            return success;

        }
    }
}
