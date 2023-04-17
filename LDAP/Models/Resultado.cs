namespace LDAP.Models
{
    public class Resultado
    {
        public Boolean Exito { get; set; }

        public String Mensaje { get; set; }

        public Object? Objeto { get; set; }

        public Resultado(Boolean Exito_, String Mensaje, String Objeto)
        {
            this.Exito = Exito_;
            this.Mensaje = Mensaje;
            this.Objeto = Objeto;
        }

        public Resultado()
        {
            this.Exito = true;
            this.Mensaje = "Autenticación Exitosa";
            this.Objeto = null;
        }

        public Resultado(System.Runtime.InteropServices.COMException ex)
        {
            this.Exito = false;
            this.Mensaje = ex.Message;
            this.Objeto = ex;
        }
    }
}
