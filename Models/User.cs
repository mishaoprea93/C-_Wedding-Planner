using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace wedding_planner.Models{
    public class User{
        public int UserId{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        
        public string Email{get;set;}
        public string Password{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}
        public List<Join> Weddings { get; set; }

        public User(){
            Weddings=new List<Join>();
            CreatedAt=DateTime.Now;
            UpdatedAt=DateTime.Now;
        }
    }
}
