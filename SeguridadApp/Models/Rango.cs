using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguridadApp.Models
{
    public class Rango
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Agente> Agentes { get; set; }
    }
}