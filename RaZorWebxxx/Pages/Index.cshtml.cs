using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RaZorWebxxx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaZorWebxxx.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyBlogContext Mycontext;

        public IndexModel(ILogger<IndexModel> logger, MyBlogContext _Mycontext)
        {
            _logger = logger;
            Mycontext = _Mycontext;
        }

        public void OnGet()
        {
            var posts = (from a in Mycontext.articles
                         orderby a.Created descending
                         select a).ToList();
            ViewData["posts"] = posts;
                       

        }
    }
}
