using UnityEngine;

public class phoneScript : MonoBehaviour
{
    public float bobHeight = 0.25f;
    public float bobSpeed = 2f;
    public int scorePenalty = 25;

    private Vector3 startPosition;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Remember the starting position
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove points from the player's score
            gameManager.UpdateScore(-scorePenalty);
            Destroy(gameObject);
        }
    }
}
