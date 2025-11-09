using System;
using System.ComponentModel.DataAnnotations;

namespace Mottu.Uwb.Api.Models
{
    public class Localizacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MotoId { get; set; }

        [Required]
        public int SensorId { get; set; }

        [Required]
        public DateTime DetectedAt { get; set; } = DateTime.UtcNow;

        public float? X { get; set; }
        public float? Y { get; set; }
        public float? Rssi { get; set; }
    }
}
