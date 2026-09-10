using UnityEngine;
using ClearSky;

public class Debuff : MonoBehaviour
{
    public CrowdDebuff crowdDebuff;

    private float originalSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DemoCollegeStudentController player =
            collision.GetComponent<DemoCollegeStudentController>();

        if (player != null)
        {
            originalSpeed = player.movePower;
            crowdDebuff.Apply(player.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DemoCollegeStudentController player =
            collision.GetComponent<DemoCollegeStudentController>();

        if (player != null)
        {
            player.movePower = originalSpeed;
        }
    }
}