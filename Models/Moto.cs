namespace Mottu.Uwb.Api.Models
{
    public class Moto
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = null!;
        public string Cor { get; set; } = null!;
        public string IdentificadorUWB { get; set; } = null!;
        public int SensorId { get; set; }
        public string Status { get; set; } = "Disponível";

        public Sensor? Sensor { get; set; }
    }
}
