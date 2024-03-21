
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


/***************************Yapýlarýn kazandýklarý ve harcadýklarý kaynaklarýn kullanýmýyla ilgili script*************/
public class PresetScript : MonoBehaviour
{
    //--------------------------------Referans--------------------------------------//
    public BuildPreset preset;
    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject Efekt;
    GameObject obj;
    [Tooltip("Yapýlarýn kazandýrdýðý ve harcadýðý kaynaklarýn bilgisini tutan textler")]
    [Header("Yapý Bilgileri")]
    TextMeshProUGUI useResourceTxt;
    TextMeshProUGUI useWaterTxt;
    TextMeshProUGUI useEnergyTxt;
    TextMeshProUGUI awardsResourceTxt;
    TextMeshProUGUI awardsWaterTxt;
    TextMeshProUGUI awardsEnergyTxt;
    TextMeshProUGUI awardsEarningTxt;
    Image selectSprite;
    //--------------------------------Deðiþken------------------------------------//
    bool isUpdate;
    public bool isStartAwards;
    //--------------------------------Kod------------------------------------------//
    private void Start()
    {
        ObjectIdentyPro();
    }
    /// <summary>
    /// Tüm text parametrelerini referans al ve güncelle
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
    /// Canvas prefabýný gameobject türünde tutarak ana prefabýn çocuk objesi olarak oluþturur.
    /// Türüne göre kazançlarý sprite listesinden çekerek butonun spritýný deðiþtirir
    /// </summary>
    private void ObjectIdentyPro()
    {
        //Canvas prefabýný oluþtur
        obj = Instantiate(canvasObject, transform.position, Quaternion.Euler(90, 0, 0));
        obj.transform.position = new Vector3(transform.position.x, GetComponent<Renderer>().bounds.max.y + 2, transform.position.z);

        //ana objenin çocuk objesi yap
        obj.transform.parent = gameObject.transform;

        //butona olay ekle
        obj.GetComponentInChildren<Button>().onClick.AddListener(LootProcess);
        //görünürlüðünü kapat
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
