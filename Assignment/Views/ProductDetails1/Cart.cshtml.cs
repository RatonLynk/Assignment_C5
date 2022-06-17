using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;

namespace Assignment.Views.ProductDetails1
{
    public class CartModel : PageModel
    {
        private readonly Assignment.Models.C5_AssignmentContext _context;

        public CartModel(Assignment.Models.C5_AssignmentContext context)
        {
            _context = context;
        }

        public IList<CartDetail> CartDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CartDetails != null)
            {
                CartDetail = await _context.CartDetails
                .Include(c => c.Cart)
                .Include(c => c.Product).ToListAsync();
            }
        }
    }
}
