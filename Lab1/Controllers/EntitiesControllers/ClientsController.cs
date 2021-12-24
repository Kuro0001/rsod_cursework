using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Lab1.Models;
using Lab1.Models.Entities;

namespace Lab1.Controllers.EntitiesControllers
{
    public class ClientsController : Controller
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();


        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create(string email)
        {
            ViewBag.email = email;
            return View();
        }

        // POST: Clients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Pasport,Name,Surname,Patronymic,BirthDate,Sex,Phone,Email,Login,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();

                HttpStatusCode code = SendToAPI(client);
                switch (code)
                {
                    case HttpStatusCode.NoContent:
                        {
                            return RedirectToAction("Index");
                        }
                    default:
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                }
                //return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Pasport,Name,Surname,Patronymic,BirthDate,Sex,Phone,Email,Login,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                var url = string.Format(string.Format("https://localhost:5001/client/{0}", client.ID.ToString())); //string.Format("https://localhost:5001/client/{0}", id.ToString())

                HttpStatusCode code = SendToAPI(client, "PUT", url);
                switch (code)
                {
                    case HttpStatusCode.Created:
                        {
                            return RedirectToAction("Index");
                        }
                    default:
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                }
                //return RedirectToAction("Index");
            }
            return View(client);
        }


        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();

            HttpStatusCode code = DeleteFromAPI(id);
            switch (code)
            {
                case HttpStatusCode.NoContent:
                    {
                        return RedirectToAction("Index");
                    }
                default:
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
            }
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult LoadFromAPI()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:5001/client");
            request.Accept = "application/xml";
            WebResponse response = request.GetResponse();
            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            XmlElement root = doc.DocumentElement;
            foreach (XmlElement clientNode in root)
            {
                int id = Convert.ToInt32(clientNode["ID"].InnerText);
                Client client = db.Clients.Find(id);
                if (client == null)
                {
                    //Изменить ввод полей и проверить на уникальность айдишника
                    Client newClient = new Client()
                    {
                        Name = clientNode["Name"].InnerText,
                        Surname = clientNode["Surname"].InnerText,
                        Patronymic = clientNode["Patronymic"].InnerText,
                        Pasport = clientNode["Passport"].InnerText,
                        BirthDate = Convert.ToDateTime(clientNode["BirthDate"].InnerText),
                        Sex = clientNode["Sex"].InnerText,
                        Phone = clientNode["Phone"].InnerText,
                        Email = clientNode["Email"].InnerText,
                        Login = clientNode["Login"].InnerText,
                        Password = clientNode["Password"].InnerText
                    };
                    db.Clients.Add(newClient);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private XmlElement PackStudentToXml(XmlDocument doc, Client note)
        {
            XmlElement workNode = doc.CreateElement("Client");

            XmlElement IdNode = doc.CreateElement("ID");
            IdNode.InnerText = note.ID.ToString();
            workNode.AppendChild(IdNode);

            XmlElement NameNode = doc.CreateElement("Name");
            NameNode.InnerText = note.Name;
            workNode.AppendChild(NameNode);

            XmlElement SurnameNode = doc.CreateElement("Surname");
            SurnameNode.InnerText = note.Surname;
            workNode.AppendChild(SurnameNode);

            XmlElement PatronymicNode = doc.CreateElement("Patronymic");
            PatronymicNode.InnerText = note.Patronymic;
            workNode.AppendChild(PatronymicNode);

            XmlElement PasportNode = doc.CreateElement("Passport");
            PasportNode.InnerText = note.Pasport;
            workNode.AppendChild(PasportNode);

            XmlElement BirthDateNoNode = doc.CreateElement("BirthDate");
            BirthDateNoNode.InnerText = note.BirthDate.ToString();
            workNode.AppendChild(BirthDateNoNode);

            XmlElement SexNode = doc.CreateElement("Sex");
            SexNode.InnerText = note.Sex;
            workNode.AppendChild(SexNode);

            XmlElement PhoneNode = doc.CreateElement("Phone");
            PhoneNode.InnerText = note.Phone;
            workNode.AppendChild(PhoneNode);

            XmlElement EmailNode = doc.CreateElement("Email");
            EmailNode.InnerText = note.Email;
            workNode.AppendChild(EmailNode);

            XmlElement LoginNode = doc.CreateElement("Login");
            LoginNode.InnerText = note.Login;
            workNode.AppendChild(LoginNode);

            XmlElement PasswordNode = doc.CreateElement("Password");
            PasswordNode.InnerText = note.Password;
            workNode.AppendChild(PasswordNode);

            return workNode;
        }
        private HttpStatusCode SendToAPI(IEnumerable<Client> notes)
        {
            foreach (Client n in notes)
            {
                SendToAPI(n);
            }
            return HttpStatusCode.NoContent;
        }

        private HttpStatusCode SendToAPI(Client note)
        {
            //формируем xml документ
            XmlDocument doc = new XmlDocument();
            doc.CreateXmlDeclaration("1.0", "utf-8", "no");
            XmlElement workNode = PackStudentToXml(doc, note);
            doc.AppendChild(workNode);
            //получаем строку в формате XML
            string xml = doc.OuterXml;
            //преобразуем строку в массив байт
            byte[] byteArray = Encoding.UTF8.GetBytes(xml);
            //создаем http запрос, указываем метод и тип содержимого
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:5001/client"); //string.Format("https://localhost:5001/client/{0}", id.ToString())
            request.Method = "POST";
            request.ContentType = "application/xml; encoding='utf-8'";
            request.ContentLength = byteArray.Length;
            //в поток (в тело) запроса записываем подготовленный массив байт
            var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);
            reqStream.Close();
            //отправляем запрос и получаем ответ
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }

        private HttpStatusCode SendToAPI(Client note, string method, string url)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement workNode = PackStudentToXml(doc, note);
            doc.AppendChild(workNode);

            string xml = doc.OuterXml;
            byte[] byteArray = Encoding.UTF8.GetBytes(xml);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.Method = method;
            request.ContentType = "application/xml; encoding=UTF-8";
            request.ContentLength = byteArray.Length;

            var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);
            reqStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }


        private HttpStatusCode DeleteFromAPI(int id)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://localhost:5001/client/{0}", id.ToString()));
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }


        
    }
}