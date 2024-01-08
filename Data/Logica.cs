using ProyectoCore.Models;

namespace ProyectoCore.Data

{
    public class Logica
    {
        public List<Usuario> ListaUsuario()
        {
            return new List<Usuario>
            {
                new Usuario { Nombre = "Admin" , Correo = "admin@hotmail.com", Clave = "123", Roles = new string []{"Administrador"} },

                new Usuario { Nombre = "GerenteTecnico" , Correo = "gerenteTecnico@hotmail.com", Clave = "123", Roles = new string []{"GerenteTecnico"} },

                new Usuario { Nombre = "JefePostCosecha" , Correo = "jefePostCosecha@hotmail.com", Clave = "123", Roles = new string []{"JefePostCosecha"} },
            };
        }
        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        }
    }
}
