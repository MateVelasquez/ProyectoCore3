using System;
using System.Collections.Generic;

namespace ProyectoCore.Models
{
    public partial class Trabajador
    {
        public Trabajador()
        {
            Ascensos = new HashSet<Ascenso>();
            Exportacions = new HashSet<Exportacion>();
            ProductoQuimicos = new HashSet<ProductoQuimico>();
            ProyeccionAnuals = new HashSet<ProyeccionAnual>();
            RendimientoGerenteTecnicos = new HashSet<RendimientoGerenteTecnico>();
            TallosCosechados = new HashSet<TallosCosechado>();
        }

        public byte IdTrabajador { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaContratacion { get; set; }
        public byte AreaTrabajo { get; set; }

        public virtual ICollection<Ascenso> Ascensos { get; set; }
        public virtual ICollection<Exportacion> Exportacions { get; set; }
        public virtual ICollection<ProductoQuimico> ProductoQuimicos { get; set; }
        public virtual ICollection<ProyeccionAnual> ProyeccionAnuals { get; set; }
        public virtual ICollection<RendimientoGerenteTecnico> RendimientoGerenteTecnicos { get; set; }
        public virtual ICollection<TallosCosechado> TallosCosechados { get; set; }
    }
}
