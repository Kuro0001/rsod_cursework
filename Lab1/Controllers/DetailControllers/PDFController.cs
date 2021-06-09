using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Lab1.Models;
using Lab1.Models.DetailModels;
using Lab1.Models.Entities;

namespace Lab1.Controllers.DetailControllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public ActionResult GetPDFClientInfo(int ClientId)
        {
            MemoryStream ms = new MemoryStream();

            Document document = new Document(PageSize.A4, 16, 16, 16, 16);

            document.SetPageSize(PageSize.A4.Rotate());

            PdfWriter pw = PdfWriter.GetInstance(document, ms);

            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(Server.MapPath("~/Font/arial.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            Font normalFont = new Font(baseFont, 12, Font.NORMAL);
            Font boldFont = new Font(baseFont, 12, Font.BOLD);

            PdfPTable tableClient = new PdfPTable(8);

            TouristAgencyDbEntities db = new TouristAgencyDbEntities();
            DetailClient allTables = new DetailClient();

            allTables.Client = db.Clients.Find(ClientId);
            List<Voucher> list = db.Vouchers.Where(v => v.ClientId == ClientId).ToList();
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

            PdfPCell cellClientName1 = new PdfPCell(new Phrase("Фамилия", boldFont));
            PdfPCell cellClientName2 = new PdfPCell(new Phrase("Имя", boldFont));
            PdfPCell cellClientName3 = new PdfPCell(new Phrase("Отчество", boldFont));
            PdfPCell cellClientName4 = new PdfPCell(new Phrase("Дата рождения".ToString(), boldFont));
            PdfPCell cellClientName5 = new PdfPCell(new Phrase("Серия и номер паспорта", boldFont));
            PdfPCell cellClientName6 = new PdfPCell(new Phrase("Телефон", boldFont));
            PdfPCell cellClientName7 = new PdfPCell(new Phrase("Email", boldFont));
            PdfPCell cellClientName8 = new PdfPCell(new Phrase("Sex", boldFont));

            PdfPCell cellClient1 = new PdfPCell(new Phrase(allTables.Client.Surname, normalFont));
            PdfPCell cellClient2 = new PdfPCell(new Phrase(allTables.Client.Name, normalFont));
            PdfPCell cellClient3 = new PdfPCell(new Phrase(allTables.Client.Patronymic, normalFont));
            PdfPCell cellClient4 = new PdfPCell(new Phrase(allTables.Client.BirthDate.ToString(), normalFont));
            PdfPCell cellClient5 = new PdfPCell(new Phrase(allTables.Client.Pasport, normalFont));
            PdfPCell cellClient6 = new PdfPCell(new Phrase(allTables.Client.Phone, normalFont));
            PdfPCell cellClient7 = new PdfPCell(new Phrase(allTables.Client.Email, normalFont));
            PdfPCell cellClient8 = new PdfPCell(new Phrase(allTables.Client.Sex, normalFont));


            tableClient.AddCell(cellClientName1);
            tableClient.AddCell(cellClientName2);
            tableClient.AddCell(cellClientName3);
            tableClient.AddCell(cellClientName4);
            tableClient.AddCell(cellClientName5);
            tableClient.AddCell(cellClientName6);
            tableClient.AddCell(cellClientName7);
            tableClient.AddCell(cellClientName8);

            tableClient.AddCell(cellClient1);
            tableClient.AddCell(cellClient2);
            tableClient.AddCell(cellClient3);
            tableClient.AddCell(cellClient4);
            tableClient.AddCell(cellClient5);
            tableClient.AddCell(cellClient6);
            tableClient.AddCell(cellClient7);
            tableClient.AddCell(cellClient8);

            tableClient.SpacingAfter = 16;

            document.Add(tableClient);

            PdfPTable tableVouchers = new PdfPTable(7);

            tableVouchers.SpacingAfter = 16;

            PdfPCell cellCarDriverName1 = new PdfPCell(new Phrase("Сотрудник компании", boldFont));
            PdfPCell cellCarDriverName2 = new PdfPCell(new Phrase("Дата оформления", boldFont));
            PdfPCell cellCarDriverName3 = new PdfPCell(new Phrase("Цена", boldFont));
            PdfPCell cellCarDriverName4 = new PdfPCell(new Phrase("Дата тура", boldFont));
            PdfPCell cellCarDriverName5 = new PdfPCell(new Phrase("Кол-во дней", boldFont));
            PdfPCell cellCarDriverName6 = new PdfPCell(new Phrase("Наименование тура", boldFont));
            PdfPCell cellCarDriverName7 = new PdfPCell(new Phrase("направление", boldFont));

            tableVouchers.AddCell(cellCarDriverName1);
            tableVouchers.AddCell(cellCarDriverName2);
            tableVouchers.AddCell(cellCarDriverName3);
            tableVouchers.AddCell(cellCarDriverName4);
            tableVouchers.AddCell(cellCarDriverName5);
            tableVouchers.AddCell(cellCarDriverName6);
            tableVouchers.AddCell(cellCarDriverName7);

            foreach (var item in allTables.Vouchers)
            {
                PdfPCell cellCarDriver1 = new PdfPCell(new Phrase(item.Employee.Surname + " " + item.Employee.Name, normalFont));
                PdfPCell cellCarDriver2 = new PdfPCell(new Phrase(item.Date.ToString(), normalFont));
                PdfPCell cellCarDriver3 = new PdfPCell(new Phrase(item.Price.ToString(), normalFont));
                PdfPCell cellCarDriver4 = new PdfPCell(new Phrase(item.Tour.StartDate.ToString(), normalFont));
                PdfPCell cellCarDriver5 = new PdfPCell(new Phrase(item.Tour.DayCount.ToString(), normalFont));
                PdfPCell cellCarDriver6 = new PdfPCell(new Phrase(item.Tour.Name, normalFont));
                PdfPCell cellCarDriver7 = new PdfPCell(new Phrase(item.Tour.Hotel.Direction.Name, normalFont));

                tableVouchers.AddCell(cellCarDriver1);
                tableVouchers.AddCell(cellCarDriver2);
                tableVouchers.AddCell(cellCarDriver3);
                tableVouchers.AddCell(cellCarDriver4);
                tableVouchers.AddCell(cellCarDriver5);
                tableVouchers.AddCell(cellCarDriver6);
                tableVouchers.AddCell(cellCarDriver7);
            }

            document.Add(tableVouchers);


            document.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();

            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
    }
}