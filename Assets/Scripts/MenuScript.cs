
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
public class MenuScript : MonoBehaviour
{
    [SerializeField] private Slider sl;
    [SerializeField]private Toggle tg;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject backPanel,loadingPanel;
    [SerializeField]Image loading;
    private void Start()
    {
        if (PlayerPrefs.HasKey("sound"))
            sl.value = PlayerPrefs.GetFloat("sound");
        else
            sl.value = 1;
        if (PlayerPrefs.HasKey("fullscreen"))
            tg.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        else
            tg.isOn = true;
    }
    public void StartGame()
    {
        backPanel.SetActive(false);
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingProc());
        IEnumerator LoadingProc()
        {
            AsyncOperation sync = SceneManager.LoadSceneAsync(1);
            while (!sync.isDone)
            {
                loading.fillAmount -= sync.progress / .9f;
                yield return null;
            }
        }
      
    }
    public void Option()
    {
        optionPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void FullScreen()
    {

        if (tg.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            PlayerPrefs.SetInt("fullscreen",1);
        }

        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            PlayerPrefs.SetInt("fullscreen", 0);
        }

    }
    public void SoundValue()
    {
        PlayerPrefs.SetFloat("sound", 100*sl.value);
      
    }
    public void Back()
    {
       
        optionPanel.SetActive(false);
    }

       
}
