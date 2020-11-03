using MyAttribute;

namespace ComponentTypes
{
    [Countable(new System.Type[0], new string[0], new string[0], "return IsAliveArray.Reinterpret<AliveState.State>()[index] == AliveState.State.Alive;")]
    public partial struct Enemy
    {
        public Position2D Position;
        public Destination2D Destination;
        public Speed2D Speed;
        public AliveState IsAlive;
    }
}