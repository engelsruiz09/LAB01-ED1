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
        private IWebHostEnvironment Environment;

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

                            Singleton.Instance1.JugadorDList.Push(NewJugador);

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
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Nombre == valor).ToList();
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
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Apellido == valor).ToList();
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
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Rol == valor).ToList();
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
                    decimal valorDecimalKDA;
                    if (decimal.TryParse(valor, out valorDecimalKDA))
                    {
                        var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.KDA == decimal.Parse(valor)).ToList();
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
                    int valorIntCS;
                    if (int.TryParse(valor, out valorIntCS))
                    {
                        var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.CreepScore == int.Parse(valor)).ToList();
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
                    var valorFiltrado = Singleton.Instance1.JugadorDList.Where(p => p.Equipo == valor.ToUpper()).ToList();
                    return View(valorFiltrado);
                }
                catch (Exception)
                {
                    return View();
                }

            }
            return View();
        }
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int pos = Singleton.Instance1.JugadorDList.Find2(x => x.ID == id);
                Singleton.Instance1.JugadorDList.RemoveAt(pos);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
