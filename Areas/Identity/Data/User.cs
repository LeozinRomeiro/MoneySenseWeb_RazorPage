using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;

namespace MoneySenseWeb.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string UserLastName { get; set; }
    public string FullName { 
        get{
            return UserName + " " + UserLastName;
        }
    }
}

