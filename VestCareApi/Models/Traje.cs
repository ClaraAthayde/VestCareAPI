namespace VestCareApi.Models
{
    public class Traje
    {
        public int IdTraje { get; set; }

        public string NomeTraje { get; set; } = string.Empty;

        public string? OcasiaoDestino { get; set; }

        public string? ClimaDestino { get; set; }

        public bool Favorito { get; set; }

        public int IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        public List<TrajePeca> TrajePecas { get; set; } = new();
    }
}