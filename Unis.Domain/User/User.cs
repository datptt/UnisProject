using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unis.Domain
{
    [Table("user")]
    public class User : AuditEntity<long>
    {
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
