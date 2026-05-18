namespace VestCareApi.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }

        public List<Peca> Pecas { get; set; } = new();

        public List<Traje> Trajes { get; set; } = new();
    }
}