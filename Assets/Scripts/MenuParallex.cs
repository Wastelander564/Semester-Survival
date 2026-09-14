using UnityEngine;

public class MenuParallex : MonoBehaviour
{
    public float offsetMultiplier = 40f;

    public float smoothTime = 0.3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        mouse.x = Mathf.Clamp01(mouse.x);
        mouse.y = Mathf.Clamp01(mouse.y);

        Vector2 offset = mouse - new Vector2(0.5f, 0.5f);

        Vector2 targetPosition = startPosition + offset * offsetMultiplier;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}