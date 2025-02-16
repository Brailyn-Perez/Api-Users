
namespace Api_Users.DAL.Core
{
    public class BaseEntity
    {
        public BaseEntity() 
        {
            IsDeleted = false;
            CreationDate = DateTime.Now;
        }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
