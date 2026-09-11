using UnityEngine;
using ClearSky;

[CreateAssetMenu]
public class Slowness : CrowdDebuff
{
    public float amount = 0.5f;

    public override void Apply(GameObject target)
    {
        DemoCollegeStudentController player =
            target.GetComponent<DemoCollegeStudentController>();

        if (player != null)
        {
            player.movePower = amount;
        }
    }
}