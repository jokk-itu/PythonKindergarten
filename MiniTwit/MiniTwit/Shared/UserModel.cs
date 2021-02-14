using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniTwit
{
    public class UserModel : PageModel
    {
        public bool UserIsLoggedIn = true;
    }
}