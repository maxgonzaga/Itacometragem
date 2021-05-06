using Itacometragem.Library;
using DinkToPdf;
using DinkToPdf.Contracts;
using Itacometragem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Itacometragem.Models
{
    public class ReportService : IReportService
    {
        public readonly IConverter _converter;

        public ReportService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdfReport(Report report)
        {

            string style = @"
                body {font-family: Arial;}
                table {width: 100%; border-collapse: collapse; }
                table, th, td {border: 1px solid black;}
                th, td { padding: 15px; text-align: left; height: 25px; vertical-align: center;}"; 
            
            string html = $@"
                <!DOCTYPE html>
                <html lang=""pt-br"">
                <head>
                <title>Registro de Quilometragem</title>
                <style>
                {style}
                </style>
                </head>
                <body>
                <div>
                <h2>Motivo: {report.Motive.Name}</h2>
                <p>Data inicial: {report.InitialDate?.ToString("d")}</p>
                <p>Data final: {report.FinalDate?.ToString("d")}</p>
                <table><thead><tr>
                <th>Data</th>
                <th>Hora</th>
                <th>Carro</th>
                <th>Motorista</th>
                <th>Distância</th>
                <th>Motivo</th></tr></thead><tbody>";

            foreach (Ride ride in report.Rides)
            {
                html += "<tr class=\"ride\">";
                html += $"<td>{ride.Date?.ToString("d")}</td>";
                html += $"<td>{ride.Date?.ToString("HH:mm")}";
                html += $"<td>{ride.Car.Name}</td>";
                html += $"<td>{ride.Driver.Name}</td>";
                html += $"<td>{ride.GetDistance()}</td>";
                html += $"<td>{ride.Motive.Name}</td>";
                html += "</tr>";
            }

            html += $"<tr>Distância total: {report.TotalDistance()}</tr>";
            html += "</tbody></table></body></html>";


            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };

            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;

            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";

            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Arial";
            headerSettings.Right = "Página [page] de [toPage]";
            headerSettings.Line = true;

            FooterSettings footerSettings = new FooterSettings
            {
                FontSize = 12,
                FontName = "Arial",
                Center = $"{DateTime.Now.Date.Date}",
                Line = true
            };

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            

            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return _converter.Convert(htmlToPdfDocument);
        }
    }
}
