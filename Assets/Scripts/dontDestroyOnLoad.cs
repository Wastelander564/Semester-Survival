using UnityEngine;

public class dontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
