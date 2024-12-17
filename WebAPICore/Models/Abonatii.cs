using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICore.Models
{
    public class Abonatii
    {
        [Key]
        public int AbonatiiId { get; set; }

        public int ClientId { get; set; }
        public int AbonamentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataEnd { get; set; }
        public int NrUtilizari { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataUtilizarii { get; set; }
    }
}
