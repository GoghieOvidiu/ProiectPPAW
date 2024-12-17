using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiNet.Models
{
    [Serializable]
    public class TipAbonament
    {
        [Key]
        public int AbonamentId { get; set; }
        public string Tip { get; set; }
        public int PretLuna { get; set; }
        public int PretAn { get; set; }
        public int NrUtilizari { get; set; }
    }
}