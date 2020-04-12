using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour
{
    public bool IsDead = false;

    public float BallSpeed = 3.5f;
    public float BallSpeedAlpha;
    public float BoostGauge = 0;
    public static bool BIsBoosting = false;

    public bool BIsCollided = false;

    public Slider BoostGaugeSlider;

    public TrailRenderer DefaultTrail;
    public TrailRenderer BoostTrail;
    public Rigidbody2D BallRigidbody;
    public PlayerInput PlayerInput;

    public AudioSource PlayerAudio;
    public AudioClip Block;
    public AudioClip IceBlock;

    void Start()
    {
        PlayerInput = FindObjectOfType<PlayerInput>();
        BallRigidbody = GetComponent<Rigidbody2D>();
        PlayerAudio = GetComponent<AudioSource>();
        BoostGaugeSlider = FindObjectOfType<Slider>();
    }
    void FixedUpdate()
    {
        if (IsDead)
        {
            return;
        }

        GameManager.instance.Meter = transform.position.y;

        if (PlayerInput.BIsMouseButton && !BIsCollided)
        {
            if (PlayerInput.IsRight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 135f));
            }
        }

        BallSpeedAlpha = GameManager.instance.Level * 0.04f;

        if (BIsBoosting)
        {
            BallSpeed = (3.5f + BallSpeedAlpha) * 1.8f;
            BallRigidbody.velocity = transform.right * BallSpeed;
        }
        else
        {
            BallSpeed = 3.5f + BallSpeedAlpha;
            BallRigidbody.velocity = transform.right * BallSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(OnCollided());

        if (collision.collider.tag == "Block" || collision.collider.tag == "Ball")
        {
            PlayerAudio.clip = Block;
            PlayerAudio.Play();
        }
        else if (collision.collider.tag == "IceBlock")
        {
            if (!BIsBoosting)
            {
                BoostGauge += 1;
                BoostGaugeSlider.value = BoostGauge / 12f;
                if (BoostGauge >= 12)
                {
                    StartCoroutine(StartBoost());
                }
            }

            PlayerAudio.clip = IceBlock;
            PlayerAudio.Play();
        }

        if (BIsBoosting)
        {
            if (collision.collider.tag == "OutLine")
            {
                Reflect(collision);
            }
        }
        else
        {
            Reflect(collision);
        }
    }

    public void Reflect(Collision2D collision)
    {
        Vector2 reflectedUnitVector = Vector2.Reflect(transform.right, collision.contacts[0].normal);
        float angle = Vector2.Angle(Vector2.right, reflectedUnitVector);

        if (reflectedUnitVector.y < 0)
        {
            angle = -angle;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !IsDead)
        {
            IsDead = true;
            GameManager.instance.OnBallDead();
        }
    }

    private IEnumerator OnCollided()
    {
        if (!BIsCollided)
        {
            BIsCollided = true;
            yield return new WaitForSeconds(0.33f);
            BIsCollided = false;
        }
    }
    public IEnumerator StartBoost()
    {
        BIsBoosting = true;

        DefaultTrail.enabled = false;
        BoostTrail.enabled = true;

        for (int i = 0; i < 24; ++i)
        {
            yield return new WaitForSeconds(0.15f);
            BoostGauge -= 0.5f;
            BoostGaugeSlider.value = BoostGauge / 12f;
        }

        BoostTrail.enabled = false;
        DefaultTrail.enabled = true;

        BIsBoosting = false;
    }
}
