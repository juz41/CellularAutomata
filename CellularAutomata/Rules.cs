using CellularAutomata.States;

namespace CellularAutomata;

public static class Rules
{
    public static List<(int a, int b)>[,,] BasicClassic()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();

        // if cell in state a is in range of <a,b> then it stays goes up depending on second index of _rules
        // goes down otherwise
        // (a,b)[i,j,k] -> a,b is range (inclusive)
        // i is current state, j is down/up, last one is all states to match
        rule[(int)Basic.Dead, 1, (int)Basic.Alive].Add((3, 3)); // Becomes alive if exactly 3 alive neighbors
        rule[(int)Basic.Alive, 0, (int)Basic.Alive].Add((0, 1)); // Dies if <2 or >3 alive neighbors
        rule[(int)Basic.Alive, 0, (int)Basic.Alive].Add((4, 8)); // Dies if <2 or >3 alive neighbors
        return rule;
    }
    //https://en.m.wikipedia.org/wiki/Day_and_Night_(cellular_automaton)
    public static List<(int a, int b)>[,,] BasicDayAndNight()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();
        
        rule[(int)Basic.Dead, 1, (int)Basic.Alive].Add((3, 3));
        rule[(int)Basic.Dead, 1, (int)Basic.Alive].Add((6, 8));
        rule[(int)Basic.Alive, 0, (int)Basic.Alive].Add((0, 2));
        rule[(int)Basic.Alive, 0, (int)Basic.Alive].Add((5, 5));
        return rule;
    }
    //https://en.wikipedia.org/wiki/Seeds_(cellular_automaton)
    public static List<(int a, int b)>[,,] BasicSeeds()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();
        rule[(int)Basic.Dead, 1, (int)Basic.Alive].Add((2, 2));
        rule[(int)Basic.Alive, 0, (int)Basic.Alive].Add((0, 8));
        return rule;
    }
    public static List<(int a, int b)>[,,] MapClassic()
    {
        var n = Enum.GetNames(typeof(Map)).Length;
        var rule = new List<(int a, int b)>[n, 2, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < 2; j++)
        for (int k = 0; k < n; k++)
            rule[i, j, k] = new List<(int a, int b)>();

        rule[(int)Map.Water, 1, (int)Map.Beach].Add((6, 8));
        rule[(int)Map.Water, 1, (int)Map.Land].Add((5, 8));
        
        rule[(int)Map.Beach, 0, (int)Map.Water].Add((5, 8));
        rule[(int)Map.Beach, 1, (int)Map.Beach].Add((5, 8));
        rule[(int)Map.Beach, 1, (int)Map.Land].Add((5, 8));

        rule[(int)Map.Land, 0, (int)Map.Water].Add((3, 8));
        rule[(int)Map.Land, 0, (int)Map.Beach].Add((4, 8));
        return rule;
    }


}