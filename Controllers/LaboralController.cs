using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using liquidador_web.Models;
using System;
using liquidador_web.Extra;
using System.Collections.Generic;
using liquidador_web.Interfaces;

namespace liquidador_web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrador General")]
    public class LaboralController : Controller
    {
        private readonly IConsultas _consultas;
        public LaboralController(IConsultas consultas)
        {
            _consultas = consultas;
        }

        public ActionResult CalcSalario()
        {
            ViewData["Title"] = "Calculadora Salarial";

            return View();
        }

        [HttpPost]
        public ActionResult Cesantias(double salario, DateTime fechaInicial, DateTime fechaFinal, double rendimiento = 0)
        {
            DateTime FF = DateTime.Parse("31/12/" + fechaInicial.Year);
            DateTime FI = fechaInicial;
            List<Cesantias> response = new List<Cesantias>();
            int j = 1;

            FF = FF < fechaFinal ? FF : fechaFinal;

            while (FI < fechaFinal)
            {
                double dias = LiqLaboral.CalculaDias(FI, FF);
                Cesantias temp = new Cesantias
                {
                    dias = dias,
                    numero = j,
                    fechaInicial = FI,
                    fechaFinal = FF,
                    cesantias = salario * dias / 360,
                    salarioPromedio = salario,
                    rendimiento = 0,
                };

                temp.intereses = temp.cesantias * dias * 0.12 / 360;
                FI = FF.AddDays(1);
                FF = DateTime.Parse("31/12/" + FI.Year);
                FF = FF < fechaFinal ? FF : fechaFinal;
                j++;

                response.Add(temp);
            }

            return Json(response);
        }

        [HttpPost]
        public ActionResult Primas(double salario, DateTime fechaInicial, DateTime fechaFinal)
        {
            DateTime FI = fechaInicial;
            DateTime FF;
            List<Primas> response = new List<Primas>();

            if (FI < DateTime.Parse("30/06/" + fechaInicial.Year))
                //-- Primer semestre
                FF = DateTime.Parse("30/06/" + fechaInicial.Year);
            else
                //-- Segundo Semestre
                FF = DateTime.Parse("31/12/" + fechaInicial.Year);

            FF = FF < fechaFinal ? FF : fechaFinal;
            short j = 1;

            while (FI < fechaFinal)
            {
                double dias = LiqLaboral.CalculaDias(FI, FF);
                Primas temp = new Primas {
                    primas = salario * dias / 360,
                    salarioPromedio = salario,
                    numero = j,
                    fechaInicial = FI,
                    fechaFinal = FF,
                    dias = dias,
                };

                FI = FF.AddDays(1);
                if (FI == DateTime.Parse("01/07/" + FI.Year))
                    FF = DateTime.Parse("31/12/" + FI.Year);
                else
                    FF = DateTime.Parse("30/06/" + FI.Year);
                FF = FF < fechaFinal ? FF : fechaFinal;
                j++;
                response.Add(temp);
            }

            return Json(response);
        }

        [HttpPost]
        public ActionResult Vacaciones(double salario, DateTime fechaInicial, DateTime fechaFinal)
        {
            DateTime FI = fechaInicial;
            DateTime FF;
            List<Vacaciones> response = new List<Vacaciones>();

            FI = fechaInicial;
            FF = fechaInicial.AddYears(1);
            FF = FF < fechaFinal ? FF : fechaFinal;

            short j = 1;

            while (FI < fechaFinal) {
                int dias = (int)LiqLaboral.CalculaDias(FI, FF);
                dias = 15 * dias / 360;
                Vacaciones temp = new Vacaciones {
                    vacaciones = salario * dias / 30,
                    salarioPromedio = salario,
                    numero = j,
                    fechaInicial = FI,
                    fechaFinal = FF,
                    dias = dias
                };

                FI = FF.AddDays(1);
                FF = FI.AddYears(1);
                FF = FF < fechaFinal ? FF : fechaFinal;
                j++;
                response.Add(temp);
            }

            return Json(response);
        }

        public ActionResult Moratorio()
        {
            ViewData["Title"] = "Indexaciones e Indemnizaciones Moratorias";

            return View();
        }

        [HttpPost]
        public ActionResult Moratorio(decimal salario, string liquidar, DateTime fechaInicial, DateTime fechaFinal)
        {
            if (liquidar == "indemnizacion")
            {
                List<Indemnizaciones> response = new List<Indemnizaciones>();

                var temp = DateTime.Parse("01/" + fechaInicial.Month + "/" + fechaInicial.Year);
                temp = temp.AddMonths(1).AddDays(-1);
                var UltimoMes = temp > fechaFinal ? fechaFinal : temp;
                var Meses = (Math.Abs(fechaFinal.Year - fechaInicial.Year) * 12) + fechaFinal.Month - fechaInicial.Month;

                short i = 0;
                decimal vrMoraAcumulado = 0;

                while (i < Meses + 1)
                {
                    decimal vrDia = salario / 240 * 8;
                    int diasMora = i == 0 ? (UltimoMes - fechaInicial).Days : (UltimoMes - fechaInicial).Days + 1;
                    decimal totalMora = vrDia * diasMora;
                    vrMoraAcumulado += totalMora;
                    Indemnizaciones resp;

                    i++;
                    resp.fechaInicial = fechaInicial;
                    resp.fechaFinal = UltimoMes;
                    resp.dias = diasMora;
                    resp.capital = salario;
                    resp.vrDia = vrDia;
                    resp.totalMora = totalMora;
                    resp.moraAcumulado = vrMoraAcumulado;

                    var fechaInicial2 = fechaInicial.AddMonths(1);
                    temp = DateTime.Parse("01/" + fechaInicial2.Month + "/" + fechaInicial2.Year);
                    fechaInicial = temp < fechaInicial ? fechaInicial : temp;

                    temp = DateTime.Parse("01/" + fechaInicial2.Month + "/" + fechaInicial2.Year);
                    temp = temp.AddMonths(1).AddDays(-1);
                    UltimoMes = temp > fechaFinal ? fechaFinal : temp;
                    response.Add(resp);
                }
                //-- Actualizar LABEL de la app
                return Json(response);
            }
            else if (liquidar == "indexacion")
            {
                List<Indexaciones> response = new List<Indexaciones>();

                var temp = DateTime.Parse("01/" + fechaInicial.Month + "/" + fechaInicial.Year);
                temp = temp.AddMonths(1).AddDays(-1);
                var UltimoMes = temp > fechaFinal ? fechaFinal : temp;
                var Meses = (Math.Abs(fechaFinal.Year - fechaInicial.Year) * 12) + fechaFinal.Month - fechaInicial.Month;

                short i = 0;

                while (i < Meses + 1)
                {
                    UltimoMes = UltimoMes.AddMonths(1);
                    UltimoMes = DateTime.Parse("01/" + UltimoMes.Month + "/" + UltimoMes.Year);
                    double ipcInicial = _consultas.CalcTasa("IPC", fechaInicial).Result.ValorTasa ?? 0;
                    double ipcFinal = _consultas.CalcTasa("IPC", UltimoMes).Result.ValorTasa ?? 0;
                    decimal correccion, vr, vrIndexado;
                    Indexaciones resp;

                    if (ipcInicial == 0 || ipcFinal == 0 || salario == 0)
                    {
                        correccion = 0;
                        vr = 0;
                    }
                    else
                    {
                        vr = (decimal)ipcFinal * salario / (decimal)ipcInicial;
                        correccion = vr - salario;
                    }
                    vrIndexado = vr;

                    i++;
                    resp.fechaInicial = fechaInicial;
                    resp.fechaFinal = UltimoMes;
                    resp.ipcInicial = ipcInicial;
                    resp.ipcFinal = ipcFinal;
                    resp.indexado = vrIndexado;
                    resp.correccion = correccion;

                    response.Add(resp);

                    var fechaInicial2 = fechaInicial.AddMonths(1);
                    temp = DateTime.Parse("01/" + fechaInicial2.Month + "/" + fechaInicial2.Year);
                    fechaInicial = temp < fechaInicial ? fechaInicial : temp;

                    temp = DateTime.Parse("01/" + fechaInicial2.Month + "/" + fechaInicial2.Year);
                    temp = temp.AddMonths(1).AddDays(-1);
                    UltimoMes = temp > fechaFinal ? fechaFinal : temp;
                }
                return Json(response);
            }
            else
                return NoContent();
        }

        public ActionResult Pension_ISS()
        {
            ViewData["Title"] = "Pensiones_ISS";

            return View();
        }

        [HttpPost]
        public ActionResult Pension_ISS(DateTime fechaNacimiento, DateTime fechaLiquidacion, int liquidar, string sexo)
        {
            if (liquidar == 1) { //-- Pensión de vejez
                return Content("");
            }else
                return NoContent();
        }

        [HttpPost]
        public ActionResult Cotizaciones(DateTime fechaInicio, DateTime fechaFinal, double ingresos) {
            List<Cotizacion> response = new List<Cotizacion>();
            DateTime FF, f_inicio = fechaInicio;
            int dias, cont = 0;

            var temp = DateTime.Parse("01/" + fechaInicio.Month + "/" + fechaInicio.Year);
            temp = temp.AddMonths(1).AddDays(-1);
            var UltimoMes = temp > fechaFinal ? fechaFinal : temp;

            do
            {
                Cotizacion resp;
                FF = UltimoMes.AddDays(1);
                dias = cont == 0 ? (UltimoMes - fechaInicio).Days : (UltimoMes - fechaInicio).Days + 1;

                resp.fechaInicial = fechaInicio;
                resp.fechaFinal = UltimoMes;
                resp.salario = ingresos;
                resp.valorDia = (decimal)ingresos / 30;
                resp.dias = FF.Month == 2 ? (dias >= 28 ? 30 : dias) : (dias >= 30 ? 30 : dias);
                resp.basePeriodo = resp.valorDia * resp.dias;

                var fechaInicial2 = fechaInicio.AddMonths(1);
                temp = DateTime.Parse("01/" + fechaInicial2.Month + "/" + fechaInicial2.Year);
                fechaInicio = temp < fechaInicio ? fechaInicio : temp;

                temp = DateTime.Parse("01/" + fechaInicial2.Month + "/" + fechaInicial2.Year);
                temp = temp.AddMonths(1).AddDays(-1);
                UltimoMes = temp > fechaFinal ? fechaFinal : temp;

                response.Add(resp);
                cont++;
            } while ( FF < fechaFinal );

            return Json(response);
        }


        [HttpPost]
        public ActionResult ObtenerTasaIPC(DateTime fecha, string moneda)
        {
            return Json(_consultas.CalcTasa(moneda, fecha).Result.ValorTasa);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}