using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelFormApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;

public class ExcelService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ExcelService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public List<FormViewModel> GetExcelData()
    {
        
        var excelData = new List<FormViewModel>
        {
            new FormViewModel { Isim = "John", Soyisim = "Doe", Adres = "123 Main St", Mail = "john.doe@example.com" },
            new FormViewModel {Isim = "Jane", Soyisim = "Doe", Adres = "456 Oak St", Mail = "jane.doe@example.com" }
            
        };

        return excelData;
    }

    public void CreateExcelFile(List<FormViewModel> data, string filePath)
    {
        
       using (var spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
        {
            
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            
            var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
            sheets.Append(sheet);

            
            var headerRow = new Row();
            headerRow.Append(new Cell() { CellValue = new CellValue("İsim") });
            headerRow.Append(new Cell() { CellValue = new CellValue("Soyisim") });
            headerRow.Append(new Cell() { CellValue = new CellValue("Adres") });
            headerRow.Append(new Cell() { CellValue = new CellValue("Mail") });
            

           
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            sheetData.Append(headerRow);
            foreach (var item in data)
            {
                var row = new Row();
                row.Append(new Cell() { CellValue = new CellValue(item.Isim) });
                row.Append(new Cell() { CellValue = new CellValue(item.Soyisim) });
                row.Append(new Cell() { CellValue = new CellValue(item.Adres) });
                row.Append(new Cell() { CellValue = new CellValue(item.Mail) });
                sheetData.Append(row);
            }
        }
    }
}
