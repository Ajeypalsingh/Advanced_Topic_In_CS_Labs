using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace BankManagement.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BankManagementUser class
public class BankUser : IdentityUser
{
    public HashSet<Account> accounts = new HashSet<Account>();
}

