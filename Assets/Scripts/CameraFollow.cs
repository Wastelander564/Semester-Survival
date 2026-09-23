using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;         
    [SerializeField] private float smoothTime = 0.15f; 
    [SerializeField] private float minX = -30f;        
    [SerializeField] private float maxX = 74f;         

    private float velocityX;

    private void Start()
    {
        
        if (target != null)
        {
            float startX = Mathf.Clamp(target.position.x, minX, maxX);
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }

    
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        float targetX = Mathf.Clamp(target.position.x, minX, maxX);
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smoothTime);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}