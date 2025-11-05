using Core;

namespace PlayerStrategies
{
    public interface IPlayer
    {
        void Move(Cell selectedCell);
    }
}