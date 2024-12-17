
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebAPICore.Models
{
    public class TipAbonament
    {
        [Key]
        public int AbonamentId { get; set; }

        public string? Tip { get; set; }
        public int PretLuna { get; set; }
        public int PretAn { get; set; }
        public int NrUtilizari { get; set; }
    }
}
