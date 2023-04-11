namespace Entidades_complejas
{
    public class TipoAccion
    {
        public int? id { get; set; }
        public Boolean? Exito { get; set; }
        public String? Mensaje { get; set; }
        public Object? Input { get; set; }
        public Object? Output { get; set; }
        public Object? Error { get; set; }

        public static TipoAccion Positiva(String Mensaje, int id, object Output)
        {
            TipoAccion response = new TipoAccion();
            response.Exito = true;
            response.id = id;
            response.Mensaje = Mensaje;
            response.Output = Output;
            return response;
        }
        public static TipoAccion Positiva(String Mensaje, int id)
        {
            TipoAccion response = new TipoAccion();
            response.Exito = true;
            response.id = id;
            response.Mensaje = Mensaje;
            return response;
        }
        public static TipoAccion Positiva(String Mensaje)
        {
            TipoAccion response = new TipoAccion();
            response.Exito = true;
            response.Mensaje = Mensaje;
            return response;
        }
        public static TipoAccion Positiva(object Output)
        {
            TipoAccion response = new TipoAccion();
            response.Exito = true;
            response.Output = Output;
            return response;
        }
        public static TipoAccion Positiva(int id)
        {
            TipoAccion response = new TipoAccion();
            response.Exito = true;
            response.id = id;
            return response;
        }
   
        public static TipoAccion Negativa(String Mensaje, Object Input, Object Error)
        {
            TipoAccion response = new TipoAccion();
            response.Mensaje = Mensaje;
            response.Input = Input;
            response.Error = Error;
            response.Exito = false;
            return response; 
        }
        public static TipoAccion Negativa(String Mensaje, Object Error)
        {
            TipoAccion response = new TipoAccion();
            response.Mensaje = Mensaje;
            response.Error = Error;
            response.Exito = false;
            return response;
        }
        public static TipoAccion Negativa(String Mensaje)
        {
            TipoAccion response = new TipoAccion();
            response.Mensaje = Mensaje;
            response.Exito = false;
            return response;
        }
        public static TipoAccion Negativa(Object Error)
        {
            TipoAccion response = new TipoAccion();
            response.Error = Error;
            response.Exito = false;
            return response;
        }

    }

}
