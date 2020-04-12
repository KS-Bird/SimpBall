using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public float Speed;
    public float SpeedAlpha;
    public Transform PlayerTransform;
    public float Distance = 0;
    void FixedUpdate()
    {
        Distance = PlayerTransform.position.y - transform.position.y;

        SpeedAlpha = GameManager.instance.Level * 0.0282f;

        if (Distance <= 9.5f)
        {
            Speed = 1.95f + SpeedAlpha;
        }
        else if (Distance <= 25)
        {
            Speed = 2.45f + SpeedAlpha;
        }
        else
        {
            Speed = 3.5f + SpeedAlpha;
        }

        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
}
