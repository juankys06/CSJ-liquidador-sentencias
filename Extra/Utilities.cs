using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using QRCoder;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Shapes;
using Color = MigraDoc.DocumentObjectModel.Color;
using ClosedXML.Excel;

namespace liquidador_web.Extra
{
    public static class Utilities
    {
        /// <summary>
        /// Crea un documento en excel basado en la tabla arrojada en la liquidación singular, o cuotas de administración.
        /// </summary>
        /// <param name="liquidacion">Tabla de la liquidación</param>
        /// <param name="resumen">Resumen de la liquidacion</param>
        /// <returns>Documento de excel con la información de la liquidación.</returns>
        public static MemoryStream CreateExcelDoc(List<Liquidacion> liquidacion, List<(string, double)> resumen, string observaciones)
        {
            MemoryStream memoryStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();
            workbook.Author = "Liquidador de Sentencias Judiciales Web";
            var workSheet = workbook.Worksheets.Add("Liquidación Intereses Plazo y M");
            int i = 5;

            workSheet.Range("A1:D1").Merge().SetValue("República de Colombia").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:D2").Merge().SetValue("Consejo Superior de la Judicatura").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A3:D3").Merge().SetValue("RAMA JUDICIAL").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            workSheet.Cell("A5").SetValue("Desde (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("B5").SetValue("Hasta (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("C5").SetValue("NoDías").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("D5").SetValue("Tasa Anual").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("E5").SetValue("Tasa Máxima").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("F5").SetValue("IntAplicado").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("G5").SetValue("InterésEfectivo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("H5").SetValue("Capital").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("I5").SetValue("CapitalALiquidar").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("J5").SetValue("IntPlazoPeríodo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("K5").SetValue("SaldoIntPlazo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("L5").SetValue("InteresMoraPeríodo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("M5").SetValue("SaldoIntMora").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("N5").SetValue("Abonos").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("O5").SetValue("SubTotal").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            foreach (Liquidacion element in liquidacion)
            {
                workSheet.Cell(++i, 1).SetValue(element.Desde).Style.DateFormat.SetFormat("dd/mm/yyyy").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(i, 2).SetValue(element.Hasta).Style.DateFormat.SetFormat("dd/mm/yyyy").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(i, 3).Value = element.NoDias;
                workSheet.Cell(i, 4).Value = element.TasaAnual;
                workSheet.Cell(i, 5).Value = element.TasaMaxima;
                workSheet.Cell(i, 6).Value = element.intAplicado;
                workSheet.Cell(i, 7).Value = element.InteresNominal;
                workSheet.Cell(i, 8).SetValue(element.capital).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 9).SetValue(element.CapitalALiquidar).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 10).SetValue(element.intPlazoPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 11).SetValue(element.saldoInteresPlazoAcum).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 12).SetValue(element.InteresMoraPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 13).SetValue(element.interesAdeudadoMoraAcum).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 14).SetValue(element.abonos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 15).SetValue(element.subTotal).Style.NumberFormat.Format = "$ #,##0.00";
            }

