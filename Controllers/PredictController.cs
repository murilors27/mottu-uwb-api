using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/predict")]
    public class PredictController : ControllerBase
    {
        [HttpPost("manutencao")]
        public ActionResult<ManutencaoPrediction> PredictManutencao([FromBody] ManutencaoData input)
        {
            var mlContext = new MLContext();

            // 🔹 Dados simulados com coluna Label obrigatória (bool)
            var dadosTreino = new List<ManutencaoDataTrain>
            {
                new() { Quilometragem = 1000,  TempoUsoMeses = 1,  Label = false },
                new() { Quilometragem = 4000,  TempoUsoMeses = 5,  Label = false },
                new() { Quilometragem = 8000,  TempoUsoMeses = 10, Label = true },
                new() { Quilometragem = 12000, TempoUsoMeses = 15, Label = true }
            };

            var treinoData = mlContext.Data.LoadFromEnumerable(dadosTreino);

            // 🔹 Define pipeline de treino (agora com Label booleano)
            var pipeline = mlContext.Transforms.Concatenate("Features",
                    nameof(ManutencaoDataTrain.Quilometragem),
                    nameof(ManutencaoDataTrain.TempoUsoMeses))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: "Label", featureColumnName: "Features"));

            var modelo = pipeline.Fit(treinoData);

            // 🔹 Cria mecanismo de predição
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ManutencaoData, ManutencaoPredictionML>(modelo);

            var resultadoML = predictionEngine.Predict(input);

            // 🔹 Converte o score em probabilidade (função sigmoide)
            float prob = 1f / (1f + MathF.Exp(-resultadoML.Score));

            var resposta = new ManutencaoPrediction
            {
                RequerManutencao = prob >= 0.5f,
                Probabilidade = prob
            };

            Console.WriteLine($"[ML.NET] Probabilidade calculada: {prob:P2}");

            return Ok(resposta);
        }

        // 🔸 Classe interna usada para treino (agora com Label bool)
        private class ManutencaoDataTrain
        {
            public float Quilometragem { get; set; }
            public float TempoUsoMeses { get; set; }
            public bool Label { get; set; }
        }

        // 🔸 Classe de saída interna do modelo
        private class ManutencaoPredictionML
        {
            public float Score { get; set; }
        }
    }
}
