// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Threading.Tasks;
using Intex_Group3_6.Infrastructure;
using Intex_Group3_6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Intex_Group3_6.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        public IDataRepo _repo;

        public PersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger,
            IDataRepo repo)
        {
            _userManager = userManager;
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public IActionResult OnPost(User user)
        {
            User newUser = new User()
            {
                userId = user.userId,
                firstName = user.firstName,
                lastName = user.lastName,
                birthDate = user.birthDate,
                country = user.country,
                gender = user.gender,
                age = user.age,
                email = user.email,
                role = user.email
            };

            _repo.UpdateUser(newUser);
            _repo.SaveChanges();
            
            HttpContext.Session.SetJson("UserData", newUser);

            return Page();
        }
    }
}
