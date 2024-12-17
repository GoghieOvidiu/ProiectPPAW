using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    [Serializable]
    public class Abonatii
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public virtual Client Client { get; set; }
        public virtual TipAbonament TipAbonament { get; set; }
    }
}
