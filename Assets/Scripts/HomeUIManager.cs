using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour
{
    public GameObject HomeUI;
    public GameObject SettingUI;
    public GameObject RulesUI;
    public AudioSource UIAudio;
    public AudioListener AudioListener;

    public bool IsInSetting = false;
    public bool IsAudioOn = true;
    void Awake()
    {
        if (PlayerPrefs.HasKey("BestMeter"))
        {
            HomeUI.GetComponentInChildren<Text>().text = $"Best Record : {PlayerPrefs.GetFloat("BestMeter"):f0}";
        }

        AudioListener = FindObjectOfType<AudioListener>();

        if (PlayerPrefs.HasKey("IsAudioOn"))
        {
            if (PlayerPrefs.GetInt("IsAudioOn") == 0)
            {
                IsAudioOn = false;
                AudioListener.volume = 0;
            }
            else
            {
                IsAudioOn = true;
                AudioListener.volume = 1;
            }
        }
    }

    public void Setting()
    {
        UIAudio.Play();

        if (!IsInSetting)
        {
            HomeUI.SetActive(false);
            IsInSetting = true;
            SettingUI.SetActive(true);

            if (IsAudioOn)
            {
                GameObject.Find("AudioText").GetComponent<Text>().text = "Audio : On";
            }
            else
            {
                GameObject.Find("AudioText").GetComponent<Text>().text = "Audio : Off";
            }
        }
        else
        {
            HomeUI.SetActive(true);
            IsInSetting = false;
            SettingUI.SetActive(false);
        }
    }
    public void XSetting()
    {
        UIAudio.Play();

        HomeUI.SetActive(true);
        IsInSetting = false;
        SettingUI.SetActive(false);
    }

    public void AudioOnOff()
    {
        UIAudio.Play();

        if (IsAudioOn)
        {
            IsAudioOn = false;
            AudioListener.volume = 0;
            GameObject.Find("AudioText").GetComponent<Text>().text = "Audio : Off";
            PlayerPrefs.SetInt("IsAudioOn", 0);
        }
        else
        {
            IsAudioOn = true;
            AudioListener.volume = 1;
            GameObject.Find("AudioText").GetComponent<Text>().text = "Audio : On";
            PlayerPrefs.SetInt("IsAudioOn", 1);
        }
    }

    public void Rules()
    {
        UIAudio.Play();

        HomeUI.SetActive(false);
        RulesUI.SetActive(true);
    }
    public void XRules()
    {
        UIAudio.Play();

        HomeUI.SetActive(true);
        RulesUI.SetActive(false);
    }

    public void StartGame()
    {
        UIAudio.Play();

        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        UIAudio.Play();

        Application.Quit();
    }
}
