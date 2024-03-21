
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


/***************************Yap�lar�n kazand�klar� ve harcad�klar� kaynaklar�n kullan�m�yla ilgili script*************/
public class PresetScript : MonoBehaviour
{
    //--------------------------------Referans--------------------------------------//
    public BuildPreset preset;
    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject Efekt;
    GameObject obj;
    [Tooltip("Yap�lar�n kazand�rd��� ve harcad��� kaynaklar�n bilgisini tutan textler")]
    [Header("Yap� Bilgileri")]
    TextMeshProUGUI useResourceTxt;
    TextMeshProUGUI useWaterTxt;
    TextMeshProUGUI useEnergyTxt;
    TextMeshProUGUI awardsResourceTxt;
    TextMeshProUGUI awardsWaterTxt;
    TextMeshProUGUI awardsEnergyTxt;
    TextMeshProUGUI awardsEarningTxt;
    Image selectSprite;
    //--------------------------------De�i�ken------------------------------------//
    bool isUpdate;
    public bool isStartAwards;
    //--------------------------------Kod------------------------------------------//
    private void Start()
    {
        ObjectIdentyPro();
    }
    /// <summary>
    /// T�m text parametrelerini referans al ve g�ncelle
    /// </summary>
    public void ObjectInfo()
    {

        useResourceTxt = GameObject.FindGameObjectWithTag("useresource").GetComponent<TextMeshProUGUI>();
        useWaterTxt = GameObject.FindGameObjectWithTag("usewater").GetComponent<TextMeshProUGUI>();
        useEnergyTxt = GameObject.FindGameObjectWithTag("useenergy").GetComponent<TextMeshProUGUI>();
        awardsResourceTxt = GameObject.FindGameObjectWithTag("awardsresource").GetComponent<TextMeshProUGUI>();
        awardsWaterTxt = GameObject.FindGameObjectWithTag("awardswater").GetComponent<TextMeshProUGUI>();
        awardsEnergyTxt = GameObject.FindGameObjectWithTag("awardsenergy").GetComponent<TextMeshProUGUI>();
        awardsEarningTxt = GameObject.FindGameObjectWithTag("earning").GetComponent<TextMeshProUGUI>();
        selectSprite = GameObject.FindGameObjectWithTag("objectimage").GetComponent<Image>();
        useResourceTxt.text = preset.useRes.ToString();
        useWaterTxt.text = preset.water.ToString();
        useEnergyTxt.text = preset.energy.ToString();
        awardsResourceTxt.text = preset.awardRes.ToString();
        awardsWaterTxt.text = preset.awardWater.ToString();
        awardsEnergyTxt.text = preset.awardEnergy.ToString();
        awardsEarningTxt.text = preset.earning.ToString();
        selectSprite.sprite = preset.sprite;


    }
    public void AwardsStart()
    {
        if (!isStartAwards)
            StartCoroutine(AwardsCoroutine());
        isStartAwards = true;
    }
    IEnumerator AwardsCoroutine()
    {
        while (true)
        {
            Efekt.SetActive(true);
            yield return new WaitForSeconds(20);
            obj.SetActive(true);
            Efekt.SetActive(false);
            break;
        }
        isStartAwards = false;
    }
    /// <summary>
    /// Canvas prefab�n� gameobject t�r�nde tutarak ana prefab�n �ocuk objesi olarak olu�turur.
    /// T�r�ne g�re kazan�lar� sprite listesinden �ekerek butonun sprit�n� de�i�tirir
    /// </summary>
    private void ObjectIdentyPro()
    {
        //Canvas prefab�n� olu�tur
        obj = Instantiate(canvasObject, transform.position, Quaternion.Euler(90, 0, 0));
        obj.transform.position = new Vector3(transform.position.x, GetComponent<Renderer>().bounds.max.y + 2, transform.position.z);

        //ana objenin �ocuk objesi yap
        obj.transform.parent = gameObject.transform;

        //butona olay ekle
        obj.GetComponentInChildren<Button>().onClick.AddListener(LootProcess);
        //g�r�n�rl���n� kapat
        obj.gameObject.SetActive(false);
        obj.GetComponentInChildren<Button>().image.sprite = preset.awardSprite;
    }
    private void LootProcess()
    {
        preset.ParticleEffectFunc(obj.transform.position, transform.rotation);
        obj.SetActive(false);
        GameManager.instance.GenerateAward
            (
                 preset.awardRes,
                 preset.awardWater,
                 preset.awardEnergy,
                 preset.earning
            );
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("zemin"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            GameManager.instance.isCreated = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            GameManager.instance.isCreated = false;
        
    }


}
