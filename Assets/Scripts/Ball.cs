using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D BallRigidbody;
    public float BallSpeed = 3.5f;

    void Start()
    {
        BallRigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 15);
    }
    void FixedUpdate()
    {
        BallRigidbody.velocity = transform.right * BallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && BallController.BIsBoosting)
        {
            Destroy(gameObject);
        }

        Vector2 reflectedUnitVector = Vector2.Reflect(transform.right, collision.contacts[0].normal);
        float angle = Vector2.Angle(Vector2.right, reflectedUnitVector);

        if (reflectedUnitVector.y < 0)
        {
            angle = -angle;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
