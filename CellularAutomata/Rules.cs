using CellularAutomata.Cells;
using CellularAutomata.States;

namespace CellularAutomata;

public static class Rules
{
    public static List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>[,] BasicClassic()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>[n,n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            rule[i, j] = new List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>();

        // if cell in state a is in range of <a,b> then it stays goes up depending on second index of _rules
        // goes down otherwise
        // (a,b)[i,j,k] -> a,b is range (inclusive)
        // i is current state, j is down/up, last one is all states to match
        rule[(int)Basic.Dead, (int)Basic.Alive].Add(nei => nei[(int)Basic.Alive] == 3);
        rule[(int)Basic.Alive, (int)Basic.Dead].Add(nei => nei[(int)Basic.Alive] >= 0 && nei[(int)Basic.Alive] <= 1);
        rule[(int)Basic.Alive, (int)Basic.Alive].Add(nei => nei[(int)Basic.Alive] >= 4 && nei[(int)Basic.Alive] <= 8);
        return rule;
    }
    //https://en.m.wikipedia.org/wiki/Day_and_Night_(cellular_automaton)
    public static List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>[,] BasicDayAndNight()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>[n,n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            rule[i, j] = new List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>();
        
        rule[(int)Basic.Dead, (int)Basic.Alive].Add(nei => nei[(int)Basic.Alive] == 3);
        rule[(int)Basic.Dead, (int)Basic.Alive].Add(nei => 6 <= nei[(int)Basic.Alive] || nei[(int)Basic.Alive] >= 8);
        rule[(int)Basic.Alive, (int)Basic.Dead].Add(nei => nei[(int)Basic.Alive] >= 0 && nei[(int)Basic.Alive] <= 2);
        rule[(int)Basic.Alive, (int)Basic.Dead].Add(nei => nei[(int)Basic.Alive] == 5);
        return rule;
    }
    //https://en.wikipedia.org/wiki/Seeds_(cellular_automaton)
    public static List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>[,] BasicSeeds()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>[n,n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            rule[i, j] = new List<Func<Neighborhood<Cell<Basic>, Basic>,bool>>();
        
        rule[(int)Basic.Dead, (int)Basic.Alive].Add(nei => nei[(int)Basic.Alive] == 2);
        rule[(int)Basic.Alive, (int)Basic.Dead].Add(nei => nei[(int)Basic.Alive] >= 0 && nei[(int)Basic.Alive] <= 8);
        return rule;
    }
    public static List<Func<Neighborhood<Cell<Map>, Map>,bool>>[,] MapClassic()
    {
        int n = Enum.GetNames(typeof(Map)).Length;
        var rule = new List<Func<Neighborhood<Cell<Map>, Map>,bool>>[n,n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            rule[i, j] = new List<Func<Neighborhood<Cell<Map>, Map>,bool>>();
        
        rule[(int)Map.Water, (int)Map.Beach].Add(nei => nei[(int)Map.Beach] >= 6 && nei[(int)Map.Beach] <= 8);
        rule[(int)Map.Water, (int)Map.Beach].Add(nei => nei[(int)Map.Land] >= 5 && nei[(int)Map.Land] <= 8);
        
        rule[(int)Map.Beach, (int)Map.Water].Add(nei => nei[(int)Map.Water] >= 5 && nei[(int)Map.Water] <= 8);
        rule[(int)Map.Beach, (int)Map.Land].Add(nei => nei[(int)Map.Beach] >= 5 && nei[(int)Map.Beach] <= 8);
        rule[(int)Map.Beach, (int)Map.Land].Add(nei => nei[(int)Map.Land] >= 5 && nei[(int)Map.Land] <= 8);
        
        rule[(int)Map.Land, (int)Map.Beach].Add(nei => nei[(int)Map.Water] >= 3 && nei[(int)Map.Water] <= 8);
        rule[(int)Map.Land, (int)Map.Beach].Add(nei => nei[(int)Map.Beach] >= 4 && nei[(int)Map.Beach] <= 8);
        return rule;
    }
}