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

namespace LAB01_ED1_G.Controllers //el single controller es con la lista normal y es para equipos
{
    public class SingleController : Controller
    {

        public static int i = 0;
        public static string log = "";
        Stopwatch stopWatch = new Stopwatch();   
        private IWebHostEnvironment Environment;

        public SingleController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
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
                            Singleton.Instance.EquipoList.Add(NewTeam);
                            
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

    }
}
