using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidbody.linearVelocity = new Vector2(
            Input.GetAxis("Horizontal") * moveSpeed,
            rigidbody.linearVelocityY
        );

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocityX, jumpForce);
        }
    }
}