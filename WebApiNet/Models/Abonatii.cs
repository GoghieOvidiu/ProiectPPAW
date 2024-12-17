using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiNet.Models
{
    [Serializable]
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