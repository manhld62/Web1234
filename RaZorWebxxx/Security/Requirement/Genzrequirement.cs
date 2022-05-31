using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaZorWebxxx.Security.Requirement
{
    public class Genzrequirement :IAuthorizationRequirement
    {
        public Genzrequirement(int fromyear=1997,int toyear = 2012)
        {
            FromYear = fromyear;
            Toyear = toyear;
        }
        public int FromYear { set; get; }
        public int Toyear { set; get; }

    }

}
