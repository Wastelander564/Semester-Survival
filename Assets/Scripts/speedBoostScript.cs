
using UnityEngine;
using System.Collections;
using ClearSky;

public class speedBoostScript : MonoBehaviour
{
    public float speedBoost = 1.5f;
    public float boostDuration = 5f;

    public float bobHeight = 0.25f;
    public float bobSpeed = 2f;

    private Vector3 startPosition;

    private void Start()
    {
        // Remember the starting position
        startPosition = transform.position;
    }

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
            // Get the player's movement script
            DemoCollegeStudentController playerMovement =
                other.GetComponent<DemoCollegeStudentController>();

            if (playerMovement != null)
            {
                // Apply the speed boost
                playerMovement.movePower *= speedBoost;

                // Start the reset coroutine on a separate object
                SpeedBoostReset reset =
                    playerMovement.gameObject.AddComponent<SpeedBoostReset>();

                reset.StartReset(
                    playerMovement,
                    speedBoost,
                    boostDuration
                );

                // Destroy the pickup
                Destroy(gameObject);
            }
        }
    }
}

public class SpeedBoostReset : MonoBehaviour
{
    public void StartReset(
        DemoCollegeStudentController playerMovement,
        float speedBoost,
        float boostDuration)
    {
        StartCoroutine(ResetSpeed(
            playerMovement,
            speedBoost,
            boostDuration
        ));
    }

    private IEnumerator ResetSpeed(
        DemoCollegeStudentController playerMovement,
        float speedBoost,
        float boostDuration)
    {
        yield return new WaitForSeconds(boostDuration);

        // Remove the speed boost
        if (playerMovement != null)
        {
            playerMovement.movePower /= speedBoost;
        }

        Destroy(this);
    }
}
