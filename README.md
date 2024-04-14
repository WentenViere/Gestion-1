# Inicio de sesión
Un simple inicio de sesión para administradores e invitados de un sistema

## Casos de uso 
<img src="Casos de uso/Caso de uso Login.png">
<img src="Casos de uso/Caso de uso Crear Cuenta.png">

---

### Nombre del caso de uso:
Inicio de sesión como administrador

---

### Precondición: 
El administrador existe en el sistema.

---

### Poscondición: 
El administrador ingresa con éxito.

---

### Flujo principal:
  1. El sistema solicita el tipo de usuario.
  2. Usuario selecciona "admin".
  3. El sistema solicita un e-mail.
  4. Usuario ingresa un e-mail.
  5. El sistema verifica que el e-mail sea válido.
  6. El sistema solicita una contraseña.
  7. El usuario ingresa una contraseña.
  8. El sistema verifica la contraseña.
  9. Inicio de sesión exitoso.
  10. Fin del caso de uso.

---

### Flujo alternativo A:
  5.1 El e-mail que ingresó el usuario no es válido y el sistema emite un error.  
  6. Fin del caso de uso
  
---

### Flujo alternativo B:
  8.1 La contraseña que ingresó el usuario no es válida y el sistema emite un error.  
  9. Fin del caso de uso
  
---
---

### Nombre del caso de uso:
Inicio de sesión como invitado

---

### Precondición: 
Ninguna

---

### Poscondición: 
El invitado ingresa con éxito.

---

### Flujo principal:
  1. El sistema solicita el tipo de usuario.
  2. Usuario selecciona "invitado".
  3. El sistema solicita un e-mail.
  4. Usuario ingresa un e-mail.
  5. El sistema verifica que el e-mail sea válido.
  6. Inicio de sesión exitoso.
  7. Fin del caso de uso.

---

### Flujo alternativo:
  5.1 El e-mail que ingresó el usuario no es válido y el sistema emite un error.  
  6. Fin del caso de uso
  
---
---



## Muestra de código

