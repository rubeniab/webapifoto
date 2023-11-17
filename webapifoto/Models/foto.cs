namespace webapifoto.Models
{
    public class foto
    {
        public IFormFile Archivo { get; set; }

        public string Nombre { get; set; }

        public string? Url { get; set; }
    }
}
