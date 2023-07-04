using liquidador_web.Interfaces;
using liquidador_web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace liquidador_web.ViewComponents
{
    public class TasasBox : ViewComponent
    {
        private readonly IConsultas _consultas;

        public TasasBox(IConsultas _consultas) {
            this._consultas = _consultas;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            List<Datasainte> tasas = new List<Datasainte>();

            tasas.Add(await _consultas.Get_LastItem("USM"));
            tasas.Add(await _consultas.Get_LastItem("IPC"));
            tasas.Add(await _consultas.Get_LastItem("IBC"));
            tasas.Add(await _consultas.Get_LastItem("MIC"));
            tasas.Add(await _consultas.Get_LastItem("DTF"));
            tasas.Add(await _consultas.Get_LastItem("UVR"));

            return View(tasas);
        }
    }
}
