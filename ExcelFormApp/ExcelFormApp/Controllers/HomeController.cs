using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExcelFormApp.Models;
using System.Collections.Generic;

namespace ExcelFormApp.Controllers;

public class HomeController : Controller
{
    private readonly ExcelService _excelService;

    public HomeController(ExcelService excelService)
    {
        _excelService = excelService;
    }

    public IActionResult Index()
    {
        return View();
    }

     [HttpPost]
    public IActionResult GenerateExcel(FormViewModel model)
    {
        
        var data = new List<FormViewModel> { model };

         var filePath = Path.Combine("file.xlsx");
        _excelService.CreateExcelFile(data, filePath);

        
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "output.xlsx");

        
    }

    [HttpGet]
    public IActionResult GetExcelData()
    {
        var excelData = _excelService.GetExcelData();

        return Json(excelData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
