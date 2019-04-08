
using DAL.Helpers;
using SeguridadApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AgentesBL
    {
        SQLDataAccessHelper helper = new SQLDataAccessHelper();
        public void Select()
        {
            var lista = helper.executeQuery("SELECT * FROM AGENTES INNER JOIN RANGOS on Agentes.RangoId = Rangos.Id", CommandType.Text, null);
            List<AgenteRangoViewModel> agente = lista.Rows.OfType<DataRow>().Select(x => new AgenteRangoViewModel()
            {
                AgenteId = Convert.ToInt16(x[0].ToString()),
                RangoId = Convert.ToInt16(x[5].ToString()),
                Apellido1 = x[1].ToString(),
                Apellido2 = x[2].ToString(),
                Nombres = x[3].ToString(),
                Cedula = Convert.ToInt64(x[4].ToString()),
                DescripcionRango = x[10].ToString(),
                Telefono = Convert.ToInt64(x[7].ToString()),
                //Foto = x[8].ToString(),
                Foto = "/Fotos/" + x[3].ToString() + ".jpg",
            }).ToList();

        }


    }
}
