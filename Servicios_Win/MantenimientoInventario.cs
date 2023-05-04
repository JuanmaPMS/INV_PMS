using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Servicios_Win
{
    public class MantenimientoInventario : BackgroundService
    {
        private readonly ILogger<MantenimientoInventario> _logger;

        public MantenimientoInventario(ILogger<MantenimientoInventario> logger)
        {
            _logger = logger;
        }
        private IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             
            while (!stoppingToken.IsCancellationRequested)
            {
                string url = Configuration.GetSection("serviciosApi").GetValue<string>("ListaMantenimiento");
                var request = (HttpWebRequest)WebRequest.Create(url);

                var postData = "esPorCorreo=" + Uri.EscapeDataString("true");
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                TipoAccion ta = new();
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    ta = JsonSerializer.Deserialize<TipoAccion>(responseText);
                }

                _logger.LogInformation("{mensaje}: {time}", ta.mensaje, DateTimeOffset.Now);
                await Task.Delay(300000, stoppingToken); //100000000 1 dia 3 hrs

            }
        }
    }

    public class TipoAccion
    {
        public int? id { get; set; }
        public bool? exito { get; set; }
        public string? mensaje { get; set; }
        public object? input { get; set; }
        public object? output { get; set; }
        public object? error { get; set; }
    }
}