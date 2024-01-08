using System;
using System.Collections.Generic;

namespace ProyectoCore.Models
{
    public partial class RendimientoGerenteTecnico
    {
        public byte IdRendimientoGerenteTecnico { get; set; }
        public byte IdProyeccionAnual { get; set; }
        public byte IdExportacion { get; set; }
        public byte IdTrabajador { get; set; }
        public int Cantidad { get; set; }

        public virtual Exportacion IdExportacionNavigation { get; set; }
        public virtual ProyeccionAnual IdProyeccionAnualNavigation { get; set; }
        public virtual Trabajador IdTrabajadorNavigation { get; set; }
    }
}
