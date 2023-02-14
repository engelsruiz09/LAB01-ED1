﻿using System;
using System.IO;
using System.Data;
using LAB01_ED1_G.Models;
using LAB01_ED1_G.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace LAB01_ED1_G.Controllers
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
            return View(Singleton._instance.PlayerList);
        }

        [HttpPost]
        public ActionResult Index(IFormFile postedFile)
        {
            //stopWatch.Reset();
            //stopWatch.Start();
            string Club = "", LName = "", Name = "", Position = "";
            Decimal Salary = 0, Compensation = 0;
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
                                        Compensation = Convert.ToDecimal(cell.Trim());
                                        var newPlayer = new jugador
                                        {
                                            club = Club,
                                            apellido = LName,
                                            nombre = Name,
                                            posicion = Position,
                                            salario = Salary,
                                            compesation = Compensation,
                                            id = i++
                                        };
                                        Singleton.Instance.PlayerList.Add(newPlayer);
                                    }
                                }
                            }
                        }
                    }
                }
                //stopWatch.Stop();
                //log += "[CSV Upload] - " + Convert.ToString(stopWatch.Elapsed) + '\n';
                return View(Singleton._instance.PlayerList);
            }
             //stopWatch.Stop();
            return View(Singleton._instance.PlayerList);
        }
    }
}