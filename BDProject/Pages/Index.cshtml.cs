using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;

namespace BDProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<MLModel.ModelInput, MLModel.ModelOutput> _predictionEnginePool;

        public IndexModel(PredictionEnginePool<MLModel.ModelInput, MLModel.ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        public void OnGet()
        {

        }
        public IActionResult OnGetAnalyzeSentiment([FromQuery] string text)
        {
            if (String.IsNullOrEmpty(text)) return Content("Neutral");
            var input = new MLModel.ModelInput { Col0 = text };
            var prediction = _predictionEnginePool.Predict(input);
            float x = prediction.PredictedLabel;
            var sentiment = Convert.ToBoolean(x) ? "Not Toxic" : "Toxic";
            return Content(sentiment);




        }
    }
}
