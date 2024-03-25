using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GestionTrabajo1
{
    class Usuario
    {
        public Usuario() { 
            
        }
        public Usuario(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
        public bool ComprobadorDeContrasenia(string contrasenia) {
            if (contrasenia.Length > 8)
            {
                if (ComprobarAlfaNum(contrasenia)) {
                    MailAddress mailAddress = new MailAddress(Email);
                    if (!contrasenia.Equals(mailAddress.User))
                    {
                        return true;
                    }
                }

            } 
            return false;
        }
        public bool ComprobarAlfaNum(string contrasenia)
        {
             foreach(char c in contrasenia)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
             return true;
        }

    }
}
