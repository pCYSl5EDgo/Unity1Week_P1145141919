namespace Unity1Week
{
    public struct PlayerSettings
    {
        public float Life;
        public int MaxLife;
        public bool IsAlive => Life > 0 && Temperature < ThermalDeathPoint;
        public float Temperature;
        public float ThermalDeathPoint;
        public bool IsOverHeat => Temperature > ThermalDeathPoint;
    }
}