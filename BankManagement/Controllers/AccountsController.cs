using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankManagement.Data;
using BankManagement.Models;
using BankManagement.Areas.Identity.Data;

namespace BankManagement.Controllers
{
    public class AccountsController : Controller
    {
        private readonly BankManagementContext _context;

        public AccountsController(BankManagementContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            BankUser user = _context.BankUsers.FirstOrDefault(b => b.UserName == User.Identity.Name);
            ViewBag.userId = user?.Id;

            var accounts = _context.Accounts.Where(a => a.BankUserId == user.Id).ToList();

            return View(accounts);
        }


        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.BankUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create(string userId)
        {
            ViewBag.userId = userId;
            Account account = new Account
            {
                BankUserId = userId,
            };
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InitialBalance,BankUserId")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Accounts");
            }
            ViewBag.userId = account.BankUserId;
            return View(account);
        }

        public IActionResult Transaction(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            Account account = _context.Accounts.FirstOrDefault(a => a.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        public IActionResult PerformTransaction(int? id, string submitbtn, int transactionAmount)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            Account account = _context.Accounts.FirstOrDefault(a => a.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            if (submitbtn == "Deposit")
            {
                account.InitialBalance += transactionAmount;
            }
            else if (submitbtn == "Withdraw")
            {
                if (transactionAmount <= account.InitialBalance)
                {
                    account.InitialBalance -= transactionAmount;
                }
                else
                {
                    ModelState.AddModelError("transactionAmount", "Insufficient balance.");
                    return View("Transaction", account);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Transaction", new {id = id}); // Redirect to an appropriate page
        }



        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.BankUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'BankManagementContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