            i += 2;
            workSheet.Cell(i, 1).SetValue("Asunto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell(i, 2).SetValue("Valor").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            foreach ((string, double) element in resumen)
            {
                workSheet.Cell(++i, 1).Value = element.Item1;
                workSheet.Cell(i, 2).SetValue(element.Item2).Style.NumberFormat.Format = "$ #,##0.00";
            }

            i += 2;
            
            workSheet.Range("A" + i + ":F" + i).Merge().SetValue("Observaciones:").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A" + (i+1) + ":F" + (i+1)).Merge().SetValue(observaciones);

            workSheet.Columns(1, 18).AdjustToContents(); //-- Ajusta el tamaño de las columnas, a su contenido.

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        /// <summary>
        /// Crea un documento en excel basado en la tabla arrojada en la liquidación por crédito hipotecario en pesos.
        /// </summary>
        /// <param name="liquidacion">Tabla de la liquidación</param>
        /// <param name="resumen">Resumen de la liquidacion</param>
        /// <returns>Documento de excel con la información de la liquidación.</returns>
        public static MemoryStream CreateExcelDocHipotecario(List<Liquidacion> liquidacion, List<(string, double)> resumen, string observaciones, out string fileName)
        {
            MemoryStream memoryStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();
            workbook.Author = "Liquidador de Sentencias Judiciales Web";
            var workSheet = workbook.Worksheets.Add("Liquidación Intereses Plazo y M");
            int i = 8;

            workSheet.Range("A1:D1").Merge().SetValue("República de Colombia").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:D2").Merge().SetValue("Consejo Superior de la Judicatura").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A3:D3").Merge().SetValue("RAMA JUDICIAL").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            workSheet.AddPicture(GenImage(liquidacion.ToString() + DateTime.Now.ToString(), out fileName)).MoveTo(workSheet.Cell("F1")).Scale(0.12);
            
            workSheet.Cell("A8").SetValue("Desde (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("B8").SetValue("Hasta (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("C8").SetValue("NoDías").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("D8").SetValue("Tasa Remuneratoria").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("E8").SetValue("Tasa Mora").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("F8").SetValue("Interés Aplicado").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("G8").SetValue("Interés Diario Aplicado").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("H8").SetValue("Capital").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("I8").SetValue("CapitalALiquidar").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("J8").SetValue("Abonos").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("K8").SetValue("Abono a Capital").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("L8").SetValue("Abono Interés Mora").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("M8").SetValue("Abono Interés Plazo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("N8").SetValue("Int Plazo Período").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("O8").SetValue("Interés Mora Período").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("P8").SetValue("Saldo Int Plazo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("Q8").SetValue("Saldo Int Mora").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("R8").SetValue("SubTotal").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            foreach (Liquidacion element in liquidacion)
            {
                workSheet.Cell(++i, 1).SetValue(element.Desde).Style.DateFormat.Format = "dd/mm/yyyy";
                workSheet.Cell(i, 2).SetValue(element.Hasta).Style.DateFormat.Format = "dd/mm/yyyy";
                workSheet.Cell(i, 3).Value = element.NoDias;
                workSheet.Cell(i, 4).Value = element.TasaAnual;
                workSheet.Cell(i, 5).Value = element.TasaMaxima;
                workSheet.Cell(i, 6).Value = element.intAplicado;
                workSheet.Cell(i, 7).Value = element.InteresNominal;
                workSheet.Cell(i, 8).Value = element.capital;
                workSheet.Cell(i, 9).Value = element.CapitalALiquidar;
                workSheet.Cell(i, 10).SetValue(element.abonos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 11).SetValue(element.abonoCapitalUVR).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 12).SetValue(element.interesAdeudadoMoraAcum).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 13).SetValue(element.abonoIntCtePesos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 14).SetValue(element.intPlazoPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 15).SetValue(element.InteresMoraPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 16).SetValue(element.saldoInteresPlazoAcum).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 17).SetValue(element.saldoInteresMoraPesos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 18).SetValue(element.subTotal).Style.NumberFormat.Format = "$ #,##0.00";
            }

            i += 2;
            workSheet.Cell(i, 1).SetValue("Asunto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell(i, 2).SetValue("Valor").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            foreach ((string, double) element in resumen)
            {
                workSheet.Cell(++i, 1).Value = element.Item1;
                workSheet.Cell(i, 2).SetValue(element.Item2).Style.NumberFormat.Format = "$ #,##0.00";
            }

            i += 2;

            workSheet.Range("A" + i + ":F" + i).Merge().SetValue("Observaciones:").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A" + (i + 1) + ":F" + (i + 1)).Merge().SetValue(observaciones);

            workSheet.Columns(1, 18).AdjustToContents(); //-- Ajusta el tamaño de las columnas, a su contenido.

            workbook.SaveAs(Directory.GetCurrentDirectory() + "\\wwwroot\\assets\\pdfDocuments\\" + fileName + ".xlsx");

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        /// <summary>
        /// Crea un documento en excel basado en la tabla arrojada en el crédito hipotecario en UVR.
        /// </summary>
        /// <param name="liquidacion">Tabla de la Liquidación</param>
        /// <param name="resumen">Resumen de la liquidación</param>
        /// <returns>Documento de excel con la información de la liquidación</returns>
        public static MemoryStream CreateExcelDocHipotecarioUVR(List<Liquidacion> liquidacion, List<(string, double, double)> resumen, string observaciones, out string fileName)
        {
            MemoryStream memoryStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();
            workbook.Author = "Liquidador de Sentencias Judiciales Web";
            var workSheet = workbook.Worksheets.Add("Liquidación Intereses Plazo y M");
            int i = 8;

            workSheet.Range("A1:D1").Merge().SetValue("República de Colombia").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:D2").Merge().SetValue("Consejo Superior de la Judicatura").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A3:D3").Merge().SetValue("RAMA JUDICIAL").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            workSheet.AddPicture(GenImage(liquidacion.ToString() + DateTime.Now.ToString(), out fileName)).MoveTo(workSheet.Cell("F1")).Scale(0.12);

            workSheet.Cell("A8").SetValue("Capital UVR").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("B8").SetValue("Capital a Liquidar UVR").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("C8").SetValue("Desde (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("D8").SetValue("Hasta (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("E8").SetValue("NoDías").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("F8").SetValue("Interés de Plazo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("G8").SetValue("Interés Mora").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("H8").SetValue("Interés Aplicado").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("I8").SetValue("Interés Diario Aplicado").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("J8").SetValue("Valor UVR").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("K8").SetValue("Abonos $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("L8").SetValue("Abono a capital en UVR").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("M8").SetValue("Abono Int Plazo $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("N8").SetValue("Abono a Int Mora $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("O8").SetValue("Capital $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("P8").SetValue("Interés Plazo Período $$$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("Q8").SetValue("SaldoIntPlazo $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("R8").SetValue("Interés Mora Período").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("S8").SetValue("Saldo Interés Mora $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("T8").SetValue("Total $$").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            foreach (Liquidacion element in liquidacion)
            {
                i++;
                workSheet.Cell(i, 1).SetValue(element.capital).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 2).SetValue(element.CapitalALiquidar).Style.NumberFormat.Format = "#,##0.00";
                workSheet.Cell(i, 3).SetValue(element.Desde).Style.DateFormat.Format = "dd/mm/yyyy";
                workSheet.Cell(i, 4).SetValue(element.Hasta).Style.DateFormat.Format = "dd/mm/yyyy";
                workSheet.Cell(i, 5).Value = element.NoDias;
                workSheet.Cell(i, 6).Value = element.TasaAnual;
                workSheet.Cell(i, 7).Value = element.TasaMaxima;
                workSheet.Cell(i, 8).Value = element.intAplicado;
                workSheet.Cell(i, 9).SetValue(element.InteresNominal).Style.NumberFormat.Format = "#,##0.00000 %"; ;
                workSheet.Cell(i, 10).SetValue(element.valorUVR).Style.NumberFormat.Format = "#,##0.00";
                workSheet.Cell(i, 11).SetValue(element.abonos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 12).SetValue(element.abonoCapitalUVR).Style.NumberFormat.Format = "#,##0.00";
                workSheet.Cell(i, 13).SetValue(element.abonoIntCtePesos).Style.NumberFormat.Format = "#,##0.00";
                workSheet.Cell(i, 14).SetValue(element.interesAdeudadoMoraAcum).Style.NumberFormat.Format = "#,##0.00";
                workSheet.Cell(i, 15).SetValue(element.capitalPesos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 16).SetValue(element.intPlazoPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 17).SetValue(element.saldoInteresPlazoAcum).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 18).SetValue(element.interesMoraPeriodoPesos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 19).SetValue(element.saldoInteresMoraPesos).Style.NumberFormat.Format = "$ #,##0.00";
                workSheet.Cell(i, 20).SetValue(element.totalPesos).Style.NumberFormat.Format = "$ #,##0.00";
            }

            i += 2;
            workSheet.Cell(i, 1).SetValue("Descripción").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell(i, 2).SetValue("UVR").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell(i, 3).SetValue("Pesos").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            foreach ((string, double, double) rw in resumen)
            {
                workSheet.Cell(++i, 1).Value = rw.Item1;
                workSheet.Cell(i, 2).SetValue(rw.Item2).Style.NumberFormat.Format = "#,##0.00";
                workSheet.Cell(i, 3).SetValue(rw.Item3).Style.NumberFormat.Format = "$ #,##0.00";
            }

            i += 2;

            workSheet.Range("A" + i + ":F" + i).Merge().SetValue("Observaciones:").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A" + (i + 1) + ":F" + (i + 1)).Merge().SetValue(observaciones);

            workSheet.Columns(1, 19).AdjustToContents(); //-- Ajusta el tamaño de las columnas, a su contenido.

            workbook.SaveAs(Directory.GetCurrentDirectory() + "\\wwwroot\\assets\\pdfDocuments\\" + fileName + ".xlsx");

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        /// <summary>
        /// Crea un documento excel con la tabla para cada una de las liquidaciones realizadas.
        /// </summary>
        /// <param name="liquidaciones">Array con las tablas de las liquidaciones efectuadas.</param>
        /// <param name="resumenes">Array con los resúmenes de las liquidaciones efectuadas</param>
        /// <returns>Documento de excel con la data de las liquidaciones efectuadas.</returns>
        public static MemoryStream CreateExcelDoc(List<Liquidacion>[] liquidaciones, List<(string, double)>[] resumenes, string[] observaciones)
        {
            MemoryStream memoryStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();
            workbook.Author = "Liquidador de Sentencias Judiciales Web";
            var workSheet = workbook.Worksheets.Add("Liquidación Intereses Plazo y M");
            int index = 5;

            workSheet.Range("A1:C1").Merge().SetValue("República de Colombia").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:D2").Merge().SetValue("Consejo Superior de la Judicatura").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A3:B3").Merge().SetValue("RAMA JUDICIAL").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            for (int i = 0; i < liquidaciones.Length; i++)
            {
                workSheet.Cell(index, 1).SetValue("Desde (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 2).SetValue("Hasta (dd/mm/aaaa)").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 3).SetValue("NoDías").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 4).SetValue("Tasa Anual").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 5).SetValue("Tasa Máxima").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 6).SetValue("IntAplicado").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 7).SetValue("InterésEfectivo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 8).SetValue("Capital").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 9).SetValue("CapitalALiquidar").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 10).SetValue("IntPlazoPeríodo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 11).SetValue("SaldoIntPlazo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 12).SetValue("InteresMoraPeríodo").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 13).SetValue("SaldoIntMora").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 14).SetValue("Abonos").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 15).SetValue("SubTotal").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                foreach (Liquidacion element in liquidaciones[i])
                {
                    workSheet.Cell(++index, 1).SetValue(element.Desde).Style.DateFormat.SetFormat("dd/mm/yyyy").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    workSheet.Cell(index, 2).SetValue(element.Hasta).Style.DateFormat.SetFormat("dd/mm/yyyy").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    workSheet.Cell(index, 3).Value = element.NoDias;
                    workSheet.Cell(index, 4).Value = element.TasaAnual;
                    workSheet.Cell(index, 5).Value = element.TasaMaxima;
                    workSheet.Cell(index, 6).Value = element.intAplicado;
                    workSheet.Cell(index, 7).Value = element.InteresNominal;
                    workSheet.Cell(index, 8).SetValue(element.capital).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 9).SetValue(element.CapitalALiquidar).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 10).SetValue(element.intPlazoPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 11).SetValue(element.saldoInteresPlazoAcum).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 12).SetValue(element.InteresMoraPeriodo).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 13).SetValue(element.interesAdeudadoMoraAcum).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 14).SetValue(element.abonos).Style.NumberFormat.Format = "$ #,##0.00";
                    workSheet.Cell(index, 15).SetValue(element.subTotal).Style.NumberFormat.Format = "$ #,##0.00";
                }

                index += 2;
                workSheet.Cell(index, 1).SetValue("Asunto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Cell(index, 2).SetValue("Valor").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                foreach ((string, double) rw in resumenes[i])
                {
                    workSheet.Cell(++index, 1).Value = rw.Item1;
                    workSheet.Cell(index, 2).SetValue(rw.Item2).Style.NumberFormat.Format = "$ #,##0.00";
                }

                index += 2;

                workSheet.Range("A" + index + ":F" + index).Merge().SetValue("Observaciones:").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                workSheet.Range("A" + (index + 1) + ":F" + (index + 1)).Merge().SetValue(observaciones[i]);

                index += 3;
            }

            workSheet.Columns(1, 18).AdjustToContents(); //-- Ajusta el tamaño de las columnas, a su contenido.

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        /// <summary>
        /// Crea un documento excel con los datos guardados en una liquidación de costas.
        /// </summary>
        /// <param name="liquidacion">Formulario Web</param>
        /// <returns>Documento de excel con los mismos datos del formulario web.</returns>
        public static MemoryStream CreateExcelDoc(List<(string,double)> liquidacion){
            MemoryStream memoryStream = new MemoryStream();

            XLWorkbook workbook = new XLWorkbook();
            workbook.Author = "Liquidador de Sentencias Judiciales Web";
            var workSheet = workbook.Worksheets.Add("Liquidación Costas del Proceso");
            int i = 5; double total = 0;

            workSheet.Range("A1:D1").Merge().SetValue("República de Colombia").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A2:D2").Merge().SetValue("Consejo Superior de la Judicatura").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Range("A3:D3").Merge().SetValue("RAMA JUDICIAL").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            workSheet.Cell("A5").SetValue("Asunto").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            workSheet.Cell("B5").SetValue("Valor").Style.Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            
            foreach((string, double) element in liquidacion){
                workSheet.Cell(++i, 1).Value = element.Item1;
                workSheet.Cell(i, 2).SetValue(element.Item2).Style.NumberFormat.Format = "$ #,##0.00";
                total += element.Item2;
            }

            i += 2;

            workSheet.Cell(++i, 1).Value = "Total";
            workSheet.Cell(i, 2).SetValue(total).Style.NumberFormat.Format = "$ #,##0.00";

            workSheet.Columns(1, 4).AdjustToContents(); //-- Ajusta el tamaño de las columnas, a su contenido.

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        /// <summary>
        /// Calcula un resumen general para múltiples liquidaciones.
        /// </summary>
        /// <param name="liquidaciones">Array de las liquidaciones a las que se les extraerá su tabla de resumen.</param>
        /// <returns>Resumen General de todas las liquidaciones.</returns>
        public static List<(string, double)> ResumenGeneral(Liquidador[] liquidaciones)
        {
            double tCapital = 0, tPlazo = 0, tMora = 0, tSancion = 0, tPago = 0, tAbono = 0, tNeto = 0, vuelto = 0;
            for (int i = 0; i < liquidaciones.Length; i++)
                foreach ((string, double) element in liquidaciones[i].Resumen())
                    if (element.Item1 == "Total Capital")
                        tCapital += element.Item2;
                    else if (element.Item1 == "Total Interés de Plazo")
                        tPlazo += element.Item2;
                    else if (element.Item1 == "Total Interés Mora")
                        tMora += element.Item2;
                    else if (element.Item1 == "Sanción Artículo 731 CC")
                        tSancion += element.Item2;
                    else if (element.Item1 == "Total a Pagar")
                        tPago += element.Item2;
                    else if (element.Item1 == "- Abonos")
                        tAbono += element.Item2;
                    else if (element.Item1 == "Neto a Pagar")                    
                        tNeto += element.Item2;
                    else if(element.Item1  == "Saldo devolver al deudor")
                        vuelto += element.Item2;

            return new List<(string, double)> {
                ("Total de Capitales", tCapital),
                ("Total Interés Plazo", tPlazo),
                ("Total Interés Mora", tMora),
                ("Total a Pagar", tPago),
                ("- Total Abonos", tAbono),
                ("- Sanción 20%", tSancion),
                ("Neto a Pagar", tNeto),
                //("Devolver al Deudor", tAbono - tPago) estaba realizando mal los calculos y realice el cambio 15/03/2023
                ("Devolver al Deudor", vuelto)
            };
        }
        /// <summary>
        /// Guarda cualquier tipo de dato en una variable de sesión.
        /// </summary>
        /// <typeparam name="T">Clase del objeto</typeparam>
        /// <param name="tempData">Estructura de la sesión.</param>
        /// <param name="key">Nombre de la variable de sesión.</param>
        /// <param name="value">Valor a almacenar en la variable de sesión.</param>
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        /// <summary>
        /// Extrae cualquier tipo de dato de una variable de sesión.
        /// </summary>
        /// <typeparam name="T">Clase Destino</typeparam>
        /// <param name="tempData">Estructura de la sesión</param>
        /// <param name="key">Nombre de la variable de sesión.</param>
        /// <returns></returns>
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static MemoryStream PdfSample() {
            Document document = new Document();

            document.Info.Title = "Vouche Liquidacion";
            document.Info.Author = "CSJ Liquidador";
            document.Info.Subject = "Liquidación";

            Style style = document.Styles["Normal"];
            style.Font.Name = "Verdana";

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Center);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            style = document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);

            Section section = document.AddSection();
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = "5mm";
            section.PageSetup.TopMargin = "5cm";
 
            var table = section.Headers.Primary.AddTable();
            table.AddColumn("6cm");
            table.AddColumn("17cm");
            table.AddColumn("6cm");

            var row = table.AddRow();
            row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            Image image = row.Cells[0].AddParagraph().AddImage(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\header-pdf-logo.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;
            image.WrapFormat.Style = WrapStyle.Through;

            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("República de Colombia");
            row.Cells[1].AddParagraph("Consejo Superior de la Judicatura");
            row.Cells[1].AddParagraph("RAMA JUDICIAL");

            Stream imagestream = GenImage("prueba", out string fileName);
            int count = (int)imagestream.Length;
            byte[] data = new byte[count];
            imagestream.Read(data, 0, count);
            string imgfile = "base64:" + Convert.ToBase64String(data);

            row.Cells[2].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            image = row.Cells[2].AddParagraph().AddImage(imgfile);
            image.Height = "3cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Column;
            image.WrapFormat.DistanceRight = "5mm";
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;

            var paragraph = row.Cells[2].AddParagraph(fileName);
            paragraph.Format.Font.Size = 5;
            paragraph.Format.RightIndent = "2cm";

            /* Footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("PowerBooks Inc · Sample Street 42 · 56789 Cologne · Germany");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            */

            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Color.FromRgb(1, 2, 3);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            var column = table.AddColumn("1.8cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.8cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.1cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2.2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Create the header of the table
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Format.Font.Bold = true;
            //row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Desde");
            row.Cells[1].AddParagraph("Hasta");
            row.Cells[2].AddParagraph("# Días");
            row.Cells[3].AddParagraph("Tasa Anual");
            row.Cells[4].AddParagraph("Tasa Máxima");
            row.Cells[5].AddParagraph("Interés Aplicado");
            row.Cells[6].AddParagraph("Interés Efectivo");
            row.Cells[7].AddParagraph("Capital");
            row.Cells[8].AddParagraph("Capital A Liquidar");
            row.Cells[9].AddParagraph("IntPlazoPeríodo");
            row.Cells[10].AddParagraph("SaldoIntPlazo");
            row.Cells[11].AddParagraph("Interés Mora Período");
            row.Cells[12].AddParagraph("Saldo Int Mora");
            row.Cells[13].AddParagraph("Abonos");
            row.Cells[14].AddParagraph("SubTotal");

            //table.SetEdge(0, 0, 15, 2, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

            for (int i = 0; i < 10; i++)
            {
                // Each item fills two rows
                var row1 = table.AddRow();
                var row2 = table.AddRow();
                row1.TopPadding = 1.5;
                //row1.Cells[0].Shading.Color = TableGray;
                row1.Cells[0].AddParagraph("12-2-2010");
                row1.Cells[1].AddParagraph("28-2-2010");
                row1.Cells[2].AddParagraph("20");
                row1.Cells[3].AddParagraph("27.945");
                row1.Cells[4].AddParagraph("25.343");
                row1.Cells[5].AddParagraph("123,1234");
                row1.Cells[6].AddParagraph("1000");
                row1.Cells[7].AddParagraph("9785.22");
                row1.Cells[8].AddParagraph("133.991");
                row1.Cells[9].AddParagraph("1778.77");
                row1.Cells[10].AddParagraph("192.139");
                row1.Cells[11].AddParagraph("0");
                row1.Cells[12].AddParagraph("2000");
                row1.Cells[13].AddParagraph("1334235.1343");

                //table.SetEdge(0, table.Rows.Count - 2, 15, 2, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75);
            }

            // Add an invisible row as a space line to the table
            row = table.AddRow();
            row.Borders.Visible = false;

            // Set the borders of the specified cell range
            table.SetEdge(5, table.Rows.Count - 4, 1, 4, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75);

            //-- Tabla de Resumen
            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Color.FromRgb(1, 2, 3);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();
            row.Cells[0].MergeRight = 1;
            row.HeadingFormat = true;
            row.Format.Font.Bold = true;
            row.Cells[0].AddParagraph("Resumen");

            row = table.AddRow();
            row.Cells[0].AddParagraph("Capital");
            row.Cells[1].AddParagraph("$ 10000.00");
            row = table.AddRow();
            row.Cells[0].AddParagraph("Capitales Adicionados");
            row.Cells[1].AddParagraph("$ 12345.67");
            row = table.AddRow();
            row.Cells[0].AddParagraph("Total Capital");
            row.Cells[1].AddParagraph("$ 10000");
            row = table.AddRow();
            row.Cells[0].AddParagraph("Total Interés Plazo");
            row.Cells[1].AddParagraph("$ 1778.76904635481");
            row = table.AddRow();
            row.Cells[0].AddParagraph("Total Interés Mora");
            row.Cells[1].AddParagraph("$ 3160.49946919472");
            row = table.AddRow();
            row.Cells[0].AddParagraph("Total A Pagar");
            row.Cells[1].AddParagraph("$ 14939.268515549");
            row = table.AddRow();
            row.Cells[0].AddParagraph("- Abonos");
            row.Cells[1].AddParagraph("$ 2000.00");
            row = table.AddRow();
            row.Cells[0].AddParagraph("Neto A Pagar");
            row.Cells[1].AddParagraph("$ 12939.2685155495");

            MemoryStream stream = new MemoryStream();
            var pdf = new PdfDocumentRenderer(true) { Document = document };
            pdf.RenderDocument();
            pdf.Save(stream, false);

            return stream;
        }
        /// <summary>
        /// Genera archivo PDF con un Vouche que identifica de manera única la liquidación.
        /// Válido para Liquidación Singular y Cuotas de Administración.
        /// </summary>
        /// <param name="liquidacion">Tabla de Liquidación para pasar a </param>
        /// <param name="resumen">Resumen de la liquidación ejecutada</param>
        /// <returns>Documento PDF</returns>
        public static MemoryStream PdfDoc(List<Liquidacion> liquidacion, List<(string, double)> resumen, string observaciones, out string fileName, string noProceso)
        {
            Document document = new Document();

            document.Info.Title = "Vouche Liquidacion";
            document.Info.Author = "CSJ Liquidador";
            document.Info.Subject = "Liquidación";

            Style style = document.Styles["Normal"];
            style.Font.Name = "Verdana";

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Center);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            style = document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);

            Section section = document.AddSection();
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = "5mm";
            section.PageSetup.TopMargin = "5cm";

            var table = section.Headers.Primary.AddTable();
            table.AddColumn("6cm");
            table.AddColumn("17cm");
            table.AddColumn("6cm");

            var row = table.AddRow();
            row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            Image image = row.Cells[0].AddParagraph().AddImage(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\header-pdf-logo.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;
            image.WrapFormat.Style = WrapStyle.Through;

            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("República de Colombia");
            row.Cells[1].AddParagraph("Consejo Superior de la Judicatura");
            row.Cells[1].AddParagraph("RAMA JUDICIAL");
            row.Cells[1].AddParagraph($"Proceso: {noProceso}");

            Stream imageStream = GenImage(liquidacion.ToString() + DateTime.Now.ToString(), out fileName);
            int count = (int)imageStream.Length;
            byte[] data = new byte[count];
            imageStream.Read(data, 0, count);
            string imgFile = "base64:" + Convert.ToBase64String(data);

            row.Cells[2].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            image = row.Cells[2].AddParagraph().AddImage(imgFile);
            image.Height = "3cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Column;
            image.WrapFormat.DistanceRight = "5mm";
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;

            var paragraph = row.Cells[2].AddParagraph(fileName);
            paragraph.Format.Font.Size = 5;
            paragraph.Format.RightIndent = "2cm";

            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddPageField();
            paragraph.AddText("/");
            paragraph.AddNumPagesField();
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Right;

            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Color.FromRgb(1, 2, 3);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            var column = table.AddColumn("1.8cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.8cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.1cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2.2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("1.7cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2.3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Create the header of the table
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Format.Font.Bold = true;
            //row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Desde dd/mm/aaaa");
            row.Cells[1].AddParagraph("Hasta dd/mm/aaaa");
            row.Cells[2].AddParagraph("# Días");
            row.Cells[3].AddParagraph("Tasa Anual");
            row.Cells[4].AddParagraph("Tasa Máxima");
            row.Cells[5].AddParagraph("Interés Aplicado");
            row.Cells[6].AddParagraph("Interés Efectivo");
            row.Cells[7].AddParagraph("Capital");
            row.Cells[8].AddParagraph("Capital A Liquidar");
            row.Cells[9].AddParagraph("IntPlazoPeríodo");
            row.Cells[10].AddParagraph("SaldoIntPlazo");
            row.Cells[11].AddParagraph("Interés Mora Período");
            row.Cells[12].AddParagraph("Saldo Int Mora");
            row.Cells[13].AddParagraph("Abonos");
            row.Cells[14].AddParagraph("SubTotal");

            //table.SetEdge(0, 0, 15, 2, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

            foreach (Liquidacion elemento in liquidacion)
            {
                // Each item fills two rows
                var row1 = table.AddRow();
                var row2 = table.AddRow();
                row1.TopPadding = 1.5;
                //row1.Cells[0].Shading.Color = TableGray;
                row1.Cells[0].AddParagraph($"{elemento.Desde:dd-M-yyyy}");
                row1.Cells[1].AddParagraph($"{elemento.Hasta:dd-M-yyyy}");
                row1.Cells[2].AddParagraph($"{elemento.NoDias}");
                row1.Cells[3].AddParagraph($"{elemento.TasaAnual}");
                row1.Cells[4].AddParagraph($"{elemento.TasaMaxima}");
                row1.Cells[5].AddParagraph($"{elemento.intAplicado}");
                row1.Cells[6].AddParagraph($"{elemento.InteresNominal:0.##}");
                paragraph = row1.Cells[7].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddText($" {elemento.capital}");
                paragraph = row1.Cells[8].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddText($" {elemento.CapitalALiquidar:0.##}");
                paragraph = row1.Cells[9].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddFormattedText($"{elemento.intPlazoPeriodo:0.##}");
                paragraph = row1.Cells[10].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddFormattedText($"{elemento.saldoInteresPlazoAcum:0.##}");
                paragraph = row1.Cells[11].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddFormattedText($"{elemento.InteresMoraPeriodo:0.##}");
                paragraph = row1.Cells[12].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddFormattedText($"{elemento.saldoInteresMoraPesos}");
                paragraph = row1.Cells[13].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddFormattedText($"{elemento.abonos}");
                paragraph = row1.Cells[14].AddParagraph();
                paragraph.AddFormattedText("$").Font.Size = 6;
                paragraph.AddFormattedText($"{elemento.subTotal:0.##}");

                //table.SetEdge(0, table.Rows.Count - 2, 15, 2, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75);
            }

            // Add an invisible row as a space line to the table
            row = table.AddRow();
            row.Borders.Visible = false;

            // Set the borders of the specified cell range
            table.SetEdge(5, table.Rows.Count - 4, 1, 4, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75);

            //-- Tabla de Resumen
            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Color.FromRgb(1, 2, 3);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();
            row.Cells[0].MergeRight = 1;
            row.HeadingFormat = true;
            row.Format.Font.Bold = true;
            row.Cells[0].AddParagraph("Resumen");

            foreach((string, double) elemento in resumen) {
                row = table.AddRow();
                row.Cells[0].AddParagraph(elemento.Item1);
                row.Cells[1].AddParagraph($"{elemento.Item2}");
            }
            
            // Add an invisible row as a space line to the table
            row = table.AddRow();
            row.Borders.Visible = false;

            //-- Tabla de Observaciones
            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Color.FromRgb(1, 2, 3);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            column = table.AddColumn("28,5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Font.Bold = true;
            row.Cells[0].AddParagraph("Observaciones");

            row = table.AddRow();
            row.Cells[0].AddParagraph(observaciones!=null?observaciones:"");

            MemoryStream stream = new MemoryStream();
            var pdf = new PdfDocumentRenderer(true) { Document = document };
            pdf.RenderDocument();
            pdf.Save(stream, false);
            pdf.Save(Directory.GetCurrentDirectory() + "\\wwwroot\\assets\\pdfDocuments\\" + fileName + ".pdf");

            return stream;
        }
        /// <summary>
        /// Genera archivo PDF con un Vouche que identifica de manera única la liquidación.
        /// Válido para Liquidación Múltiple
        /// </summary>
        /// <param name="liquidacion">Tabla de Liquidación para pasar a </param>
        /// <param name="resumen">Resumen de la liquidación ejecutada</param>
        /// <returns>Documento PDF</returns>
        public static MemoryStream PdfDoc(List<Liquidacion>[] liquidaciones, List<(string, double)>[] resumenes, string[] observaciones, out string fileName, string noProceso)
        {
            Document document = new Document();

            document.Info.Title = "Vouche Liquidacion";
            document.Info.Author = "CSJ Liquidador";
            document.Info.Subject = "Liquidación";

            Style style = document.Styles["Normal"];
            style.Font.Name = "Verdana";

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Center);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            style = document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);

            Section section = document.AddSection();
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = "5mm";
            section.PageSetup.TopMargin = "5cm";

            var table = section.Headers.Primary.AddTable();
            table.AddColumn("6cm");
            table.AddColumn("17cm");
            table.AddColumn("6cm");

            var row = table.AddRow();
            row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            Image image = row.Cells[0].AddParagraph().AddImage(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\header-pdf-logo.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;
            image.WrapFormat.Style = WrapStyle.Through;

            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].AddParagraph("República de Colombia");
            row.Cells[1].AddParagraph("Consejo Superior de la Judicatura");
            row.Cells[1].AddParagraph("RAMA JUDICIAL");
            row.Cells[1].AddParagraph($"Proceso: {noProceso}");

            Stream imagestream = GenImage(liquidaciones.ToString() + DateTime.Now.ToString() , out fileName);
            int count = (int)imagestream.Length;
            byte[] data = new byte[count];
            imagestream.Read(data, 0, count);
            string imgfile = "base64:" + Convert.ToBase64String(data);

            row.Cells[2].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            image = row.Cells[2].AddParagraph().AddImage(imgfile);
            image.Height = "3cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Column;
            image.WrapFormat.DistanceRight = "5mm";
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Center;

            var paragraph = row.Cells[2].AddParagraph(fileName);
            paragraph.Format.Font.Size = 5;
            paragraph.Format.RightIndent = "2cm";

            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddPageField();
            paragraph.AddText("/");
            paragraph.AddNumPagesField();
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Right;

            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.0cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            for (int i = 0; i < liquidaciones.Length; i++) {
                table = section.AddTable();
                table.Style = "Table";
                table.Borders.Color = Color.FromRgb(1, 2, 3);
                table.Borders.Width = 0.25;
                table.Borders.Left.Width = 0.5;
                table.Borders.Right.Width = 0.5;
                table.Rows.LeftIndent = 0;

                var column = table.AddColumn("1.8cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("1.8cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("1.1cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2.2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2.5cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("1.3cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;

                // Create the header of the table
                row = table.AddRow();
                row.HeadingFormat = true;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
                row.Format.Font.Bold = true;
                row.Cells[0].AddParagraph("Desde dd/mm/aaaa");
                row.Cells[1].AddParagraph("Hasta dd/mm/aaaa");
                row.Cells[2].AddParagraph("# Días");
                row.Cells[3].AddParagraph("Tasa Anual");
                row.Cells[4].AddParagraph("Tasa Máxima");
                row.Cells[5].AddParagraph("Interés Aplicado");
                row.Cells[6].AddParagraph("Interés Efectivo");
                row.Cells[7].AddParagraph("Capital");
                row.Cells[8].AddParagraph("Capital A Liquidar");
                row.Cells[9].AddParagraph("IntPlazoPeríodo");
                row.Cells[10].AddParagraph("SaldoIntPlazo");
                row.Cells[11].AddParagraph("Interés Mora Período");
                row.Cells[12].AddParagraph("Saldo Int Mora");
                row.Cells[13].AddParagraph("Abonos");
                row.Cells[14].AddParagraph("SubTotal");

                //table.SetEdge(0, 0, 15, 2, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

                foreach (Liquidacion elemento in liquidaciones[i])
                {
                    // Each item fills two rows
                    var row1 = table.AddRow();
                    var row2 = table.AddRow();
                    row1.TopPadding = 1.5;
                    //row1.Cells[0].Shading.Color = TableGray;
                    row1.Cells[0].AddParagraph($"{elemento.Desde:dd-M-yyyy}");
                    row1.Cells[1].AddParagraph($"{elemento.Hasta:dd-M-yyyy}");
                    row1.Cells[2].AddParagraph($"{elemento.NoDias}");
                    row1.Cells[3].AddParagraph($"{elemento.TasaAnual}");
                    row1.Cells[4].AddParagraph($"{elemento.TasaMaxima}");
                    row1.Cells[5].AddParagraph($"{elemento.intAplicado}");
                    row1.Cells[6].AddParagraph($"{elemento.InteresNominal:0.##}");
                    paragraph = row1.Cells[7].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddText($" {elemento.capital}");
                    paragraph = row1.Cells[8].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddText($" {elemento.CapitalALiquidar:0.##}");
                    paragraph = row1.Cells[9].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddFormattedText($"{elemento.intPlazoPeriodo:0.##}");
                    paragraph = row1.Cells[10].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddFormattedText($"{elemento.saldoInteresPlazoAcum:0.##}");
                    paragraph = row1.Cells[11].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddFormattedText($"{elemento.InteresMoraPeriodo:0.##}");
                    paragraph = row1.Cells[12].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddFormattedText($"{elemento.saldoInteresMoraPesos}");
                    paragraph = row1.Cells[13].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddFormattedText($"{elemento.abonos}");
                    paragraph = row1.Cells[14].AddParagraph();
                    paragraph.AddFormattedText("$").Font.Size = 6;
                    paragraph.AddFormattedText($"{elemento.subTotal:0.##}");

                    //table.SetEdge(0, table.Rows.Count - 2, 15, 2, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75);
                }

                // Add an invisible row as a space line to the table
                row = table.AddRow();
                row.Borders.Visible = false;

                // Set the borders of the specified cell range
                table.SetEdge(5, table.Rows.Count - 4, 1, 4, MigraDoc.DocumentObjectModel.Tables.Edge.Box, BorderStyle.Single, 0.75);

                //-- Tabla de Resumen
                table = section.AddTable();
                table.Style = "Table";
                table.Borders.Color = Color.FromRgb(1, 2, 3);
                table.Borders.Width = 0.25;
                table.Borders.Left.Width = 0.5;
                table.Borders.Right.Width = 0.5;
                table.Rows.LeftIndent = 0;

                column = table.AddColumn("4cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                column = table.AddColumn("4cm");
                column.Format.Alignment = ParagraphAlignment.Center;

                row = table.AddRow();
                row.Cells[0].MergeRight = 1;
                row.HeadingFormat = true;
                row.Format.Font.Bold = true;
                row.Cells[0].AddParagraph("Resumen");

                foreach ((string, double) elemento in resumenes[i])
                {
                    row = table.AddRow();
                    row.Cells[0].AddParagraph(elemento.Item1);
                    row.Cells[1].AddParagraph($"{elemento.Item2}");
                }

                // Add an invisible row as a space line to the table
                row = table.AddRow();
                row.Borders.Visible = false;

                //-- Tabla de Observaciones
                table = section.AddTable();
                table.Style = "Table";
                table.Borders.Color = Color.FromRgb(1, 2, 3);
                table.Borders.Width = 0.25;
                table.Borders.Left.Width = 0.5;
                table.Borders.Right.Width = 0.5;
                table.Rows.LeftIndent = 0;

                column = table.AddColumn("28,5cm");
                column.Format.Alignment = ParagraphAlignment.Center;

                row = table.AddRow();
                row.HeadingFormat = true;
                row.Format.Font.Bold = true;
                row.Cells[0].AddParagraph("Observaciones");

                row = table.AddRow();
                row.Cells[0].AddParagraph(observaciones[i] != null ? observaciones[i] : "");
            }

            MemoryStream stream = new MemoryStream();
            var pdf = new PdfDocumentRenderer(true) { Document = document };
            pdf.RenderDocument();
            pdf.Save(stream, false);
            pdf.Save(Directory.GetCurrentDirectory() + "\\wwwroot\\assets\\pdfDocuments\\" + fileName + ".pdf");

            return stream;
        }
        /// <summary>
        /// Genera un hash de 64 dígitos para cada liquidación, el cual a su vez es usado para generar un código QR.
        /// </summary>
        /// <param name="liquidacion">Liquidación a procesar</param>
        /// <param name="fileName">Nombre del archivo generado</param>
        /// <returns>Ruta de la imagen con el código QR</returns>
        private static MemoryStream GenImage(string liquidacion, out string fileName)
        {
            MemoryStream stream = new MemoryStream();

            SHA256 sha256Hash = SHA256.Create();
            byte[] hashData = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(liquidacion));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
                builder.Append(hashData[i].ToString("x2"));

            fileName = builder.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(fileName, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);

            qrCodeImage.Save(stream, ImageFormat.Jpeg);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}