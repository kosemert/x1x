using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmbBase.Web.Models;
using Umbraco.Web.Mvc;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using PhoneNumbers;
using System.Net;

namespace UmbBase.Web.Controllers
{
    public class HRController : Umbraco.Web.Mvc.SurfaceController
    {
        [ChildActionOnly]
        public ActionResult HRApplicationForm()
        {
            return View(new HRFormViewModel()
            {
                Email = "Email@email.com",
                FullName = "Ad - Soyad",
                BirtDay = DateTime.Now,
                file = null,
                TelNo = "telno",
                birthplace = "Doğum Yeri",
                maritalStatus = null,
                PreviewText = "Ön Yazı",
                Sexual = null,
                smoke = null
            }
                );
        }
        [HttpPost]
        //[CaptchaValidator(ErrorMessage = "HRApplicationForm.Captcha.Error", RequiredMessage = "HRApplicationForm.Captcha.Required")]
        [NotChildAction]
        public ActionResult HRApplicationForm(HRFormViewModel model)
        {

            var page = (CurrentPage);// as ContactFormDocType);
            if (ModelState.IsValid)
            {
                HRFormViewModel hr = new HRFormViewModel()
                {
                    birthplace = model.birthplace,
                    Email = model.Email,
                    FullName = model.FullName,
                    maritalStatus = model.maritalStatus,
                    PreviewText = model.PreviewText,
                    Sexual = model.Sexual,
                    smoke = model.smoke,
                    TelNo = model.TelNo,
                    BirtDay = model.BirtDay};
                var emailClient = new SmtpClient();
                var message = new MailMessage();
                ViewData.Model = model;
                string messageBody;
                emailClient.EnableSsl = true;
                if (model.file != null)
                {
                    var domain = HttpContext.Request.Url.Host;
                    string fileName = Path.GetFileName(model.file.FileName);
                    message.Attachments.Add(new Attachment(model.file.InputStream, fileName));
                    using (var sw = new StringWriter())
                    {
                        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, ConfigurationManager.AppSettings["HrApplicationFormMailTemplate"]);
                        var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                        viewResult.View.Render(viewContext, sw);
                        messageBody = sw.GetStringBuilder().ToString();
                    }
                    message.Body = messageBody;
                    message.IsBodyHtml = true;
                    message.Subject = string.Format(ConfigurationManager.AppSettings["HRApplicationFormSubjectMask"], domain);
                    var emailAddresses = ConfigurationManager.AppSettings["ContactFormReceiver"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var addr in emailAddresses)
                    {
                        message.To.Add(new MailAddress(addr));
                    }

                    var bccEmailAddresses = ConfigurationManager.AppSettings["ContactFormReceiverBcc"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var addr in bccEmailAddresses)
                    {
                        message.Bcc.Add(new MailAddress(addr));
                    }
                    emailClient.Send(message);
                    if (page != null)
                    {
                        return Redirect(page.Url + "?success");
                    }
                }
                return Redirect(page.Url + "?Failed");
            }
            var index = ModelState.Keys.ToList().IndexOf("ReCaptcha");
            if (index > 0)
            {
                var captchaMessages = new List<string>();
                var values = ModelState.Values.ToList();

                foreach (var error in values[index].Errors)
                {
                    var translatedMessage = Umbraco.GetDictionaryValue(error.ErrorMessage);
                    captchaMessages.Add(!string.IsNullOrEmpty(translatedMessage) ? translatedMessage : error.ErrorMessage);
                }

                if (captchaMessages.Any())
                {
                    ModelState["ReCaptcha"].Errors.Clear();
                    foreach (var msg in captchaMessages)
                    {
                        ModelState["ReCaptcha"].Errors.Add(msg);
                    }
                }
            }
            return CurrentUmbracoPage();
        }
    }
}