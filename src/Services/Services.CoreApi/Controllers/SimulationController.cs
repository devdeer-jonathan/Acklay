using Microsoft.AspNetCore.Mvc;

namespace Services.CoreApi.Controllers
{
    using Logic.Interfaces;
    using Logic.Models;

    [ApiController]
    [Route("[controller]")]
    public class SimulationController : ControllerBase
    {

        private readonly ILogger<SimulationController> _logger;

        public SimulationController(IThreeBodySimulator simulator)
        {
            Simulator = simulator;
        }

        [HttpGet("GetNextPositions")]
        public BodiesPositions GetNextPositions()
        {
            return Simulator.SimulateNextPosition();
        }

        private IThreeBodySimulator Simulator { get; set; }
    }
}
