using System.Collections;
using UnityEngine;


public class PlatformType : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public int Health;
    public int number;

    private void OnEnable()
    {
        number = Random.Range(0, 5);

        if (number == 0)
        {
            SpriteRenderer.color = new Color32(180, 222, 255, 255);
            Health = 1;
            tag = "IceBlock";
        }
        else
        {
            SpriteRenderer.color = new Color32(247, 147, 30, 255);
            Health = 5;
            tag = "Block";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (BallController.BIsBoosting)
            {
                Health -= 5;
            }
            else
            {
                Health -= 1;
            }

            if (Health <= 0)
            {
                switch (tag)
                {
                    case "Block":
                        StartCoroutine(respawnBlock(1.8f));
                        break;
                    case "IceBlock":
                        StartCoroutine(respawnBlock(1.8f));
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private IEnumerator respawnBlock(float time)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(time);

        Health = 1;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}
