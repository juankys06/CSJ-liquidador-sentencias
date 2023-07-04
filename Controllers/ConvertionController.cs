using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using liquidador_web.Models;
using liquidador_web.Interfaces;
using System.Threading.Tasks;

namespace liquidador_web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ConvertionController : Controller
    {
        private readonly IConsultas _consultas;
        public ConvertionController(IConsultas consultas)
        {
            _consultas = consultas ;
        }

        public IActionResult Tasas_Interes()
        {
            ViewData["Title"] = "Tasas de Interéses";
            ViewData["Message"] = "Conversión de Tasas de Intereses";

            return View();
        }

        [HttpPost]
        public IActionResult Tasas_Interes(double pInteresEA, double pPeriodosA, string periodo)
        {
            double EA = pInteresEA / 100;
            double ea = 0, ea2 = 0 ;

            if (periodo.CompareTo("mensual") == 0)
            {
                ea = Math.Pow((double)1 + EA, (double)1 / (double)12) - (double)1; // Mensual
                ea2 = Math.Pow((double)1 + EA, (double)1 / pPeriodosA) - (double)1; // Diario
            }
            else if (periodo.CompareTo("diario") == 0)
            {
                ea2 = Math.Pow((double)1 + EA, (double)1 / (double)30) - (double)1;
            }
            else
                ea = Math.Pow( (double)1 + EA / (double)12 , (double)12) - (double)1;

            var result = new { EA = ea*100 , EA2 = ea2*100 , P=periodo};

            return Json(result);
        }

        public async Task<IActionResult> DolarToCOP()
        {
            ViewData["Title"] = "Dólares a Pesos";
            ViewData["Message"] = "Convertir Dólares a Pesos";

            var temp = await _consultas.Get_LastItem("USM");

            if (temp == null)
                return Content($"Vacio");
            else
                return View(temp.VigenteHasta);
        }

        [HttpPost]
        public async Task<IActionResult> DolarToCOP(double Cantidad, DateTime Fecha, string Moneda)
        {
            if (Fecha.CompareTo(DateTime.Parse("1/1/1754")) <= 0)
                return NoContent();
            else
            {
                var result = await _consultas.CalcTasa("USM", Fecha);

                if (result != null)
                    if (Moneda == "pesos")
                        return Content($"{Cantidad * (double)result.ValorTasa}");
                    else
                        return Content($"{Cantidad / (double)result.ValorTasa}");
                else
                    return NoContent();
            }
        }

        public async Task<IActionResult> UVR_Pesos()
        {
            ViewData["Title"] = "UVR a Pesos";
            ViewData["Message"] = "Convertir UVR a Pesos";

            var temp = await _consultas.Get_LastItem("UVR");

            if (temp == null)
                return Content($"Vacio");
            else
                return View(temp.VigenteHasta);
        }

        [HttpPost]
        public async Task<IActionResult> UVR_Pesos(decimal Cantidad, DateTime Fecha, string Moneda)
        {
            if (Fecha.CompareTo(DateTime.Parse("1/1/1754")) <= 0)
                return NoContent();
            else
            {
                var result = await _consultas.CalcTasa("UVR", Fecha);

                if (result != null)
                    if (Moneda == "pesos")
                        return Json(new { conversion = Cantidad * (decimal)result.ValorTasa, tasa = result.ValorTasa });
                    else
                        return Json(new { conversion = Cantidad / (decimal)result.ValorTasa, tasa = result.ValorTasa });
                else
                    return NoContent();
            }
        }

        public async Task<IActionResult> UPAC_Pesos()
        {
            ViewData["Title"] = "UPAC  Pesos";
            ViewData["Message"] = "Convertir UPAC a Pesos";

            var temp = await _consultas.Get_LastItem("UPAC");

            if (temp == null)
                return Content($"Vacio");
            else
                return View(temp.VigenteHasta);
        }

        [HttpPost]
        public async Task<IActionResult> UPAC_Pesos(double Cantidad, DateTime Fecha, string Moneda)
        {
            if (Fecha.CompareTo(DateTime.Parse("1/1/1754")) <= 0)
                return NoContent();
            else
            {
                var result = await _consultas.CalcTasa("UPAC", Fecha);

                if (result != null)
                    if (Moneda == "pesos")
                        return Content($"{Cantidad * (double)result.ValorTasa}");
                    else
                        return Content($"{Cantidad / (double)result.ValorTasa}");
                else
                    return NoContent();
            }
        }

        public IActionResult UVR_UPAC()
        {
            ViewData["Title"] = "UVR a UPAC";
            ViewData["Message"] = "Convertir UVR a UPAC";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
