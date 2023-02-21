using System;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using LAB01_ED1_G.Models;
using LAB01_ED1_G.Models.Data;
using System.Diagnostics;
using System.Numerics;
using Microsoft.VisualBasic.FileIO;

namespace LAB01_ED1_G.Controllers
{
    public class DoubleController : Controller  // el double controller es para los jugadores se utiliza la lista doblemente
    {
        public static int i = 0;
        public static string log = "";
        public Stopwatch cronometro2= new Stopwatch();
        public static bool Acceso = true;

        private IWebHostEnvironment Environment;

        public void Log(string Texto)
        {
            Texto = Texto + ". Tiempo: " + cronometro2.ElapsedMilliseconds + " Milisegundos \n";
            string RutaTXT = @"Tiempos.txt";

            System.IO.File.AppendAllText(RutaTXT, Texto);
        }

        public DoubleController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            return View(Singleton.Instance1.JugadorDList);
        }

        [HttpPost]
        public ActionResult Index(IFormFile postedFile)
        {
            try
            {
                string Nombre = "", Apellido = "", Rol = "", Equipo = "";
                decimal KDA = 0;
                int Creep_Score = 0;
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

                        //csvFile.CommentTokens = new string[] { "#" };
                        csvFile.SetDelimiters(new string[] { "," });
                        csvFile.HasFieldsEnclosedInQuotes = true;

                        csvFile.ReadLine();

                        while (!csvFile.EndOfData)
                        {
                            string[] fields = csvFile.ReadFields();
                            Nombre = Convert.ToString(fields[0]);
                            Apellido = Convert.ToString(fields[1]);
                            Rol = Convert.ToString(fields[2]);
                            KDA = Convert.ToDecimal(fields[3]);
                            Creep_Score = Convert.ToInt32(fields[4]);
                            Equipo = Convert.ToString(fields[5]);
                            var NewJugador = new jugador
                            {

                                Nombre = Nombre,
                                Apellido = Apellido,
                                Rol = Rol,
                                KDA = KDA,
                                CreepScore = Creep_Score,
                                Equipo = Equipo,
                                ID = i++
                            };
                            Log("Lista de Jugadores");
                            cronometro2.Restart();
                            Singleton.Instance1.JugadorDList.Push(NewJugador);
                            cronometro2.Stop();
                            Log("Se Creo Un Jugador");
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
            try
            {
                var NewJugador = new Models.jugador
                {


                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Rol = collection["Rol"],
                    KDA = Convert.ToDecimal(collection["KDA"]),
                    CreepScore = Convert.ToInt32(collection["CreepScore"]),
                    Equipo = collection["Equipo"],
                    ID = i++

                };
                Singleton.Instance1.JugadorDList.Push(NewJugador);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult busqueda_double(string filtro_equipo, string valor)
        {
            if (filtro_equipo == "Nombre")
            {
                try
                {
                    cronometro2.Restart();
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Nombre == valor).ToList();
                    Log("Busqueda Por Nombre Del Jugador");
                    cronometro2.Stop();
                    Log("Se encontro jugador por nombre");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {

                    return View();
                }

            }
            else if (filtro_equipo == "Apellido")
            {
                try
                {
                    cronometro2.Restart();
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Apellido == valor).ToList();
                    Log("Busqueda Por Apellido Del Jugador");
                    cronometro2.Stop();
                    Log("Se encontro al jugador por apellido");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                }

            }
            else if (filtro_equipo == "NombreCompleto")
            {
                try
                {
                    cronometro2.Restart();
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => (p.Nombre+" "+ p.Apellido) == valor).ToList();
                    Log("Busqueda Por Nombre Completo Del Jugador");
                    cronometro2.Stop();
                    Log("Se encontro al jugador por Nombre Completo");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                }

            }
            else if (filtro_equipo == "Rol")
            {
                try
                {
                    cronometro2.Restart();
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Rol == valor).ToList();
                    Log("Busqueda Por Rol Del Jugador");
                    cronometro2.Stop();
                    Log("Se encontro al jugador por rol");
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                    throw;
                }

            }
            else if (filtro_equipo == "KDA")
            {
                try
                {
                    cronometro2.Restart();
                    decimal valorDecimalKDA;
                    if (decimal.TryParse(valor, out valorDecimalKDA))
                    {
                        var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.KDA == decimal.Parse(valor)).ToList();
                        Log("Busqueda Por KDA Del Jugador");
                        cronometro2.Stop();
                        Log("Se encontro al jugador por KDA");
                        return View(valorFiltrado);
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception)
                {
                    return View();
                }


            }
            else if (filtro_equipo == "CreepScore")
            {
                try
                {
                    cronometro2.Restart();
                    int valorIntCS;
                    if (int.TryParse(valor, out valorIntCS))
                    {
                        var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.CreepScore == int.Parse(valor)).ToList();
                        Log("Busqueda Por CreepScore Del Jugador");
                        cronometro2.Stop();
                        Log("Se encontro al jugador por CreepScore");
                        return View(valorFiltrado);
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception)
                {
                    return View();
                }
            }
            else if (filtro_equipo == "Equipo")
            {
                try
                {
                    cronometro2.Restart();
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Equipo == valor.ToUpper()).ToList();
                    Log("Busqueda Por Nombre Del Equipo");
                    cronometro2.Stop();
                    Log("Se encontraron los siguientes jugadores en su equipo");
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
        public ActionResult Edit(int id, string newRol, string newTeam)
        {
            foreach (var player in Singleton.Instance1.JugadorDList)
            {
                if (player.ID == id)
                {
                    if (newRol != "Selecciona el rol aqui")
                    {
                        player.Rol = newRol;
                    }
                    if (newTeam != null)
                    {
                        player.Equipo = newTeam;
                    }
                }
            }
            if (newRol == "Selecciona el rol aqui" && newTeam == null)
            {
                Log("No se ingresaron nuevos valores");
            }
            return View();
        }
    }
}
