
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UıManager : MonoBehaviour
{  //----------------------------Referans-----------------------------------//
    [SerializeField]GameObject scrollView;
    [Header("Satın Al Prefab")]
    [Tooltip("Marketteki satın alma butonlarının prefabı")]
    [SerializeField]Image buyButtonImg;
    [Space(20)]
    [Header("Kaynak Textleri")]
    [SerializeField]TextMeshProUGUI resourceTxt;
    [SerializeField] TextMeshProUGUI waterTxt;
    [SerializeField] TextMeshProUGUI energyTxt;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] GameObject infoPanel,helpPanel;
    Button[] button;
    GameManager gameManager;
    //----------------------------Degisken---------------------------------//
    int indexCount;
    //------------------------------Liste----------------------------------//
    List<Image> buyImage=new List<Image>();
    //----------------------------Event----------------------------------//
    public static Action <int> OnbuttonClicked;
    //----------------------------Kod-----------------------------------//
    
    private void Start()
    {
        gameManager = GameManager.instance;
        indexCount = GameManager.instance.preset.Length;
        button = new Button[indexCount];//Obje sayısı kadar buton oluştur
        scrollView.SetActive(false);//Market penceresini gizle
        infoPanel.SetActive(false);//panel penceresini gizle
        SelectObject.onSelect += OnSelectEvent;
        AddButton();
    }
    /// <summary>
    /// Yapı bilgisinin panelini açar/kapatır
    /// </summary>
    private void OnSelectEvent(bool isOpenPanel)
    {
      infoPanel.gameObject.SetActive(isOpenPanel);
    }
    private void Update()
    {
        if(gameManager!=null)
            InfoUpdate();  
    }
    /// <summary>
    /// Indexcount kadar buybuttonimg prefabını oluşturur.Prefab içindeki butonları buton dizisine ekler.
    /// </summary>
    private void AddButton()
    {
        for (int i = 0; i < indexCount; i++)
        {
            int index = i;
            buyImage.Add(Instantiate(buyButtonImg, scrollView.transform.Find("Viewport/Content")));
            buyImage[i].transform.Find("Kaynak/text").GetComponent<TextMeshProUGUI>().text = gameManager.preset[i].resource.ToString();
            buyImage[i].sprite = gameManager.preset[i].sprite;
            button[i] = buyImage[i].transform.GetChild(0).GetComponent<Button>();
            button[i].onClick.AddListener(() => Select(index));
            button[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gameManager.preset[i].sales.ToString();
        }
    }
    /// <summary>
    /// Buton ile market penceresini aktif/deaktif et
    /// </summary>
    public void ActiveShop()
    {
        if (scrollView.gameObject.activeInHierarchy)
            scrollView.SetActive(false);
        else
            scrollView.SetActive(true);
    }
    /// <summary>
    /// Buton tetiklendiğinde ilgili butonun index değerini gönderir
    /// </summary>
    /// <param name="index">GameManager daki diziden index değerini yakalar</param>
    public void Select(int index)
    {
        if (gameManager.preset[index].sales <= gameManager.coin && gameManager.preset[index].resource <= gameManager.resource)
        {
            gameManager.isBuying = true;
            OnbuttonClicked?.Invoke(index);
        }
        else
            gameManager.isBuying = false;
    }
    /// <summary>
    /// Ana kaynak bilgilerini günceller
    /// </summary>
    private void InfoUpdate()
    {
            resourceTxt.text = GameManager.instance.resource.ToString();
            waterTxt.text = gameManager.water.ToString();
            energyTxt.text = gameManager.energy.ToString();
            coinTxt.text = gameManager.coin.ToString();
    }
    /// <summary>
    /// Seçili objenin panelinde yer alan butonun işlevi.
    /// </summary>
    public void GenerateButton()
    {
        GameManager.instance.GenerateUse();
    }
    public void HelpPanel()
    {
        if(helpPanel.activeInHierarchy)
            helpPanel.SetActive(false);
        else
            helpPanel.SetActive(true);
    }
    private void OnDestroy()
    {
        SelectObject.onSelect -= OnSelectEvent;
    }

}
