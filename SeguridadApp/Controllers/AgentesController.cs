using SeguridadApp.Models;
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
        string connectionString = "Data Source = .; Initial Catalog = SeguridadDpto; Integrated Security = true";
        CultureInfo culture = new CultureInfo("en-US"); //Para manejar el formato de la fecha cuando la parseo

     
        // GET: Agentes
        public ActionResult Index()
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);


            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AGENTES", conn);
            da.Fill(dt);

            List<Agente> agente = dt.Rows.OfType<DataRow>().Select(x => new Agente()
            {
                Id = Convert.ToInt16(x[0].ToString()),
                Apellido1 = x[1].ToString(),
                Apellido2 = x[2].ToString(),
                Nombres = x[3].ToString(),
                Cedula = Convert.ToInt64(x[4].ToString()),
                Rango = x[5].ToString(),
                FechaNacimiento = DateTime.Parse(x[6].ToString(), culture),
                Telefono = Convert.ToInt64(x[7].ToString()),
                Foto = "/Fotos/" + x[3].ToString() + ".jpg"
            }).ToList();

            #region Codigo anterior con el que asignaba valores a la clase desde la ROW; no me funcino para pasar la row como modelo

            //agente.Apellido1 = dt.Rows[0][1].ToString();
            //agente.Apellido2 = dt.Rows[0][2].ToString();
            //agente.Nombres = dt.Rows[0][3].ToString();
            //agente.Cedula =Convert.ToInt64(dt.Rows[0][4].ToString());
            //agente.Rango = dt.Rows[0][5].ToString();
            //agente.FechaNacimiento =DateTime.Parse(dt.Rows[0][6].ToString(), culture);
            //agente.Telefono = Convert.ToInt64(dt.Rows[0][7].ToString());
            //agente.Foto = dt.Rows[0][8].ToString();

            #endregion

            return View(agente);
        }

        public ActionResult Create()
        {

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
            var newRango = form["rango"];
            var newFechaNacimiento = DateTime.Parse(form["fechaNacimiento"], culture);
            var newTelefono = Convert.ToInt64(form["telefono"]);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Apellido1", newApellido1);
                cmd.Parameters.AddWithValue("@Apellido2", newApellido2);
                cmd.Parameters.AddWithValue("@Nombres", newNombres);
                cmd.Parameters.AddWithValue("@Cedula", newCedula);
                cmd.Parameters.AddWithValue("@Rango", newRango);
                cmd.Parameters.AddWithValue("@FechaNacimiento", newFechaNacimiento);
                cmd.Parameters.AddWithValue("@Telefono", newTelefono);
                cmd.Parameters.AddWithValue("@Foto", newFoto);

                cmd.CommandText = @"INSERT INTO Agentes (Apellido1, Apellido2, Nombres, Cedula, Rango, FechaNacimiento, Telefono, Foto ) 
                                                VALUES(@Apellido1, @Apellido2, @Nombres, @Cedula, @Rango, @FechaNacimiento, @Telefono,@Foto) ";

                cmd.ExecuteNonQuery();
                conn.Close();
            }


            return RedirectToAction("Index");
        }


        public ActionResult Editar(int id)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AGENTES WHERE Id = " + id + "", conn);
            da.Fill(dt);
       

            Agente agente = new Agente();
            agente.Id = id;
            agente.Apellido1 = dt.Rows[0][1].ToString();
            agente.Apellido2 = dt.Rows[0][2].ToString();
            agente.Nombres = dt.Rows[0][3].ToString();
            agente.Cedula = Convert.ToInt64(dt.Rows[0][4].ToString());
            agente.Rango = dt.Rows[0][5].ToString();
            agente.FechaNacimiento = DateTime.Parse(dt.Rows[0][6].ToString());
            agente.Telefono = Convert.ToInt64(dt.Rows[0][7].ToString());
            agente.Foto = "/Fotos/" + dt.Rows[0][3].ToString() + ".jpg"; 
       
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
            var newRango = form["rango"];
            var newFechaNacimiento = DateTime.Parse(form["fechaNacimiento"], culture);
            var newTelefono = Convert.ToInt64(form["telefono"]);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Apellido1", newApellido1);
                cmd.Parameters.AddWithValue("@Apellido2", newApellido2);
                cmd.Parameters.AddWithValue("@Nombres", newNombres);
                cmd.Parameters.AddWithValue("@Cedula", newCedula);
                cmd.Parameters.AddWithValue("@Rango", newRango);
                cmd.Parameters.AddWithValue("@FechaNacimiento", newFechaNacimiento);
                cmd.Parameters.AddWithValue("@Telefono", newTelefono);
                cmd.Parameters.AddWithValue("@Foto", newFoto);

                cmd.CommandText = @"UPDATE Agentes SET Apellido1 = @Apellido1, Apellido2 = @Apellido2, Nombres = @Nombres, Cedula = @Cedula,
                                                                    Rango = @Rango, FechaNacimiento = @FechaNacimiento, Telefono =@Telefono,
                                                                    Foto = @Foto WHERE ID = '"+id+"'";

                cmd.ExecuteNonQuery();
                conn.Close();
            }


            return RedirectToAction("Index");
        }

        public ActionResult Detalle(int id)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);


            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AGENTES WHERE Id = "+id+"", conn);
            da.Fill(dt);

            Agente agente = new Agente();
            {
                agente.Apellido1 = dt.Rows[0][1].ToString();
                agente.Apellido2 = dt.Rows[0][2].ToString();
                agente.Nombres = dt.Rows[0][3].ToString();
                agente.Cedula = Convert.ToInt64(dt.Rows[0][4].ToString());
                agente.Rango = dt.Rows[0][5].ToString();
                agente.FechaNacimiento = DateTime.Parse(dt.Rows[0][6].ToString(), culture);
                agente.Telefono = Convert.ToInt64(dt.Rows[0][7].ToString());
                agente.Foto = "/Fotos/" + dt.Rows[0][3].ToString() + ".jpg";
            };
            return View(agente);
        }
    }
}