using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Globalization;

/**
 * Kontroller um die Button funktionalität zu kontrollieren.
 */
public class ControllButtons : MonoBehaviour
{

    private PolyPet polyPet;
    private Timer stateTimer;

    public Button menuButton, walkButton, feedButton, playButton, doctorButton, shopButton, reportButton, leaveButton;
    public Text healthyText, walkText, feedingText, attentionText, movmentText, reportResultText;
    public GameObject shopPanel, reportPanel, playPanel, feedPanel;

    /**
     * Wird aufgerufen bei jedem Frame.
     */
    private void Update()
    {
        // Überprüfen, ob es der Android Back Button ist.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cleanAll();
        }
    }

    /**
     * Wird aufgerufen wenn die App geöffnet / fortgesetzt wird.
     */
    void OnApplicationFocus(bool hasFocus)
    {
        string name = "PolyPet";
        int hungerSatisfied = 1100;
        int thirstSatisfied = 11000;
        int playingSatisfied = 1100;
        int movmentSatisfied = 1100;
        DateTime lastUpdate = DateTime.Now;

        // Lade das PolyPet von der Datei.
        if (File.Exists(Application.persistentDataPath + "/myPet.txt"))
        {
            string[] test = File.ReadAllLines(Application.persistentDataPath + "/myPet.txt");

            try
            {
                if (test.Length > 5)
                {
                    name = test[0];
                    hungerSatisfied = Convert.ToInt32(test[1]);
                    thirstSatisfied = Convert.ToInt32(test[2]);
                    playingSatisfied = Convert.ToInt32(test[3]);
                    movmentSatisfied = Convert.ToInt32(test[4]);
                    lastUpdate = DateTime.ParseExact(test[5], "O", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception e)
            {
                // Converting Failed
            }
        }

        // Erstelle dein PolyPet.
        polyPet = new PolyPet(name, hungerSatisfied, thirstSatisfied, playingSatisfied, movmentSatisfied, lastUpdate);

        // Erstelle den Timer (Alle 10 Minuten ausführen).
        stateTimer = new Timer(polyPet.updateStats, null, 0, 900000);
    }

    /**
     * Wird aufgerufen, wenn die App pausiert wird.
     */
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Speicher dein PolyPet.
            File.WriteAllLines(Application.persistentDataPath + "/myPet.txt", new string[] {
                polyPet.Name,
                ""+polyPet.HungerSatisfied,
                ""+polyPet.ThirstSatisfied,
                ""+polyPet.PlayingSatisfied,
                ""+polyPet.MovmentSatisfied,
                DateTime.Now.ToString("O")
            });

            stateTimer.Dispose();
        }
        else
        {
            OnApplicationFocus(true);
        }
    }

    /**
     * Setzt die UI auf das Standard zurück.
     */
    public void cleanAll()
    {
        walkButton.gameObject.SetActive(false);
        feedButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        doctorButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(false);

        healthyText.gameObject.SetActive(false);
        walkText.gameObject.SetActive(false);

        shopPanel.SetActive(false);
        reportPanel.SetActive(false);
        playPanel.SetActive(false);
        feedPanel.SetActive(false);

        reportButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    /**
     * Zeigt die Menü Buttons an.
     */
    public void openMenu()
    {
        walkButton.gameObject.SetActive(!walkButton.gameObject.active);
        feedButton.gameObject.SetActive(!feedButton.gameObject.active);
        playButton.gameObject.SetActive(!playButton.gameObject.active);
        doctorButton.gameObject.SetActive(!doctorButton.gameObject.active);
        shopButton.gameObject.SetActive(!shopButton.gameObject.active);
    }

    /**
     * Setzt die UI zurück und gibt den Gesundheitszustand des Tiers aus.
     */
    public void openDoctor()
    {
        cleanAll();
        healthyText.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);
        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }

    /**
     * Zeigt an wie viele KM gelaufen wurde.
     */
    public void openWalk()
    {
        cleanAll();
        walkText.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);
        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        polyPet.resetMovment();
    }

    /**
     * Öffnet den Einkaufen Screen.
     */
    public void openShop()
    {
        cleanAll();
        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        shopPanel.SetActive(true);
        leaveButton.gameObject.SetActive(true);
    }

    /**
     * Öffnet den Spielen Screen.
     */
    public void openPlay()
    {
        cleanAll();
        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        playPanel.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        polyPet.resetPlaying();
    }

    /**
     * Öffnet den Füttern Screen.
     */
    public void openFeed()
    {
        cleanAll();
        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        feedPanel.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        polyPet.resetHunger();
        polyPet.resetThirst();
    }

    /**
     * Zeigt den Bericht an.
     */
    public void openReport()
    {
        cleanAll();

        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        reportPanel.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        if ((polyPet.HungerSatisfied / 100) + (polyPet.PlayingSatisfied / 100) + (polyPet.MovmentSatisfied / 100) > 22)
        {
            reportResultText.text = "You are ready to own a Pet";
            reportResultText.color = Color.black;
        }
        else if ((polyPet.HungerSatisfied / 100) + (polyPet.PlayingSatisfied / 100) + (polyPet.MovmentSatisfied / 100) > 11)
        {
            reportResultText.text = "You are not quite ready to own a Pet";
            reportResultText.color = Color.yellow;
        }
        else
        {
            reportResultText.text = "You are not ready to own a Pet";
            reportResultText.color = Color.red;
        }

        feedingText.text = polyPet.HungerSatisfied / 100 + "";
        attentionText.text = polyPet.PlayingSatisfied / 100 + "";
        movmentText.text = polyPet.MovmentSatisfied / 100 + "";

        updateColor(polyPet.HungerSatisfied / 100, feedingText);
        updateColor(polyPet.PlayingSatisfied / 100, attentionText);
        updateColor(polyPet.MovmentSatisfied / 100, movmentText);
    }

    /**
     * Setzt die Farbe nach dem Wert.
     */
    private void updateColor(int value, Text text)
    {
        if (value > 5)
        {
            text.color = Color.black;
            return;
        }
        if (value > 2)
        {
            text.color = Color.yellow;
            return;
        }
        text.color = Color.red;
    }
}
