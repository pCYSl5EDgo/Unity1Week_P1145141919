using Unity.Collections;
using MyAttribute;

namespace ComponentTypes
{
    public struct ChipKind
    {
        public int Value;
    }
    
    public struct ChipTemperature
    {
        public float Value;
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
    public partial struct Size
    {
        public float Value;
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
