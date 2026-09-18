using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ClearSky;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text instructionText;

    public GameObject tutorialPanel;
    public GameObject nextButton;
    public GameObject yesButton;
    public GameObject noButton;
    public ClearSky.DemoCollegeStudentController playerController;
	public GameObject classroomParticles;
	public GameObject tutorialTimer;
	public GameObject scoreText;
	public GameObject circle;
	public GameObject phoneSemesterSurvival;

    private int currentStep = 0;

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

    private void Start()
    {
		classroomParticles.SetActive(false);
		tutorialTimer.SetActive(false);
		scoreText.SetActive(false);
		circle.SetActive(false);
		phoneSemesterSurvival.SetActive(false);
		
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
			//enable particles
			classroomParticles.SetActive(true);

                instructionText.text =
                    "Classrooms zijn herkenbaar aan hun gouden particles. Om erin te gaan, druk op E.\n\n" +
                    "In die classrooms kan je interacten (E) met een docent voor een quizvraag.";

                nextButton.SetActive(true);
                break;

            case 3:
			//disable particles
			classroomParticles.SetActive(false);
			
			//show timer
			tutorialTimer.SetActive(true);
	
			//show score
			scoreText.SetActive(true);
			
                instructionText.text =
                    "Heb je de vraag goed, dan krijg je studiepunten.\n\n" +
                    "Rechts onderin loopt een groene tijdbalk achteruit, laat die niet leeglopen!";

                nextButton.SetActive(true);
                break;

            case 4:
			//hide timer
			tutorialTimer.SetActive(false);
			
			//hide score
			scoreText.SetActive(false);
			
			//show buff
			circle.SetActive(true);
			
			//show debuff
			phoneSemesterSurvival.SetActive(true);
			
			
                instructionText.text =
                    "Je kan ook buffs en debuffs krijgen.\n\n" +
                    "Buffs (energy drankjes) geven je voordelen, terwijl debuffs (afleidingen) je tegenwerken.";

                nextButton.SetActive(true);
                break;

            case 5:
			//hide buff
			circle.SetActive(false);
			
			//hide debuff
			phoneSemesterSurvival.SetActive(false);
			
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
        Debug.Log("FINISHTUTORIAL IS AANGEROEPEN!");

        // Hide the entire tutorial panel
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        // Disable the tutorial buttons
        nextButton.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        // Load Justin's Scene
        Debug.Log("Loading Justin's Scene...");

        SceneManager.LoadScene("Justin's Scene");
    }
}