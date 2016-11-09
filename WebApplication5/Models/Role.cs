using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication5.Models
{
    public class Role
    {
        public Role()
        {
        }

        //[Key]
        //[ForeignKey("Users")]
        public long RoleId { get; set; }
        [StringLength(65)]
        [Index(IsUnique = true)]
        public string RoleName { get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}