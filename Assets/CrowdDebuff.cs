using UnityEngine;

public abstract class CrowdDebuff : ScriptableObject
{
    public abstract void Apply(GameObject target);
}
