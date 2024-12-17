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
    public class TipAbonament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AbonamentId { get; set; }

        public string Tip { get; set; }
        public int PretLuna { get; set; }
        public int PretAn { get; set; }
        public int NrUtilizari { get; set; }

        public virtual ICollection<Abonatii> Abonatiis { get; set; }
    }
}
