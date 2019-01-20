using UnityEngine;
using UnityEditor;
using System;
using System.Threading;
using System.Collections.Generic;

/**
 * PolyPet Klasse
 */
public class PolyPet
{
    private string name;
    private int hungerSatisfied;
    private int thirstSatisfied;
    private int playingSatisfied;
    private int movmentSatisfied;
    private List<PolyPetStatsHistory> history = new List<PolyPetStatsHistory>();

    /**
     * Konstruktor der das PolyPet erstellt.
     */
    public PolyPet(string name, int hungerSatisfied, int thirstSatisfied, int playingSatisfied, int movmentSatisfied, DateTime lastUpdate)
    {
        this.name = name;
        this.hungerSatisfied = hungerSatisfied;
        this.thirstSatisfied = thirstSatisfied;
        this.playingSatisfied = playingSatisfied;
        this.movmentSatisfied = movmentSatisfied;

        // Wie oft muss "updateStats" aufgerufen werden, damit die Stats angepasst werden auf die vergangene Zeit.
        // ("updateStats" wird standard mäßig all 15 Minuten aufgerufen (900 Sekunden))
        int tenMinutesTimesPassed =  (int) ((DateTime.Now.Subtract(lastUpdate)).TotalSeconds / 900.0);

        // Für die verpasste Zeit "updateStats" aufrufen.
        for (int i = 0; i < tenMinutesTimesPassed; i++)
        {
            updateStats(null);
        }
    }

    /**
     * Updated die Stats von dem PolyPet
     */
    public void updateStats(object state)
    {
        hungerSatisfied -= 11;
        thirstSatisfied -= 16;
        playingSatisfied -= 14;
        movmentSatisfied -= 24;

        hungerSatisfied = hungerSatisfied < 0 ? 0 : hungerSatisfied;
        thirstSatisfied = thirstSatisfied < 0 ? 0 : thirstSatisfied;
        playingSatisfied = playingSatisfied < 0 ? 0 : playingSatisfied;
        movmentSatisfied = movmentSatisfied < 0 ? 0 : movmentSatisfied;

        // Neue History anlegen.
        history.Add(new PolyPetStatsHistory(hungerSatisfied, thirstSatisfied, playingSatisfied, movmentSatisfied));
    }

    public string Name { get { return name; } }
    public int HungerSatisfied { get { return hungerSatisfied; } }
    public int ThirstSatisfied { get { return thirstSatisfied; } }
    public int PlayingSatisfied { get { return playingSatisfied; } }
    public int MovmentSatisfied { get { return movmentSatisfied; } }
    public List<PolyPetStatsHistory> History { get { return history; } }

    public void resetMovment()
    {
        movmentSatisfied = 1100;
    }

    public void resetThirst()
    {
        thirstSatisfied = 1100;
    }

    public void resetHunger()
    {
        hungerSatisfied = 1100;
    }

    public void resetPlaying()
    {
        playingSatisfied = 1100;
    }

    
}