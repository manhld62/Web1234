using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaZorWebxxx.Security.Requirement
{
 
    public class Articlerequirement : IAuthorizationRequirement
    {
        public Articlerequirement(int year=2021,int mounth=6,int date=30)
        {
            Year = year;
            Mounth = mounth;
            Date = date;
        }
        public int Year { set; get;}
        public int Mounth { set; get; }
        public int Date { set; get; }
    }

}
