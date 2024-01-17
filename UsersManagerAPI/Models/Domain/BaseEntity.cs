using System.ComponentModel.DataAnnotations;

namespace ClientRegistryAPI.Models.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifyDate { get; set; }

        public BaseEntity()
        {
            CreationDate = LastModifyDate = DateTime.Now;
        }

        protected void RefreshLastModifyDate()
        {
            LastModifyDate = DateTime.Now;
        }
    }
}
