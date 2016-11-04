using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TravelBug.Controllers.Elements
{
    enum ContactFormType
    {
        isEmptyEmailorPhone,
        isEmptyMessage,
        isOk
    }
    public class AjaxController : BaseController
    {
        [HttpPost]
        public JsonResult ContactForm(string name, string email, string phone, string message)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
                return Json(new { ContactFormType = ContactFormType.isEmptyEmailorPhone.ToString(), Message = "Please, write you email or phone number." });

            if (string.IsNullOrEmpty(message))
                return Json(new
                {
                    ContactFormType = ContactFormType.isEmptyMessage.ToString(),
                    Message = "Please, write you message."
                });

         //manager.SetContact(new Contatc(name: name, email: email, phone: phone, message: message));

            return Json(new { ContactFormType = ContactFormType.isOk, Message = "Is everything alright." });
        }
        
    }
}