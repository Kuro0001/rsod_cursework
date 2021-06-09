using Lab1.Models;
using Lab1.Models.DetailModels;
using Lab1.Models.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Lab1.Controllers.DetailControllers
{
    public class EXELController : Controller
    {
        public ActionResult GetExelClientInfo(int ClietnId)
        {
            string pathExample = Server.MapPath("~/OutputFiles/ClientInfo_example.xlsx"); ;
            string pathResult = Server.MapPath("~/OutputFiles/ClientInfo.xlsx"); ;
            if (!System.IO.File.Exists(pathResult))
            {
                System.IO.File.Create(pathResult);
            }

            // Путь к файлу с шаблоном
            FileInfo fi = new FileInfo(pathExample);

            FileInfo fi_report = new FileInfo(pathResult);
            //будем использовть библитотеку не для коммерческого использования

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //открываем файл с шаблоном
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                //заполнение переменной информацией клиента
                TouristAgencyDbEntities db = new TouristAgencyDbEntities();
                DetailClient allTables = new DetailClient();

                allTables.Client = db.Clients.Find(ClietnId);
                List<Voucher> list = db.Vouchers.Where(v => v.ClientId == ClietnId).ToList();
                allTables.Vouchers = new List<Voucher>();
                foreach (var item in list)
                {
                    item.Client = allTables.Client;
                    item.Employee = db.Employees.Find(item.EmployeeId);
                    item.Tour = db.Tours.Find(item.TourId);
                    item.Tour.Hotel = db.Hotels.Find(item.Tour.HotelId);
                    item.Tour.Hotel.Direction = db.Directions.Find(item.Tour.Hotel.DirectionId);
                    allTables.Vouchers.Add(item);
                }

                //устанавливаем поля документа
                excelPackage.Workbook.Properties.Author = "Сотрудник компании";
                excelPackage.Workbook.Properties.Title = "Путевки клиента";
                excelPackage.Workbook.Properties.Subject = "Учетные записи клиента: " + allTables.Client.Surname + " " + allTables.Client.Name;
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Лист1"];

                int startLine = 2;

                foreach (var item in allTables.Vouchers)
                {
                    worksheet.Cells[startLine, 1].Value = item.ID;
                    worksheet.Cells[startLine, 2].Value = item.Employee.Surname + " " + item.Employee.Name;
                    worksheet.Cells[startLine, 3].Value = item.Date.ToString();
                    worksheet.Cells[startLine, 4].Value = item.Price;
                    worksheet.Cells[startLine, 5].Value = item.Tour.StartDate.ToString();
                    worksheet.Cells[startLine, 6].Value = item.Tour.DayCount;
                    worksheet.Cells[startLine, 7].Value = item.Tour.Name;
                    worksheet.Cells[startLine, 8].Value = item.Tour.Hotel.Direction.Name;

                    startLine++;
                }

                //созраняем в новое место
                excelPackage.SaveAs(fi_report);
            }
            // Тип файла - content-type
            string file_type = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
            // Имя файла - необязательно
            string file_name = "ClientInfo.xlsx";

            return File(pathResult, file_type, file_name);
            //System.Web.Mvc.FilePathResult 
        }
    }
}