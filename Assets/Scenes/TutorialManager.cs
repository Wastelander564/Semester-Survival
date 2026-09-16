using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ClearSky;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text instructionText;

    public GameObject tutorialPanel;

    public GameObject nextButton;
    public GameObject yesButton;
    public GameObject noButton;

public ClearSky.DemoCollegeStudentController playerController;
	
	void UnlockPlayer()
{
    if (playerController != null)
    {
        // Enable the controller
        playerController.enabled = true;

        // Unlock movement
        DemoCollegeStudentController controller =
            playerController as DemoCollegeStudentController;

        if (controller != null)
{
    controller.SetTutorialLocked(false);
    Debug.Log("PLAYER UNLOCKED");
}
    }
}

    private int currentStep = 0;

    private void Start()
    {
        // Disable player movement while the tutorial intro is active
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Make sure the tutorial is visible
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }

        ShowStep();
    }

    void ShowStep()
    {
        // Hide all buttons first
        nextButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        switch (currentStep)
        {
            case 0:
                instructionText.text =
                    "Welkom!\n\n" +
                    "Bedankt voor het kiezen van ons spel.";

                nextButton.SetActive(true);
                break;

            case 1:
                instructionText.text =
                    "Wil je de tutorial spelen?";

                yesButton.SetActive(true);
                noButton.SetActive(true);
                break;

            case 2:
                // UNLOCK PLAYER MOVEMENT
                 UnlockPlayer();

                instructionText.text =
                    "Classrooms zijn herkenbaar aan hun speciale uiterlijk.\n\n" +
                    "Loop naar een classroom en ga naar binnen om de opdracht te starten.";

                nextButton.SetActive(true);
                break;

            case 3:
                instructionText.text =
                    "Tijdens het spelen verdien je punten.\n\n" +
                    "Houd ook de timer in de gaten!";

                nextButton.SetActive(true);
                break;

            case 4:
                instructionText.text =
                    "Let goed op buffs en debuffs.\n\n" +
                    "Buffs geven je voordelen, terwijl debuffs je kunnen tegenwerken.";

                nextButton.SetActive(true);
                break;

            case 5:
                instructionText.text =
                    "Veel succes en vooral veel plezier met het spel!";

                nextButton.SetActive(true);
                break;

            case 6:
                FinishTutorial();
                break;
        }
    }

    public void NextButtonClicked()
    {
        currentStep++;
        ShowStep();
    }

    public void YesButtonClicked()
    {
        // Start the actual tutorial
        currentStep = 2;
        ShowStep();
    }

    public void NoButtonClicked()
    {
        // Skip the tutorial completely
        FinishTutorial();
    }

    void FinishTutorial()
    {
        // Hide the entire tutorial panel
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        // Disable the tutorial buttons
        nextButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        // Make sure player movement is enabled
        UnlockPlayer();
    }
}