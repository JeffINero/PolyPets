using UnityEngine;
using UnityEditor;
using System;

/**
 * PolyPetStatsHistory Klasse
 */
public class PolyPetStatsHistory
{
    private long hungerSatisfied;
    private long thirstSatisfied;
    private long playingSatisfied;
    private long movmentSatisfied;
    private DateTime date;

    /**
     * Konstruktor.
     */
    public PolyPetStatsHistory(long hungerSatisfied, long thirstSatisfied, long playingSatisfied, long movmentSatisfied)
    {
        this.hungerSatisfied = hungerSatisfied;
        this.thirstSatisfied = thirstSatisfied;
        this.playingSatisfied = playingSatisfied;
        this.movmentSatisfied = movmentSatisfied;

        date = DateTime.Now;
    }

    // Get the stats.

    public long HungerSatisfied { get { return hungerSatisfied; } }
    public long ThirstSatisfied { get { return thirstSatisfied; } }
    public long PlayingSatisfied { get { return playingSatisfied; } }
    public long MovmentSatisfied { get { return movmentSatisfied; } }
    public DateTime Date { get { return date; } }
}