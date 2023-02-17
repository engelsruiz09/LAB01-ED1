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

namespace LAB01_ED1_G.Controllers
{
    public class DoubleController : Controller
    {
        public static int i = 0;
        public static string log = "";
        Stopwatch stopWatch = new Stopwatch();
        public static string SName = "";
        public static string SLName = "";
        public static decimal? SPay = 0;
        public static string SClub = "";
        public static int Id = 0;
        public static jugador IDFinder = new jugador();
        public static string PayFinder = "";

        private IWebHostEnvironment Environment;

        public DoubleController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            return View(Singleton.Instance1.PlayerDList);
        }

        [HttpPost]
        public ActionResult Index(IFormFile postedFile)
        {
            stopWatch.Reset();
            stopWatch.Start();
            string Club = "", LName = "", Name = "", Position = "";
            Decimal Salary = 0, compensacion = 0;
            if (postedFile != null)
            {
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                string csvData = System.IO.File.ReadAllText(filePath);
                bool firstRow = true;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (firstRow)
                            {
                                firstRow = false;
                            }
                            else
                            {
                                int y = 0;
                                foreach (string cell in row.Split(','))
                                {
                                    if (y == 0)
                                    {
                                        Club = cell.Trim();
                                        y++;
                                    }
                                    else if (y == 1)
                                    {
                                        LName = cell.Trim();
                                        y++;
                                    }
                                    else if (y == 2)
                                    {
                                        Name = cell.Trim();
                                        y++;
                                    }
                                    else if (y == 3)
                                    {
                                        Position = cell.Trim();
                                        y++;
                                    }
                                    else if (y == 4)
                                    {
                                        Salary = Convert.ToDecimal(cell.Trim());
                                        y++;
                                    }
                                    else
                                    {
                                        compensacion = Convert.ToDecimal(cell.Trim());
                                        var newPlayer = new jugador
                                        {
                                            club = Club,
                                            apellido = LName,
                                            nombre = Name,
                                            posicion = Position,
                                            salario = Salary,
                                            compensacion = compensacion,
                                            id = i++
                                        };
                                        Singleton.Instance1.PlayerDList.Push(newPlayer);
                                    }
                                }
                            }
                        }
                    }
                }
                //stopWatch.Stop();
                //log += "[CSV Upload] - " + Convert.ToString(stopWatch.Elapsed) + '\n';
                return View(Singleton._instance1.PlayerDList);
            }
            //stopWatch.Stop();
            return View(Singleton._instance1.PlayerDList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            stopWatch.Reset();
            stopWatch.Start();
            try
            {
                var newPlayer = new Models.jugador
                {
                    club = collection["club"],
                    apellido = collection["apellido"],
                    nombre = collection["nombre"],
                    posicion = collection["posicion"],
                    salario = Convert.ToInt32(collection["salario"]),
                    compensacion = Convert.ToInt32(collection["compensacion"]),
                    id = i++
                };
                Singleton.Instance1.PlayerDList.Push(newPlayer);
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
        /*public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var DeletePlayer = Singleton.Instance.PlayerDList.Find(x => x.id == id);
                int pos = Singleton.Instance.PlayerDList.IndexOf(DeletePlayer);
                Singleton.Instance.PlayerDList.RemoveAt(pos);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
