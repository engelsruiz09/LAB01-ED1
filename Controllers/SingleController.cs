using System;
using System.IO;
using System.Data;
using LAB01_ED1_G.Models;
using LAB01_ED1_G.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using System.Drawing;
using System.Globalization;

namespace LAB01_ED1_G.Controllers //el single controller es con la lista normal y es para equipos
{
    public class SingleController : Controller
    {

        public static int i = 0;
        public static string log = "";
        Stopwatch stopWatch = new Stopwatch();
        public Stopwatch cronometro = new Stopwatch();
        public static bool Acceso = true;

        private IWebHostEnvironment Environment;

        public void Log(string Texto)
        {
            Texto = Texto + ". Y Tardo: " + cronometro.ElapsedMilliseconds + " Milisegundos \n \n";
            string RutaTXT = @"Tiempos.txt";
            System.IO.File.AppendAllText(RutaTXT, Texto);
        }

        public SingleController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            if (Acceso)
            {
                System.IO.File.WriteAllText(@"Tiempos.txt", "-------TIEMPOS DE LAS EJECUCIONES PRINCIPALES DEL PROGRAMA-------  \n \n");
                Acceso = false;
            }
            return View(Singleton._instance.EquipoList);
        }

        [HttpPost]
        public ActionResult Index(IFormFile postedFile)
        {
            string NombreEquipo = "", Coach = "", Liga = "";
            DateTime FechaCreacion;
            try
            {
                if (postedFile != null)
                {
                    string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string FileName = Path.GetFileName(postedFile.FileName);
                    string FilePath = Path.Combine(path, FileName);
                    using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }
                    using (TextFieldParser csvFile = new TextFieldParser(FilePath))
                    {

                        csvFile.CommentTokens = new string[] { "#" };
                        csvFile.SetDelimiters(new string[] { "," });
                        csvFile.HasFieldsEnclosedInQuotes = true;

                        csvFile.ReadLine();

                        while (!csvFile.EndOfData)
                        {
                            string[] fields = csvFile.ReadFields();
                            NombreEquipo = Convert.ToString(fields[0]);
                            Coach = Convert.ToString(fields[1]);
                            Liga = Convert.ToString(fields[2]);
                            FechaCreacion = Convert.ToDateTime(fields[3]);

                            var NewTeam = new equipo
                            {

                                NombreEquipo = NombreEquipo,
                                Coach = Coach,
                                Liga = Liga,
                                FechaCreacion = FechaCreacion,
                                ID = i++
                            };
                            Log("Lista de Equipos");
                            cronometro.Start();
                            Singleton.Instance.EquipoList.Add(NewTeam);
                            cronometro.Stop();
                            Log("Se Creo Un Equipo");

                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["Message"] = "Algo sucedio mal";
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            //stopWatch.Reset();
            //stopWatch.Start();
            try
            {
                var newEquip = new Models.equipo
                {
                    NombreEquipo = collection["NombreEquipo"],
                    Coach = collection["Coach"],
                    Liga = collection["Liga"],
                    FechaCreacion = Convert.ToDateTime(collection["FechaCreacion"]),
                    ID = i++
                };
                Singleton.Instance.EquipoList.Add(newEquip);
                //stopWatch.Stop();
                //log += "[Create] - " + Convert.ToString(stopWatch.Elapsed) + '\n';
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                stopWatch.Stop();
                return View();
            }
        }
        public ActionResult busqueda_single(string filtro_equipo, string valor)
        {
            List<equipo> listaRetorno = new List<equipo>();
            if (filtro_equipo == "nombreEquipo")
            {
                try
                {
                    cronometro.Restart();
                    var valorFiltrado = Singleton.Instance.EquipoList.Where(p => p.NombreEquipo == valor).ToList();
                    Log("Busqueda Por Nombre Del Equipo");
                    cronometro.Stop();
                    Log("Se encontro al equipo por su nombre");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                }

            }
            else if (filtro_equipo == "coachEquipo")
            {
                try
                {
                    cronometro.Restart();
                    var valorFiltrado = Singleton.Instance.EquipoList.Where(p => p.Coach == valor).ToList();
                    Log("Busqueda Por Coach Del Equipo");
                    cronometro.Stop();
                    Log("Se encontro al equipo por coach");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                }

            }
            else if (filtro_equipo == "ligaEquipo")
            {
                try
                {
                    cronometro.Restart();
                    var valorFiltrado = Singleton.Instance.EquipoList.Where(p => p.Liga == valor).ToList();
                    Log("Busqueda Por Liga Del Equipo");
                    cronometro.Stop();
                    Log("Se encontro los siguientes equipor por su liga");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                }

            }
            else if (filtro_equipo == "fechaCreacion")
            {
                try
                {
                    cronometro.Restart();
                    var valorFiltrado = Singleton.Instance.EquipoList.Where(p => p.FechaCreacion == DateTime.ParseExact(valor, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
                    Log("Busqueda Por Fecha Creacion Del Equipo");
                    cronometro.Stop();
                    Log("Se encontro al equipo por su fecha de creacion");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {

                    return View();
                }

            }
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, string newCoach, string newLeague)
        {
            cronometro.Restart();
            Log("Modificacion de equipo");

            foreach (var team in Singleton.Instance.EquipoList)
            {
                if(team.ID == id)
                {
                    if (newCoach != null)
                    {
                        team.Coach = newCoach;
                    }
                    if (newLeague != null)
                    {
                        team.Liga = newLeague;
                    }
                    cronometro.Stop();
                    Log("Se modifico al equipo por sus parametros respectivos");
                    return View();
                }
            }
            return View();
        }
        public ActionResult Delete(int id, IFormCollection collection)
        {
            cronometro.Restart();
            Log("Eliminacion de Equipo");
            try
            {
                var DeletePlayer = Singleton.Instance.EquipoList.Find(x => x.ID == id);
                int pos = Singleton.Instance.EquipoList.IndexOf(DeletePlayer);
                Singleton.Instance.EquipoList.RemoveAt(pos);
                cronometro.Stop();
                Log("Se elimino al equipo elegido");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
