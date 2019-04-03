using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeguridadApp.ViewModels
{
    public class AgenteRangoViewModel
    {
        public int AgenteId { get; set; }
        public int RangoId { get; set; }
        public int MyProperty { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Nombres { get; set; }
        public long Cedula { get; set; }
        public long Telefono { get; set; }
        public string Foto { get; set; }
        public string DescripcionRango { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
    }
}