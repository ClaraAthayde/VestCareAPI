namespace VestCareApi.DTOs
{
    public class PecaCreateDto
    {
        public string NomePeca { get; set; } = string.Empty;

        public string? Ocasiao { get; set; }

        public string? Categoria { get; set; }

        public string? Cor { get; set; }

        public string? Estilo { get; set; }

        public string? Clima { get; set; }

        public string? UrlFoto { get; set; }

        public int IdUsuario { get; set; }
    }
}