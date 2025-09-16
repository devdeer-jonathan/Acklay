using Logic.Models;

namespace Logic.Interfaces
{
    public interface IThreeBodySimulator
    {
        BodiesPositions SimulateNextPosition();
    }
}
