using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Level = 0;

    public float BestMeter = 0;
    public float HighestMeter;
    public float Meter;
    public int BrokenBlockCount = 0;

    public bool IsGameover = false;
    public bool IsPause = false;
    public Text MeterText;

    public GameObject PauseUI;
    public GameObject GameoverUI;

    public AudioSource UIAudio;
    public AudioListener AudioListener;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("두개이상의 게임오브젝트 존재");
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("BestMeter"))
        {
            BestMeter = PlayerPrefs.GetFloat("BestMeter");
        }

        AudioListener = FindObjectOfType<AudioListener>();

        if (PlayerPrefs.HasKey("IsAudioOn"))
        {
            if (PlayerPrefs.GetInt("IsAudioOn") == 0)
            {
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = 1;
            }
        }
    }
     

    void Update()
    {
        if (Meter > HighestMeter)
        {
            HighestMeter = Meter;
        }

        Level = (int)HighestMeter / 100;

        MeterText.text = $"{Meter:f0}m  Level:{Level}";
    }

    public void OnBallDead()
    {
        IsGameover = true;

        if (HighestMeter > BestMeter)
        {
            BestMeter = HighestMeter;
            PlayerPrefs.SetFloat("BestMeter", BestMeter);
        }
        GameoverUI.GetComponentInChildren<Text>().text = $"Record : {HighestMeter:f0}   Best Record : {BestMeter:f0}";
        GameoverUI.SetActive(true);
    }

    public void PauseGame()
    {
        if (IsGameover)
        {
            return;
        }

        UIAudio.Play();

        if (IsPause)
        {
            ContinueGame();
            return;
        }


        IsPause = true;
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void ContinueGame()
    {
        UIAudio.Play();

        IsPause = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        UIAudio.Play();

        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void GoHome()
    {
        UIAudio.Play();

        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }

    public void ExitGame()
    {
        UIAudio.Play();

        Application.Quit();
    }
}