### Clase Usuario
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Login_Gestion
{
    public class Usuario
    {
        public Usuario() {
            Email = "";
            Contraseña = "";
        }
        public Usuario(string email,string contraseña)
        {
            Email = email;
            Contraseña = contraseña;
        }
        public string Email { get; set; }
        public string Contraseña { get; set; }
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
```

### Ventana de inicio de sesión
```C#
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace Login_Gestion
{
    public partial class Form1 : Form
    {
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

        public static ListaUsuarios listaUsuarios = new ListaUsuarios();
        public Form1()
        {
            InitializeComponent();
            lblResultado.Visible = false;
            lblEstadoinicio.Visible = false;
            lblContraseña.Visible = false;
            txtContraseña.Visible = false;
            txtContraseña.PasswordChar = '·';
            btnCrearUsuario.Visible = false;
            btnVer.Visible = false;

            Usuario admin = new Usuario("admin@gmail.com", "admin1234");
            listaUsuarios.AgregarUsuario(admin);

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            lblResultado.Visible = false;
            lblEstadoinicio.Visible = false;


            if (verificadorEmail(txtEmail.Text))
            {
                if (chkInvitado.Checked)
                {
                    lblEstadoinicio.Text = "Inicio de sesión exitoso";
                    lblEstadoinicio.Visible = true;
                    Usuario invitado = new Usuario(txtEmail.Text, "");
                }
                else
                {
                    if (listaUsuarios.ExisteUsuario(txtEmail.Text))
                    {
                        if ((listaUsuarios.BuscarUsuario(txtEmail.Text).Contraseña).Equals(txtContraseña.Text))
                        {
                            lblEstadoinicio.Visible = true;
                            lblEstadoinicio.Text = "Inicio de sesión exitoso";
                            btnCrearUsuario.Visible = true;
                        }
                        else
                        {
                            lblResultado.Visible = true;
                            lblResultado.Text = "La contraseña es incorrecta";
                        }
                    }
                    else
                    {
                        lblResultado.Text = "El usuario no existe";
                        lblResultado.Visible = true;
                    }
                }
            }
            else
            {
                lblResultado.Text = "El email es incorrecto.";
                lblResultado.Visible = true;
            }

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (btnVer.Text == "Ver")
            {
                btnVer.Text = "Ocultar";
                txtContraseña.PasswordChar = '\0';
            }
            else
            {
                btnVer.Text = "Ver";
                txtContraseña.PasswordChar = '·';
            }

        }

        private void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
            lblResultado.Text = "";
            lblEstadoinicio.Text = "";
            if (chkAdmin.Checked) { chkInvitado.Checked = false; }
            txtContraseña.Visible = true;
            lblContraseña.Visible = true;
            btnVer.Visible = true;
        }

        private void chkInvitado_CheckedChanged(object sender, EventArgs e)
        {
            lblResultado.Text = "";
            lblEstadoinicio.Text = "";
            if (chkInvitado.Checked) { chkAdmin.Checked = false; }
            txtContraseña.Visible = false;
            lblContraseña.Visible = false;
            btnVer.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            lblResultado.Text = "";
            lblEstadoinicio.Text = "";
            txtEmail.Text = "";
            txtContraseña.Text = "";
            btnCrearUsuario.Visible = false;

        }
    }
}
```

### Ventana para crear una cuenta
```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Gestion
{
    public partial class Form2 : Form
    {
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
       
        public Form2()
        {
            InitializeComponent();
            label2.Visible = false;
            txtContraseña.Visible = false;
            lblResultado.Visible = false;
            btnVer.Visible = false;
            btnCrearCuenta.Visible = false; 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btnVer.Text == "Ver")
            {
                btnVer.Text = "Ocultar";
                txtContraseña.PasswordChar = '\0';
            }
            else
            {
                btnVer.Text = "Ver";
                txtContraseña.PasswordChar = '·';
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            lblResultado.Visible = false;
            if (verificadorEmail(txtEmail.Text) && !Form1.listaUsuarios.ExisteUsuario(txtEmail.Text))
            {
                label2.Visible = true;
                txtContraseña.Visible = true;
                btnVer.Visible = true;
                btnCrearCuenta.Visible = true;
                btnCrear.Visible = false;
                

            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Error con el Email.";

            }
            


        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            user.Email = txtEmail.Text;
            if (user.ComprobadorDeContrasenia(txtContraseña.Text))
            {
                Usuario usuario = new Usuario(txtEmail.Text, txtContraseña.Text);
                Form1.listaUsuarios.AgregarUsuario(usuario);
                lblResultado.Visible = true;
                lblResultado.Text = "Usuario creado exitosamente.";
            }
            else
            {
                lblResultado.Visible = true;
                lblResultado.Text = "Contraseña no válida.";
            }
        }
    }
}
```

## Analicemos esto

### Clase Usuario
Empecemos por la clase Usuario

---

### El usuario
```C#
public Usuario() {
    Email = "";
    Contraseña = "";
}
public Usuario(string email,string contraseña)
{
    Email = email;
    Contraseña = contraseña;
}
```
Creamos una clase sólo para el usuario con el propósito de facilitar su gestión y la comprobación de sus credenciales (E-Mail y contraseña).

"Usuario" es un objeto con dos atributos: su mail y contraseña.

### Los métodos del objeto Usuario
La clase "Usuario" tiene tres métodos: Los Getter/Setter, un verificador de caracteres alfanuméricos y finalmente un verificador de contraseñas.

```C#
public string Email { get; set; }

public bool ComprobadorDeContrasenia(string contrasenia) {
    ...
}

public bool ComprobarAlfaNum(string contrasenia)
{
    ...
}
```
Como mencionamos antes, se hicieron con la intención de gestionar al objeto "Usuario". Los Getter/Setter son para definir al objeto y los otros dos métodos los utilizamos
para corroborar diferentes aspectos de la contraseña y así verificar si es válida o no. Los parámetros que elegimos para una contraseña válida fueron los siguientes:
- Longitud mayor a 8 caracteres
- Sólo caracteres alfanuméricos
- Contraseña diferente al usuario

---

### Lista de usuarios

```C#
 public ListaUsuarios() 
 {
     List<Usuario> lista=new List<Usuario>();
     Lista = lista;
 }
```
 Esta clase es utilizada para almacenar a los diferentes usuarios, verificar su existencia y agregarlos o eliminarlos si es necesario.

### Los métodos de la lista de usuarios

```C#
 public List<Usuario> Lista {  get; set; }

 public void AgregarUsuario(Usuario user)
 {
    ...
 }

 public void EliminarUsuario(string user)
 {
    ...
 }

public bool ExisteUsuario(string user)
{
    ...
}

public Usuario BuscarUsuario(string user)
{
    ...
}
```

Los nombres de los métodos son dan bastante información: Podemos buscar, agregar o eliminar un usuario.

```C#
public Usuario BuscarUsuario(string user)
{
    foreach (Usuario u in Lista)
    {
        if (u.Email.Equals(user))
        {
           return u;
        }
    }
    Usuario usuarioinexistente = new Usuario();
    return usuarioinexistente;
}
```

El método "BuscarUsuario" recorre toda nuestra lista en búsqueda del nombre de usuario que recibe como parámetro y lo devuelve si lo encuentra. Si no lo hace, devuelve un usuario vacío.

```C#
public bool ExisteUsuario(string user)
{
    foreach (Usuario u in Lista)
    {
        if (u.Email.Equals(user))
        {
            return true;
        }
    }
    return false; 
}
```
Este método recorre la lista para verificar si existe un usuario.

```C#
public void EliminarUsuario(string user)
{
    if(Lista.Count()>0) {
        foreach (Usuario u in Lista)
        {
            if (u.Email.Equals(user))
            {
                List<Usuario> lista = new List<Usuario>();
                lista = Lista;
                lista.Remove(u);
                Lista = lista;
            }
        }
    }
    
}


public void AgregarUsuario(Usuario user)
{
    if (Lista.Count() == 0)
    {

        List<Usuario> lista = new List<Usuario>();
        lista = Lista;
        lista.Add(user);
        Lista = lista;              
    }
    else
    {
        foreach (Usuario u in Lista)
        {
            if (!u.Email.Equals(user.Email))
            {                    
                List<Usuario> lista = new List<Usuario>();
                lista = Lista;
                lista.Add(user);
                Lista = lista;
                break;
            }
        }
    }
}
```

Finalmente, AgregarUsuario y EliminarUsuario. Amos recorren la lista en búsqueda de un usuario y lo agrega si no existe en el caso de AgregarUsuario o lo elimina si lo encuentra en EliminarUsuario.

---

### Programa principal
Ahora explicaremos el flujo del programa principal.



### Inicialización de los componentes

```C#
public Form1()
{
    InitializeComponent();
    lblResultado.Visible = false;
    lblEstadoinicio.Visible = false;
    lblContraseña.Visible = false;
    txtContraseña.Visible = false;
    txtContraseña.PasswordChar = '·';
    btnCrearUsuario.Visible = false;
    btnVer.Visible = false;

    Usuario admin = new Usuario("admin@gmail.com", "admin1234");
    listaUsuarios.AgregarUsuario(admin);

}
```
En esta sección de código se inicializan los componentes gráficos. Algunos los marcamos como invisibles hasta que el usuario elija la opción de "Administrador".

Los e-mail de los administradores son definidos en una lista al inicio de la ejecución. Sólo estos e-mail son válidos para el ingreso como administrador.

### Solicitud de información

```C#
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

Console.WriteLine("INICIO DE SESIÓN");
Console.WriteLine("¿Quiere iniciar sesión como Invitado o como Admin?");
string op = Console.ReadLine();
switch (op)
{
    case "Invitado" or "invitado":
        ...
        break;
    case "Admin" or "admin":
       ...
        break;

    default:
        Console.WriteLine("Ingrese una opción válida");
        break;
}
```
Le pedimos al usuario que elija entre ingresar como Admin o Invitado. El checkbox nos facilita la escritura de código para cada usuario, además de que nos permite agregar un caso 
por si no se escribió correctamente lo solicitado.

El método "verificadorEmail" nos servirá más adelante, pero compara el e-mail ingresado con un objeto de tipo Mail. Nos permite saber si es una dirección válida.

### Caso 1: invitado

```C#
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
```

El invitado es el usuario sin contraseña y poco acceso a un futuro sistema. Como tal, sólo tenemos que solicitar un e-mail, verificarlo y si es válido, le damos acceso. En el caso
contrario, le negamos el acceso.

### Caso 2: Admin

```C#
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
```

El administrador va a tener mayores permisos a un futuro sistema. Nosotros le exigimos para mayor seguridad una contraseña con características ya mencionadas.
El código es muy similar; Sólo hay que verificar la contraseña.

```C#
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
```



## Referencias
Clase "MailAddress"
https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.mailaddress?view=net-8.0


## Contribuidores 
- Federico González
- Máximo Castro
- Santiago Hornos
- Wenten Viere
- 
