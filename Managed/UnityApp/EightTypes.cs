using Unity.Collections;
using MyAttribute;

namespace ComponentTypes
{
    public static class Z
    {
        public static Destination2D.Eight a;
    }

    [Eight]
    public partial struct AliveState
    {
        public State Value;

        public enum State
        {
            Alive,
            Dead = -1,
        }
    }

    [Eight]
    public partial struct Destination2D
    {
        public float X;
        public float Y;
    }

    [Eight]
    public partial struct Position2D
    {
        public float X;
        public float Y;
    }

    [Eight]
    public partial struct Speed2D
    {
        public float X;
        public float Y;
    }

    [Eight]
    public partial struct FireStartTime
    {
        public float Value;
    }
}
