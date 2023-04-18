using Entidades_complejas;
using LDAP.Models;
using System.DirectoryServices;

namespace LDAP
{
    public class ListarUsuarios
    {

        public TipoAccion Listar_Usuarios(string direccion, string nombre)
        {
            try
            {
                var dirEntry = new DirectoryEntry(direccion);
                var searcher = new DirectorySearcher(dirEntry)
                {
                    Filter = "(&(&(objectClass=user)(objectClass=person)(cn=*" + nombre + "*)))"
                };

                SearchResultCollection resultCollection = searcher.FindAll();
                List<Usuarios> listadoUsuarios = new();
                Usuarios usuarios = new();

                foreach (SearchResult result in resultCollection)
                {
                    if (result.Properties.PropertyNames != null)
                    {
                        foreach (string key in result.Properties.PropertyNames)
                        {
                            foreach (object prop in result.Properties[key])
                            {
                                if (key == "name")
                                {
                                    usuarios.Nombre = prop.ToString();
                                }
                                else if (key == "mail")
                                {
                                    usuarios.Correo = prop.ToString();
                                }
                                else if (key == "samaccountname")
                                {
                                    usuarios.Cuenta = prop.ToString();
                                }
                            }
                        }

                        listadoUsuarios.Add(usuarios);
                        usuarios = new();
                    }
                    else
                    {
                        throw new Exception("No existen registros por mostrar.");
                    }
                }

                listadoUsuarios = listadoUsuarios.Where(q => q.Correo != null).ToList();

                return TipoAccion.Positiva(listadoUsuarios);
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
