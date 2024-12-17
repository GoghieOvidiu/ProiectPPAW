using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICore.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        public string? Nume { get; set; }
        public string? Email { get; set; }
        public string? Parola { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataStart { get; set; }
    }
}
