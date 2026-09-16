using UnityEngine;
using TMPro;

public class endScreenscript : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI ResultSubText;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        EndScreen();
    }

    void EndScreen()
    {
        if (gameManager.playerScore >= gameManager.winScore)
        {
            ResultText.text = "You Win!";
            ResultText.color = Color.green;

            ResultSubText.text = "Je hebt de nodige studiepunten gehaald.";
        }
        else
        {
            ResultText.text = "You Lose!";
            ResultText.color = Color.red;

            ResultSubText.text = "Je hebt niet genoeg studiepunten gehaald.";
        }
    }
}