namespace CellularAutomata.Cells
{
    public class Neighborhood<T, U> 
        where T : Cell<U> 
        where U : Enum, IConvertible
    {
        private readonly int _n = Enum.GetNames(typeof(U)).Length;
        private int[] states;

        public Neighborhood(int[] states)
        {
            this.states = states;
        }
        public int this[int i]
        {
            get => states[i % _n];
        }
        public int GetNumber(U state)
        {
            return states[(int)(object)state];
        }   
    }
}