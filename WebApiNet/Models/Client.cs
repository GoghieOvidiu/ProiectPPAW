using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiNet.Models
{
    [Serializable]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        public string Nume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataStart { get; set; }
    }
}