using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Kontroller um die Button funktionalität zu kontrollieren.
 */
public class ControllButtons : MonoBehaviour {

    public Button menuButton, walkButton, feedButton, playButton, doctorButton, shopButton, reportButton, leaveButton;
    public Text healthyText, walkText;
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

    }
}
