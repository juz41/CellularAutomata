namespace CellularAutomata.Cells
{
    public class Neighborhood<T, TU> 
        where T : Cell<TU> 
        where TU : Enum, IConvertible
    {
        private readonly int _n = Enum.GetNames(typeof(TU)).Length;
        private readonly int[] _states;

        public Neighborhood(int[] states)
        {
            this._states = states;
        }
        public int this[int i]
        {
            get => _states[i % _n];
        }
        public int GetNumber(TU state)
        {
            return _states[(int)(object)state];
        }   
    }
}