using System;
using System.Collections.Generic;

namespace ProyectoCore.Models
{
    public partial class Exportacion
    {
        public Exportacion()
        {
            ProductoQuimicos = new HashSet<ProductoQuimico>();
            ProyeccionAnuals = new HashSet<ProyeccionAnual>();
            RendimientoGerenteTecnicos = new HashSet<RendimientoGerenteTecnico>();
        }

        public byte IdExportacion { get; set; }
        public byte IdTrabajador { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaExportacion { get; set; }

        public virtual Trabajador IdTrabajadorNavigation { get; set; }
        public virtual ICollection<ProductoQuimico> ProductoQuimicos { get; set; }
        public virtual ICollection<ProyeccionAnual> ProyeccionAnuals { get; set; }
        public virtual ICollection<RendimientoGerenteTecnico> RendimientoGerenteTecnicos { get; set; }
    }
}
