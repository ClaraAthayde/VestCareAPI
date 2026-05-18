namespace VestCareApi.Models
{
    public class TrajePeca
    {
        public int IdTraje { get; set; }

        public int IdPeca { get; set; }

        public Traje? Traje { get; set; }

        public Peca? Peca { get; set; }
    }
}