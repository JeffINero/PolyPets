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
    private int selectedItem = 0;
    private double foodAmount = 0;
    private int cost = 0;
    private DateTime costDate = DateTime.Now;

    private bool tutorialOpen = false;

    public Button menuButton, walkButton, feedButton, playButton, doctorButton, shopButton, reportButton, leaveButton;
    public Text healthyText, walkText, reportResultText, costText, amountInInventoryText;
    public GameObject shopPanel, reportPanel, playPanel, tutorialPanel, feedPanel, alertBox, feedAlertBox, feedFailAlertBox;

    public Button tutMenuButton, tutWalkButton, tutFeedButton, tutPlayButton, tutDoctorButton, tutShopButton, tutReportButton;
    public Text tutorialText, reportText, menuText, menuExplainedText;
    public Image menuArrow, reportArrow;

    public Slider feedingSlider, movementSlider, attentionSlider;
    public Image feedingFill, movementFill, attentionFill;

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

        // Überprüfe ob weiter das Tutorial offen ist.
        if (Input.touches.Length > 0 && tutorialOpen)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {

                // Überprüfe ob das nächste tutorial angezeigt werden soll oder ob es geschlossen werden soll.
                if (tutorialText.text == "Tutorial 1/3") {
                    this.openTutTwo();
                }
                else if (tutorialText.text == "Tutorial 2/3")
                {
                    this.openTutThree();
                }
                else
                {
                    tutorialOpen = false;

                    tutorialPanel.gameObject.SetActive(false);
                }
            }
        }
    }

    /**
     * Wird aufgerufen wenn die App geöffnet / fortgesetzt wird.
     */
    void OnApplicationFocus(bool hasFocus)
    {
        string name = "PolyPet";
        int hungerSatisfied = 1100;
        int thirstSatisfied = 1100;
        int playingSatisfied = 1100;
        int movmentSatisfied = 1100;
        DateTime lastUpdate = DateTime.Now;
        DateTime costDate = DateTime.Now;

        // Lade das PolyPet von der Datei.
        if (File.Exists(Application.persistentDataPath + "/myPet.txt"))
        {
            string[] test = File.ReadAllLines(Application.persistentDataPath + "/myPet.txt");

            try
            {
                if (test.Length > 8)
                {
                    name = test[0];
                    hungerSatisfied = Convert.ToInt32(test[1]);
                    thirstSatisfied = Convert.ToInt32(test[2]);
                    playingSatisfied = Convert.ToInt32(test[3]);
                    movmentSatisfied = Convert.ToInt32(test[4]);
                    lastUpdate = DateTime.ParseExact(test[5], "O", CultureInfo.InvariantCulture);
                    foodAmount = Convert.ToInt32(test[6]);
                    cost = Convert.ToInt32(test[7]);
                    costDate = DateTime.ParseExact(test[8], "O", CultureInfo.InvariantCulture);

                    // Schau ob es ein neuer Monat geworden ist.
                    if (costDate.Month != this.costDate.Month && costDate.Year == this.costDate.Year)
                    {
                        cost = 0;
                    }
                    else
                    {
                        this.costDate = costDate;
                    }
                }
            }
            catch (Exception e)
            {
                // Converting Failed
            }
        }
        else
        {
            openTutorial();
        }

        // Erstelle dein PolyPet.
        polyPet = new PolyPet(name, hungerSatisfied, thirstSatisfied, playingSatisfied, movmentSatisfied, lastUpdate);

        // Erstelle den Timer (Alle 15 Minuten ausführen).
        stateTimer = new Timer(polyPet.updateStats, null, 0, 90000);
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
                DateTime.Now.ToString("O"),
                ""+foodAmount,
                ""+cost,
                costDate.ToString("O"),
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
        alertBox.SetActive(false);
        feedAlertBox.SetActive(false);
        feedFailAlertBox.SetActive(false);

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
     * Öffnet das Tutorial.
     */
    public void openTutorial()
    {
        cleanAll();

        tutorialPanel.gameObject.SetActive(true);

        this.openTutOne();

        tutorialOpen = true;
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

        // Bewegungtracken beginnen
        LocationService.getInstance().begin();

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
        amountInInventoryText.text = "Total dog Food (in kg): " + foodAmount / 1000;
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
        feedAlertBox.SetActive(true);
        leaveButton.gameObject.SetActive(true);
    }

    public void checkFood()
    {
        if (foodAmount >= 300)
        {
            foodAmount -= 300;
            polyPet.resetHunger();
            openFeedScreen();
        }
        else
        {
            feedAlertBox.SetActive(false);
            feedFailAlertBox.SetActive(true);
        }
    }

    public void openFeedScreen()
    {
        cleanAll();
        reportButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        feedPanel.SetActive(true);
        leaveButton.gameObject.SetActive(true);

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
            reportResultText.color = Color.white;
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

        string costString = ""+cost;
        // costText.text = costString.Substring(0, costString.Length - 2) + "," + costString.Substring(costString.Length - 2, costString.Length) + "€";
        costText.text = cost / 100 + "," + (cost >= 100 ? costString.Substring(costString.Length - 2, 2) : costString) + "€";

        feedingSlider.value = polyPet.HungerSatisfied + polyPet.ThirstSatisfied /2 / 100;
        attentionSlider.value = polyPet.PlayingSatisfied / 100;
        movementSlider.value = polyPet.MovmentSatisfied / 100;

        if (feedingSlider.value>6)
        {
            var tempColor = Color.green;
            tempColor.a = 0.42f;
            feedingFill.color = tempColor;
        }
        else if (feedingSlider.value > 3)
        {
            var tempColor = Color.yellow;
            tempColor.a = 0.42f;
            feedingFill.color = tempColor;
        }
        else
        {
            var tempColor = Color.red;
            tempColor.a = 0.42f;
            feedingFill.color = tempColor;
        }


        if (attentionSlider.value > 6)
        {
            var tempColor = Color.green;
            tempColor.a = 0.42f;
            attentionFill.color = tempColor;
        }
        else if (attentionSlider.value > 3)
        {
            var tempColor = Color.yellow;
            tempColor.a = 0.42f;
            attentionFill.color = tempColor;
        }
        else
        {
            var tempColor = Color.red;
            tempColor.a = 0.42f;
            attentionFill.color = tempColor;
        }




        if (movementSlider.value > 6)
        {
            var tempColor = Color.green;
            tempColor.a = 0.42f;
            movementFill.color = tempColor;
        }
        else if (movementSlider.value > 3)
        {
            var tempColor = Color.yellow;
            tempColor.a = 0.42f;
            movementFill.color = tempColor;
        }
        else
        {
            var tempColor = Color.red;
            tempColor.a = 0.42f;
            movementFill.color = tempColor;
        }

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

    public void buyItem(int i)
    {
        selectedItem = i;
        alertBox.SetActive(true);
    }

    public void acceptItem()
    {
        switch (selectedItem)
        {
            case 1:
                foodAmount+=3000;
                cost += 799;
                break;
            case 2:
                foodAmount+=15000;
                cost += 2699;
                break;
            case 3:
                foodAmount += 30000;
                cost += 4999;
                break;
        }
        alertBox.SetActive(false);
        amountInInventoryText.text = "Total dog Food (in kg): " + foodAmount/1000 + "kg";
    }

    public void closeAlert()
    {
        alertBox.SetActive(false);
    }

    /**
     * Öffnet das erste Tutorial Screen.
     */
    private void openTutOne()
    {
        tutorialText.text = "Tutorial 1/3";

        tutDoctorButton.gameObject.SetActive(false);
        tutFeedButton.gameObject.SetActive(false);
        tutPlayButton.gameObject.SetActive(false);
        tutShopButton.gameObject.SetActive(false);
        tutWalkButton.gameObject.SetActive(false);
        menuExplainedText.gameObject.SetActive(false);
        tutMenuButton.gameObject.SetActive(false);
        menuArrow.gameObject.SetActive(false);
        menuText.gameObject.SetActive(false);

        tutReportButton.gameObject.SetActive(true);
        reportArrow.gameObject.SetActive(true);
        reportText.gameObject.SetActive(true);
    }

    /**
     * Öffnet das zweite Tutorial Screen.
     */
    private void openTutTwo()
    {
        tutorialText.text = "Tutorial 2/3";

        tutReportButton.gameObject.SetActive(false);
        reportArrow.gameObject.SetActive(false);
        reportText.gameObject.SetActive(false);

        tutMenuButton.gameObject.SetActive(true);
        menuArrow.gameObject.SetActive(true);
        menuText.gameObject.SetActive(true);
    }

    /**
     * Öffnet das dritte Tutorial Screen.
     */
    private void openTutThree()
    {
        tutorialText.text = "Tutorial 3/3";

        tutMenuButton.gameObject.SetActive(false);
        menuArrow.gameObject.SetActive(false);
        menuText.gameObject.SetActive(false);

        tutDoctorButton.gameObject.SetActive(true);
        tutFeedButton.gameObject.SetActive(true);
        tutPlayButton.gameObject.SetActive(true);
        tutShopButton.gameObject.SetActive(true);
        tutWalkButton.gameObject.SetActive(true);
        menuExplainedText.gameObject.SetActive(true);
    }
}
