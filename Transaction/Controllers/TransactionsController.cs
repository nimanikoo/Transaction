using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transaction.Models;

namespace Transaction.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
              return _context.Transactions != null ? 
                          View(await _context.Transactions.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Transactions'  is null.");
        }

       
        // GET: Transactions/Create
        public IActionResult AddOrEditEntity(int id=0)
        {
            if (id == 0)
            {
                return View(new TransactionsModel());
            }
            else{
                return View(_context.Transactions.Find(id));
            }
            
        }

        // POST: Transactions/AddOrEditEntity
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditEntity([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SwiftCode,Amount,Date")] TransactionsModel transactionsModel)
        {
            if (ModelState.IsValid)
            {
                if (transactionsModel.TransactionId == 0)
                {
                    transactionsModel.Date = DateTime.Now;
                    _context.Add(transactionsModel);
                } else
                _context.Update(transactionsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionsModel);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactionsModel = await _context.Transactions.FindAsync(id);
            if (transactionsModel == null)
            {
                return NotFound();
            }
            return View(transactionsModel);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SwiftCode,Amount,Date")] TransactionsModel transactionsModel)
        {
            if (id != transactionsModel.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (transactionsModel.TransactionId==null)
                    {
                        
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transactionsModel);
        }

   
      

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
            var Selecttransaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(Selecttransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
    }
}
