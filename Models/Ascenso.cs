using System;
using System.Collections.Generic;

namespace ProyectoCore.Models
{
    public partial class Ascenso
    {
        public byte IdAscenso { get; set; }
        public byte IdTrabajador { get; set; }
        public string Sustentacion { get; set; }

        public virtual Trabajador IdTrabajadorNavigation { get; set; }
    }
}
