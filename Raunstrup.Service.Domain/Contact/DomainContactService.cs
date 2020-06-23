using Raunstrup.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainContactService : IContactService
    {
        public bool SendEmail(Contact vm)
        {
            try
            {
                MailMessage msz = new MailMessage();
                msz.From = new MailAddress(vm.Email); //Den email man får en besked fra. 

                //Mail sendes til                                     
                msz.To.Add("teamraunstrup@gmail.com");
                msz.Subject = vm.Subject;

                msz.Body = string.Format("Navn: " + vm.Name + "<br> Email: " + vm.Email + "<br> Besked: " + vm.Message);
                msz.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.Port = 587;
                // Login til mail
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("teamraunstrup@gmail.com", "PasswordHere");

                smtp.EnableSsl = true;

                smtp.Send(msz);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
