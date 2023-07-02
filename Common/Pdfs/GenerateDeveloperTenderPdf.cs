using castlers.Dtos;
using System.IO;
using castlers.Models;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Microsoft.AspNetCore.Http.Metadata;

namespace castlers.Common.Pdfs
{
    //public class GenerateDeveloperTenderPdf
    //{
    //    public async Task<IActionResult> GeneratePdf (TenderDetails tenderDetails)
    //    {
    //        var document = new PdfDocument ();
    //        string htmlContent = "<h1>Developer Name: XYZ</h1>";
    //        htmlContent += "<div>";
    //        htmlContent += "<table style='width:100%; border:1px solid #000'>";
    //        htmlContent += "<thead style='font-weight:bold>";
    //        htmlContent += "<tr>";
    //        htmlContent += "<td style='border:1px solid #000'> </td>";
    //        htmlContent += "<td style='border:1px solid #000'> </td>";
    //        htmlContent += "</tr>";
    //        htmlContent += "<tr>";
    //        htmlContent += "<td style='border:1px solid #000'> 23</td>";
    //        htmlContent += "<td style='border:1px solid #000'> 25</td>";
    //        htmlContent += "</tr>";
    //        htmlContent += "</div>";


    //        PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
    //        byte[]? response = null;
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            document.Save(ms);
    //            response = ms.ToArray();
    //        }
    //        string filename = "Tender Details" + ".pdf";

    //        IFormFile file;
            

    //        //return File(response, "application/pdf", filename);
            
    //    }
    //}
}
