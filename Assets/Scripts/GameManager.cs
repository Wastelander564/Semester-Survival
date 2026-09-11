using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{
    private int score = 0;
    private int week = 1;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text weekText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if(week = 17 && score >= 100)
        {
            Debug.Log("You Win!");
        }
    }
}
