using CellularAutomata.Cells;
using CellularAutomata.States;

namespace CellularAutomata;

public static class RulesBase
{
    public static Rules<Basic> BasicClassic()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(Func<Neighborhood<Cell<Basic>, Basic>, bool>, Basic)>[n];
        for (int i = 0; i < n; i++)
            rule[i] = new ();

        // if cell in state a is in range of <a,b> then it stays goes up depending on second index of _rules
        // goes down otherwise
        // (a,b)[i,j,k] -> a,b is range (inclusive)
        // i is current state, j is down/up, last one is all states to match
        rule[(int)Basic.Dead].Add((nei => nei[(int)Basic.Alive] == 3, Basic.Alive));
        rule[(int)Basic.Alive].Add((nei => nei[(int)Basic.Alive] >= 0 && nei[(int)Basic.Alive] <= 1, Basic.Dead));
        rule[(int)Basic.Alive].Add((nei => nei[(int)Basic.Alive] >= 4 && nei[(int)Basic.Alive] <= 8, Basic.Alive));
        return new Rules<Basic>(rule);
    }
    //https://en.m.wikipedia.org/wiki/Day_and_Night_(cellular_automaton)
    public static List<(Func<Neighborhood<Cell<Basic>, Basic>, bool>, Basic)>[]? BasicDayAndNight()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(Func<Neighborhood<Cell<Basic>, Basic>, bool>, Basic)>[n];
        for (int i = 0; i < n; i++)
            rule[i] = new ();
        
        rule[(int)Basic.Dead].Add((nei => nei[(int)Basic.Alive] == 3, Basic.Alive));
        rule[(int)Basic.Dead].Add((nei => 6 <= nei[(int)Basic.Alive] || nei[(int)Basic.Alive] >= 8, Basic.Alive));
        rule[(int)Basic.Alive].Add((nei => nei[(int)Basic.Alive] >= 0 && nei[(int)Basic.Alive] <= 2, Basic.Dead));
        rule[(int)Basic.Alive].Add((nei => nei[(int)Basic.Alive] == 5, Basic.Dead));
        return rule;
    }
    //https://en.wikipedia.org/wiki/Seeds_(cellular_automaton)
    public static Rules<Basic> BasicSeeds()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(Func<Neighborhood<Cell<Basic>, Basic>, bool>, Basic)>[n];
        for (int i = 0; i < n; i++)
            rule[i] = new();
        
        rule[(int)Basic.Dead].Add((nei => nei[(int)Basic.Alive] == 2, Basic.Alive));
        rule[(int)Basic.Alive].Add((nei => nei[(int)Basic.Alive] >= 0 && nei[(int)Basic.Alive] <= 8, Basic.Dead));
        return new Rules<Basic>(rule);
    }
    public static Rules<Map> MapClassic()
    {
        int n = Enum.GetNames(typeof(Map)).Length;
        var rule = new List<(Func<Neighborhood<Cell<Map>, Map>, bool> func, Map state)>[n];
        for (int i = 0; i < n; i++)
            rule[i] = new();
        
        rule[(int)Map.Water].Add((nei => nei[(int)Map.Beach] >= 6 && nei[(int)Map.Beach] <= 8, Map.Beach));
        rule[(int)Map.Water].Add((nei => nei[(int)Map.Land] >= 5 && nei[(int)Map.Land] <= 8, Map.Beach));
        
        rule[(int)Map.Beach].Add((nei => nei[(int)Map.Water] >= 5 && nei[(int)Map.Water] <= 8, Map.Water));
        rule[(int)Map.Beach].Add((nei => nei[(int)Map.Beach] >= 5 && nei[(int)Map.Beach] <= 8, Map.Land));
        rule[(int)Map.Beach].Add((nei => nei[(int)Map.Land] >= 5 && nei[(int)Map.Land] <= 8, Map.Land));
        
        rule[(int)Map.Land].Add((nei => nei[(int)Map.Water] >= 3 && nei[(int)Map.Water] <= 8, Map.Beach));
        rule[(int)Map.Land].Add((nei => nei[(int)Map.Beach] >= 4 && nei[(int)Map.Beach] <= 8, Map.Beach));
        return new Rules<Map>(rule);
    }
    public static Rules<Basic> DuelClassic()
    {
        int n = Enum.GetNames(typeof(Basic)).Length;
        var rule = new List<(Func<Neighborhood<Cell<Basic>, Basic>, bool> func, Basic state)>[n];
        for (int i = 0; i < n; i++)
            rule[i] = new();
        
        rule[(int)Basic.Dead].Add((nei => (new Random()).NextInt64(nei.Sum) < nei[(int) Basic.Alive], Basic.Alive));
        rule[(int)Basic.Alive].Add((nei => (new Random()).NextInt64(nei.Sum) < nei[(int) Basic.Dead], Basic.Dead));
        
        return new Rules<Basic>(rule);
    }
    
    // TODO make Rules class more generic with States
    // TODO seperate class for Rules
}