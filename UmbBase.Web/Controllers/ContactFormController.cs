using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using reCAPTCHA.MVC;
using UmbBase.Web.Models;
using Umbraco.Web.Mvc;
using System;

namespace UmbBase.Web.Controllers
{
    public class ContactFormController : Umbraco.Web.Mvc.SurfaceController
    {
        [ChildActionOnly]
        public ActionResult SendContactForm()
        {            
            return View(new ContactFormViewModel());
        }

        [HttpPost]        
        [CaptchaValidator(ErrorMessage = "ContactForm.Captcha.Error",RequiredMessage = "ContactForm.Captcha.Required")]
        [NotChildAction]
        public ActionResult SendContactForm(ContactFormViewModel model)
        {
            
            var page = (CurrentPage);// as ContactFormDocType);
            if (ModelState.IsValid)
            {
                var emailClient = new SmtpClient();
                var message = new MailMessage();
                ViewData.Model = model;
                string messageBody;
                using (var sw = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, ConfigurationManager.AppSettings["ContactFormMailTemplate"]);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    messageBody = sw.GetStringBuilder().ToString();
                }
                message.Body = messageBody;
                message.IsBodyHtml = true;
                message.Subject = string.Format(ConfigurationManager.AppSettings["ContactFormSubjectMask"], model.Subject);
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
                    return Redirect(page.Url+"?success");
                }
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