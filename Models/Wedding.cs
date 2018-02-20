using System;
using System.Collections.Generic;

namespace wedding_planner.Models
{
    public class Wedding{
        public int WeddingId{get;set;}
        public string WedderOne {get;set;}
        public string WedderTwo{get;set;}
        public DateTime Date{get;set;}
        public string Address{get;set;}
        public int UserId{get;set;}
        public List<Join> Users{get;set;}

        public Wedding(){
            Users=new List<Join>();
        }
    }

}