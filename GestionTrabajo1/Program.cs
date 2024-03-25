using GestionTrabajo1;
using System.Net.Mail;
using System.Reflection.Metadata;
static bool verificadorEmail(string email)
{
    try
    {
        MailAddress mail = new MailAddress(email);
        return true;
    }
    catch (Exception e)
    {
        return false;
    }
}

List<Usuario> usuarios = new List<Usuario>
{
    new Usuario("wentenviere0@gmail.com"),
    new Usuario("wentenviere32151@gmail.com")
};

Console.WriteLine("INICIO DE SESIÓN");
Console.WriteLine("¿Quiere iniciar sesión como Invitado o como Admin?");
string op = Console.ReadLine();
switch (op)
{
    case "Invitado" or "invitado":
        Console.Clear();
        Console.WriteLine("INICIO DE SESIÓN");
        Console.WriteLine("Ingrese su e-mail para iniciar sesión");
        string mail = Console.ReadLine();
        if (verificadorEmail(mail))
        {
            Console.WriteLine("Iniciaste sesión como invitado correctamente");
        }
        else
        {
            Console.WriteLine("El e-mail no es válido");
        }

        break;
    case "Admin" or "admin":
        Console.Clear();
        Console.WriteLine("INICIO DE SESIÓN");
        Console.WriteLine("Ingrese su e-mail: ");
        string mail1 = Console.ReadLine();
        if (verificadorEmail(mail1))//comprobamos que el email es correcto
        {
            bool comprobador = false;
            Usuario user = new Usuario();
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Email == mail1)
                {
                   comprobador=true;
                   user = usuario;
                }
            }

            if (comprobador == true)
            {
                Console.WriteLine("Ingrese contraseña: ");
                string cont = Console.ReadLine();
                if (user.ComprobadorDeContrasenia(cont))
                {
                    Console.WriteLine("Ingresó correctamente");
                }
                else
                {
                    Console.WriteLine("La contraseña es inválida");
                }
            }
            else
            {
                Console.WriteLine("No se encontró el usuario");
            }
        }
        else
        {
            Console.WriteLine("El e-mail no es válido");
        }
        break;
}