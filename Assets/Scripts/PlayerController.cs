using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rigidbody;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];

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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocityX, jumpForce);
        }
    }
    private bool IsGrounded()
    {
        int contactCount = rigidbody.GetContacts(contacts);

        for (int i = 0; i < contactCount; i++)
        {

            if (contacts[i].normal.y > 0.5f)
            {
                return true;
            }
        }

        return false;
    }
}