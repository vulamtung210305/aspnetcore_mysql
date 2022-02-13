using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Column("username")]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Column("password")]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}