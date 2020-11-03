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

    [Countable(new System.Type[0], new string[0], new string[0], "return IsAliveArray.Reinterpret<AliveState.State>()[index] == AliveState.State.Alive;")]
    public partial struct EnemyAttack
    {
        public Position2D Position;
        public Speed2D Speed;
        public AliveState IsAlive;
    }

    [Countable(new System.Type[0], new string[0], new string[0], "return IsAliveArray.Reinterpret<AliveState.State>()[index] == AliveState.State.Alive;")]
    public partial struct PlayerBullet
    {
        public Position2D Position;
        public Speed2D Speed;
        public AliveState IsAlive;
    }

    [Countable(new System.Type[] { typeof(float) }, new string[] { "deadTime" }, new string[] { "DeadTime" }, "return StartTimeArray.Reinterpret<float>()[index] > deadTime;")]
    public partial struct PlayerFire
    {
        public Position2D Position;
        public Speed2D Speed;
        public FireStartTime StartTime;
    }
}
