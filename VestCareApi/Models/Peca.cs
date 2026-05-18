namespace VestCareApi.Models
{
    public class Peca
    {
        public int IdPeca { get; set; }

        public string NomePeca { get; set; } = string.Empty;

        public string? Ocasiao { get; set; }

        public string? Categoria { get; set; }

        public string? Cor { get; set; }

        public string? Estilo { get; set; }

        public string? Clima { get; set; }

        public string? UrlFoto { get; set; }

        public int IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        public List<TrajePeca> TrajePecas { get; set; } = new();
    }
}