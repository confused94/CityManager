
using UnityEngine;
using TMPro;

    /************************************************Kar��lama Panelinde Yaz� Yazan Script*********************************************/

public class TextWriter : MonoBehaviour
{  
    //-----------------------------Referanslar----------------------------------------//
    [SerializeField] private TextMeshProUGUI textWriter;
    [SerializeField] private GameObject[] panels;
    
    //-----------------------------De�i�kenler----------------------------------------//
    [TextArea][SerializeField]string text;
    private float lastTime;
    private float timer;
    private int idx;
    void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
       
        timer = .05f;
        Time.timeScale = 0;
    }
    void Update()
    {
        TextWrite();
    }
    public void PanelClose()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(true);
            GameManager.instance.SoundActivate();
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    /// <summary>
    /// YAz�lar�n harf harf yaz�lmas�n� sa�layan metod
    /// </summary>
    private void TextWrite()
    {
        if (lastTime <= 0 && text != null)
        {
            textWriter.text = text.Substring(0, idx);
            idx++;
            lastTime = timer;
          
            if (idx > text.Length)
                text = null; return;
        }
        else
        {
            lastTime -= Time.unscaledDeltaTime;
        }
        switch (idx)
        {
            case 235:panels[2].SetActive(true); break;
            case 275:panels[0].SetActive(true); break;
            case 325:panels[1].SetActive(true); break;
        }    
    }

}
