using SeguridadApp.Helpers;
using SeguridadApp.Models;
using SeguridadApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SeguridadApp.Controllers
{
    public class AgentesController : Controller
    {
        CultureInfo culture = new CultureInfo("en-US"); //Para manejar el formato de la fecha cuando la parseo
        SQLDataAccessHelper helper = new SQLDataAccessHelper();


        // GET: Agentes
        public ActionResult Index()
        {
            var lista =  helper.executeQuery("SELECT * FROM AGENTES JOIN RANGOS on Agentes.RangoId = Rangos.Id",CommandType.Text,null);
            List<AgenteRangoViewModel> agente = lista.Rows.OfType<DataRow>().Select( x => new AgenteRangoViewModel()
            {
                AgenteId = Convert.ToInt16(x[0].ToString()),
                RangoId = Convert.ToInt16(x[5].ToString()),
                Apellido1 = x[1].ToString(),
                Apellido2 = x[2].ToString(),
                Nombres = x[3].ToString(),
                Cedula = Convert.ToInt64(x[4].ToString()),
                DescripcionRango = x[10].ToString(),
                Telefono = Convert.ToInt64(x[7].ToString()),
                Foto = "/Fotos/" + x[3].ToString() + ".jpg",
            }).ToList();

         

            return View(agente);
        }

        public ActionResult Create()
        { 

            var listaRangos = helper.executeQuery("SELECT * FROM RANGOS", CommandType.Text,null);
            
            List <Rango> Rangos = listaRangos.Rows.OfType<DataRow>().Select(x => new Rango()
            {
                Id = int.Parse(x[0].ToString()),
                Descripcion = x[1].ToString()

            }).ToList();
            ViewBag.RangoId = new SelectList(Rangos, "Id", "Descripcion");

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {

            var newFoto = form["uploadFoto"].ToString();
            var newApellido1 = form["Apellido1"];
            var newApellido2 = form["Apellido2"];
            var newNombres = form["nombres"];
            var newCedula = Convert.ToInt64(form["cedula"]);
            var newRangoId = form["RangoId"];
            var newFechaNacimiento = DateTime.Parse(form["fechaNacimiento"], culture);
            var newTelefono = Convert.ToInt64(form["telefono"]);

            SqlParameter[] parameters = {

                             new SqlParameter("@Apellido1", newApellido1),
                             new SqlParameter("@Apellido2", newApellido2),
                             new SqlParameter("@Nombres", newNombres),
                             new SqlParameter("@Cedula", newCedula),
                             new SqlParameter("@Rango", newRangoId),
                             new SqlParameter("@FechaNacimiento", newFechaNacimiento),
                             new SqlParameter("@Telefono", newTelefono),
                             new SqlParameter("@Foto", newFoto)
                           
                    };

            helper.executeNonQuery(@"INSERT INTO Agentes (Apellido1, Apellido2, Nombres, Cedula, RangoId, FechaNacimiento, Telefono, Foto ) 
                                                VALUES(@Apellido1, @Apellido2, @Nombres, @Cedula, @Rango, @FechaNacimiento, @Telefono,@Foto) ",CommandType.Text, parameters);
        
            return RedirectToAction("Index");
        }


        public ActionResult Editar(int id)
        {            
            var lista = helper.executeQuery("SELECT * FROM AGENTES WHERE Id = " + id + "",CommandType.Text,null);
            Agente agente = new Agente();
            agente.Id = id;
            agente.Apellido1 =lista.Rows[0][1].ToString();
            agente.Apellido2 = lista.Rows[0][2].ToString();
            agente.Nombres = lista.Rows[0][3].ToString();
            agente.Cedula = Convert.ToInt64(lista.Rows[0][4].ToString());
            agente.RangoId = int.Parse(lista.Rows[0][5].ToString());
            agente.FechaNacimiento = DateTime.Parse(lista.Rows[0][6].ToString());
            agente.Telefono = Convert.ToInt64(lista.Rows[0][7].ToString());
            agente.Foto = "/Fotos/" + lista.Rows[0][3].ToString() + ".jpg";

            var listaRangos = helper.executeQuery("SELECT * FROM RANGOS", CommandType.Text,null);
            List<Rango> Rangos = listaRangos.Rows.OfType<DataRow>().Select(x => new Rango()
            {
                Id = int.Parse(x[0].ToString()),
                Descripcion = x[1].ToString()

            }).ToList();
            ViewBag.RangoId = new SelectList(Rangos, "Id", "Descripcion");
            return View(agente);
        }
        [HttpPost]
        public ActionResult Editar(int id, FormCollection form)
        {

            var newFoto = form["uploadFoto"].ToString();
            var newApellido1 = form["Apellido1"];
            var newApellido2 = form["Apellido2"];
            var newNombres = form["nombres"];
            var newCedula = Convert.ToInt64(form["cedula"]);
            var newRangoId = int.Parse(form["RangoId"]);
            var newFechaNacimiento = DateTime.Parse(form["fechaNacimiento"], culture);
            var newTelefono = Convert.ToInt64(form["telefono"]);

            SqlParameter[] sqlParameters = {

                             new SqlParameter("@Apellido1", newApellido1),
                             new SqlParameter("@Apellido2", newApellido2),
                             new SqlParameter("@Nombres", newNombres),
                             new SqlParameter("@Cedula", newCedula),
                             new SqlParameter("@Rango", newRangoId),
                             new SqlParameter("@FechaNacimiento", newFechaNacimiento),
                             new SqlParameter("@Telefono", newTelefono),
                             new SqlParameter("@Foto", newFoto)
            };

            helper.executeNonQuery(@"UPDATE Agentes SET Apellido1 = @Apellido1, Apellido2 = @Apellido2, Nombres = @Nombres, Cedula = @Cedula,
                                                                    RangoId = @Rango, FechaNacimiento = @FechaNacimiento, Telefono =@Telefono,
                                                                    Foto = @Foto WHERE ID = '" + id + "'",CommandType.Text, sqlParameters);

           
     
            return RedirectToAction("Index");
        }

        public ActionResult Detalle(int id)
        {
            var lista = helper.executeQuery("SELECT * FROM AGENTES a JOIN RANGOS r on a.RangoId = r.Id WHERE a.Id = " + id + "",CommandType.Text,null);
            AgenteRangoViewModel agente = new AgenteRangoViewModel();
            {
                agente.AgenteId = int.Parse(lista.Rows[0][0].ToString());
                agente.Apellido1 = lista.Rows[0][1].ToString();
                agente.Apellido2 = lista.Rows[0][2].ToString();
                agente.Nombres = lista.Rows[0][3].ToString();
                agente.Cedula = Convert.ToInt64(lista.Rows[0][4].ToString());
                agente.RangoId = int.Parse(lista.Rows[0][5].ToString());
                agente.FechaNacimiento = DateTime.Parse(lista.Rows[0][6].ToString(), culture);
                agente.Telefono = Convert.ToInt64(lista.Rows[0][7].ToString());
                agente.Foto = "/Fotos/" + lista.Rows[0][3].ToString() + ".jpg";
                agente.DescripcionRango = lista.Rows[0][10].ToString();
            };
            return View(agente);
        }
    }
}