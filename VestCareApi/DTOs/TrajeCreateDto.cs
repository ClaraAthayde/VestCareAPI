namespace VestCareApi.DTOs
{
    public class TrajeCreateDto
    {
        public string NomeTraje { get; set; } = string.Empty;

        public string? OcasiaoDestino { get; set; }

        public string? ClimaDestino { get; set; }

        public bool Favorito { get; set; }

        public int IdUsuario { get; set; }
    }
}