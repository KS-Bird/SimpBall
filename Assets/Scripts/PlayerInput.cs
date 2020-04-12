using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public Camera Camera;
    public BallController Ball;

    public bool IsRight;

    public bool BIsMouseButton;
    public Vector2 MousePosition;
    void Start()
    {
        Camera = FindObjectOfType<Camera>();
        Ball = FindObjectOfType<BallController>();
    }

    void FixedUpdate()
    {
        if (Ball.IsDead)
        {
            return;
        }

        if (BIsMouseButton = Input.GetMouseButton(0))
        {
            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            if (MousePosition.x > 0)
            {
                IsRight = true;
            }
            else
            {
                IsRight = false;
            }
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.instance.PauseGame();
        }
    }
}



/*
public class PlayerInput : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public Camera Camera;
    public BallController Ball;
    public Button BoostButton;

    public bool BIsMouseDraged = false;
    public bool BIsMouseUp = false;
    public bool BNoClick = true;
    public Vector2 FirstMousePosition;
    public Vector2 MousePosition;
    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        Camera = FindObjectOfType<Camera>();
        Ball = FindObjectOfType<BallController>();
        BoostButton = GameObject.Find("BoostButton").GetComponent<Button>();
    }

    void FixedUpdate()
    {
        if (Ball.IsDead)
        {
            return;
        }

        if (!Ball.BIsGrounded)
        {
            BIsMouseUp = false;
            return;
        }

        if (!EventSystem.current.IsPointerOverGameObject() || !BoostButton.interactable)
        {
            BIsMouseDraged = Input.GetMouseButton(0);
        }

        if (BIsMouseDraged)
        {
            BoostButton.interactable = false;

            if (BNoClick)
            {
                BNoClick = false;
                FirstMousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            }

            MousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            LineRenderer.SetPosition(0, FirstMousePosition);
            LineRenderer.SetPosition(1, MousePosition);
        }
        else if (!BNoClick)
        {
            BoostButton.interactable = true;

            BIsMouseUp = true;
            BNoClick = true;

            LineRenderer.SetPosition(0, Vector2.zero);
            LineRenderer.SetPosition(1, Vector2.zero);
        }

    }

    public void OnPressBoost()
    {
        if (Ball.IsDead)
        {
            return;
        }

        StartCoroutine(StartBoost());
    }

    public IEnumerator StartBoost()
    {
        Ball.BIsBoosting = true;
        yield return new WaitForSeconds(Ball.BoostTime);
        Ball.BIsBoosting = false;
    }
}
*/

