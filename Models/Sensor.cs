namespace Mottu.Uwb.Api.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Localizacao { get; set; } = null!;
        public string Patio { get; set; } = null!;

        public ICollection<Moto>? Motos { get; set; }
    }
}

