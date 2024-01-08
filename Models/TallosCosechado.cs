using System;
using System.Collections.Generic;

namespace ProyectoCore.Models
{
    public partial class TallosCosechado
    {
        public byte IdTallosCosechados { get; set; }
        public byte IdTrabajador { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCosecha { get; set; }

        public virtual Trabajador IdTrabajadorNavigation { get; set; }
    }
}
