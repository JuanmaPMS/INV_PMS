using LDAP.Models;
using System.DirectoryServices;

namespace LDAP
{
    public class ListarUsuarios
    {
        private Resultado Response;

        public Resultado Listar_Usuarios()
        {
            try
            {
                // TODO:
                var dirEntry = new DirectoryEntry("LDAP://Grupopm.mx:389");
                var searcher = new DirectorySearcher(dirEntry)
                {
                    Filter = "(&(&(objectClass=user)(objectClass=person)))"
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

                Response = new()
                {
                    Mensaje = "Se encontraron usuarios por listar.",
                    Exito = true,
                    Objeto = listadoUsuarios
                };

                return Response;
            }
            catch (Exception ex)
            {
                Response = new()
                {
                    Mensaje = ex.Message,
                    Exito = false,
                    Objeto = null
                };

                return Response;
            }
        }
    }
}
