using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RaZorWebxxx.Models;

namespace RaZorWebxxx.Pages.Blog
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RaZorWebxxx.Models.MyBlogContext _context;

        public IndexModel(RaZorWebxxx.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }
        public const int Itempage = 10;
        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentpage { set; get; }
        public int countpage { set; get; }

        public async Task OnGetAsync(string search)
            
        {
            
            int totalarticle = await _context.articles.CountAsync();
            Article = await _context.articles.ToListAsync();
            countpage = (int)Math.Ceiling((double)totalarticle / Itempage);
            if (currentpage < 1)
                currentpage = 1;
            if (currentpage > countpage)
                currentpage = countpage;
           
            var qr=(from a in _context.articles
                   orderby a.Created descending
                   select a).Skip((currentpage-1)*10)
                   .Take(Itempage)
                   ;
            if (!string.IsNullOrEmpty(search))
            {
                Article= qr.Where(a => a.Title.Contains(search)).ToList();
            }
            else
            {
                Article = await qr.ToListAsync();

            }
          

        }
    }
}
