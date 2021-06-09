using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Lab1.Models;
using Lab1.Models.Entities;

namespace Lab1.Controllers
{
    public class MmailManagerController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        // GET: Message
        public ActionResult Index(int id = 2)
        {
            MmailManager model = new MmailManager();
            model.Client = db.Clients.Find(id);
            return View(model);
        }


        [HttpPost]
        public ActionResult Index()
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("ant_aa@mail.ru", "Сообщение из системы Турагентство");
            // кому отправляем
            MailAddress to = new MailAddress("antaa200@gmail.com");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Оповещение";
            // текст письма
            string message = "На сайте отправлено письмо администратору";
            m.Body = $"<h2>Сообщение: {message} </h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("ant_aa", "");
            smtp.EnableSsl = true;
            smtp.Send(m);

            return RedirectToAction("Index", "Home");
        }
    }
}