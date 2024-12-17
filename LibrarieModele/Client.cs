using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    [Serializable]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataStart { get; set; }

        public virtual ICollection<Abonatii> Abonatiis { get; set; }
    }
}
