using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class GenericLoggerHelper
    {
        private readonly ILogger<GenericLoggerHelper> _logger;
        public GenericLoggerHelper(ILogger<GenericLoggerHelper> logger)
        {
            _logger = logger;
            _logger.LogInformation(1, "GenericLoggerHelper has been constructed");
        }
        public void JustADumbFunctionCall()
        {
            _logger.LogInformation("JustADumbFunctionCall has been called");
        }
    }
}
