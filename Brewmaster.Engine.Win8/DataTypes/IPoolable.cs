namespace BrewmasterEngine.DataTypes
{
    public interface IPoolable
    {
        bool IsFree { get; set; }

        void Reset();
    }
}
