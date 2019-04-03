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
        //string connectionString = "Data Source = .; Initial Catalog = SeguridadDpto; Integrated Security = true"; esta conexion se pasa en el helper
        CultureInfo culture = new CultureInfo("en-US"); //Para manejar el formato de la fecha cuando la parseo
        SQLDataAccessHelper helper = new SQLDataAccessHelper();


        // GET: Agentes
        public ActionResult Index()
        {
            #region Codigo anterior 
            // Con este codigo asignaba valores a la clase desde la ROW; no me funcino para pasar la row como modelo
            // quedo' mejorado luego de usar el metodo .Select de una lista

            //agente.Apellido1 = dt.Rows[0][1].ToString();
            //agente.Apellido2 = dt.Rows[0][2].ToString();
            //agente.Nombres = dt.Rows[0][3].ToString();
            //agente.Cedula =Convert.ToInt64(dt.Rows[0][4].ToString());
            //agente.Rango = dt.Rows[0][5].ToString();
            //agente.FechaNacimiento =DateTime.Parse(dt.Rows[0][6].ToString(), culture);
            //agente.Telefono = Convert.ToInt64(dt.Rows[0][7].ToString());
            //agente.Foto = dt.Rows[0][8].ToString();

            #endregion

            #region Antes de usar el Helper para acceder a la BD

            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionString);


            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AGENTES JOIN RANGOS on Agentes.RangoId = Rangos.Id", conn);
            //da.Fill(dt);
            //List<AgenteRangoViewModel> agente = dt.Rows.OfType<DataRow>().Select(x => new AgenteRangoViewModel()

            #endregion

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
            #region Ya no uso esta conexion; lo hago con el helper(SQLDataAcessHelper.cs)
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionString);
            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RANGOS", conn);
            //da.Fill(dt); 
            #endregion

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
            #region Codigo antes del helper
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = conn.CreateCommand();
            //    cmd.CommandType = CommandType.Text;

            //    cmd.Parameters.AddWithValue("@Apellido1", newApellido1);
            //    cmd.Parameters.AddWithValue("@Apellido2", newApellido2);
            //    cmd.Parameters.AddWithValue("@Nombres", newNombres);
            //    cmd.Parameters.AddWithValue("@Cedula", newCedula);
            //    cmd.Parameters.AddWithValue("@Rango", newRangoId);
            //    cmd.Parameters.AddWithValue("@FechaNacimiento", newFechaNacimiento);
            //    cmd.Parameters.AddWithValue("@Telefono", newTelefono);
            //    cmd.Parameters.AddWithValue("@Foto", newFoto);

            //    cmd.CommandText = @"INSERT INTO Agentes (Apellido1, Apellido2, Nombres, Cedula, RangoId, FechaNacimiento, Telefono, Foto ) 
            //                                    VALUES(@Apellido1, @Apellido2, @Nombres, @Cedula, @Rango, @FechaNacimiento, @Telefono,@Foto) ";

            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //} 
            #endregion


            return RedirectToAction("Index");
        }


        public ActionResult Editar(int id)
        {
            #region Codigo antes del helper de acceso a la base de datos
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionString);
            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AGENTES WHERE Id = " + id + "", conn);
            //da.Fill(dt);

            //DataTable dtRangos = new DataTable();

            //SqlDataAdapter daRangos = new SqlDataAdapter("SELECT * FROM RANGOS", conn);
            //daRangos.Fill(dtRangos);

            #endregion

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

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    SqlCommand cmd = conn.CreateCommand();
            //    cmd.CommandType = CommandType.Text;

            //    cmd.Parameters.AddWithValue("@Apellido1", newApellido1);
            //    cmd.Parameters.AddWithValue("@Apellido2", newApellido2);
            //    cmd.Parameters.AddWithValue("@Nombres", newNombres);
            //    cmd.Parameters.AddWithValue("@Cedula", newCedula);
            //    cmd.Parameters.AddWithValue("@Rango", newRangoId);
            //    cmd.Parameters.AddWithValue("@FechaNacimiento", newFechaNacimiento);
            //    cmd.Parameters.AddWithValue("@Telefono", newTelefono);
            //    cmd.Parameters.AddWithValue("@Foto", newFoto);

            //    cmd.CommandText = @"UPDATE Agentes SET Apellido1 = @Apellido1, Apellido2 = @Apellido2, Nombres = @Nombres, Cedula = @Cedula,
            //                                                        RangoId = @Rango, FechaNacimiento = @FechaNacimiento, Telefono =@Telefono,
            //                                                        Foto = @Foto WHERE ID = '" + id + "'";

            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //}


            return RedirectToAction("Index");
        }

        public ActionResult Detalle(int id)
        {

            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection(connectionString);


            //conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AGENTES a JOIN RANGOS r on a.RangoId = r.Id WHERE a.Id = " + id + "", conn);
            //da.Fill(dt);

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