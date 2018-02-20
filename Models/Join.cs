
namespace wedding_planner.Models
{
    public class Join{
        public int JoinId{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int WeddingId{get;set;}
        public Wedding Wedding{get;set;}

    }
}