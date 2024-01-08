using System;
using System.Collections.Generic;

namespace ProyectoCore.Models
{
    public partial class ProyeccionAnual
    {
        public byte IdProyeccionAnual { get; set; }
        public byte IdExportacion { get; set; }
        public byte IdTrabajador { get; set; }
        public int Cantidad { get; set; }

        public virtual Exportacion IdExportacionNavigation { get; set; }
        public virtual Trabajador IdTrabajadorNavigation { get; set; }
    }
}
