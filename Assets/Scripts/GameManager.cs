
using UnityEngine;
using UnityEngine.SceneManagement;

//***************************************Oyunun Mekaniklerini Yöneten Script************************************//
public class GameManager : MonoBehaviour
{
    //----------------------------Referanslar-----------------------------//
    Camera cam;
    GameObject buildObject;
    Ray ray;
    RaycastHit hit;
    [SerializeField] AudioSource lootSound,buildSound,citySound;
    public PresetScript selectObjScript;
    public BuildPreset[] preset;
    public static GameManager instance;
    //----------------------------Deðiþkenler---------------------------//
    public int resource, water, energy, coin;
    public bool isBuying;
    public bool isClicked;
    public int index;
    public bool isCreated;
    float lastTime,time;
    
    //------------------------------KOD----------------------------------//
    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        ray = cam.ScreenPointToRay(Input.mousePosition);//Kameranýn ortasýndan farenin pozisoynuna ýþýn gönder.
        UýManager.OnbuttonClicked += OnbuttonClicked;//Uý manager olayýna abone ol
        lootSound.volume = buildSound.volume =citySound.volume= PlayerPrefs.GetFloat("sound");
        lootSound.enabled = buildSound.enabled = citySound.enabled = false;
    }
    /// <summary>
    /// UýManager nesnesinden tetiklenen olayla çalýþýr.
    /// </summary>
    /// <param name="index">Presetteki indexe  denk gelen deger</param>
    private void OnbuttonClicked(int index)// 
    {
        this.index = index;
        if (isBuying && buildObject == null)
        {
            this.buildObject =preset[index].CreateObject(hit.point, transform.rotation);
            this.buildObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }

    }
    private void Update()
    {
        CreateObject();
        ResourceLoop();
        GameOver();

    }
    /// <summary>
    /// Obje Oluþturma Ýþlemleri
    /// </summary>
    void CreateObject()//
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            if (buildObject != null)//Objeyi oluþturmak istediðin yere sürükle
            {

                buildObject.transform.position = hit.point;
                //Objei döndürme iþlemi
                if (Input.GetKeyDown(KeyCode.E))
                    buildObject.transform.Rotate(new Vector3(0,45,0));
                else if(Input.GetKeyDown(KeyCode.Q)) 
                    buildObject.transform.Rotate(new Vector3(0, -45, 0));
                
            }
        }
        if (Input.GetMouseButtonDown(0))//Objeyi oluþtur
        {
            if (buildObject != null&&!isCreated)
            {
                buildObject.transform.position = hit.point;
                buildObject.GetComponent<MeshRenderer>().material.color = Color.white;
                buildObject.GetComponent<BoxCollider>().isTrigger = false;
                buildObject = null;
                BuyProcess(index);
                isClicked = true;
                buildSound.Play();
                

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))//Oluþturmaktan vazgeç
        {
            Destroy(buildObject);
        }
    }
    /// <summary>
    /// Satýn alma iþlemi
    /// </summary>
    /// <param name="index"></param>
    public void BuyProcess(int index)//
    {
        resource -= preset[index].resource;//kaynak durumunu güncelle
        coin -= preset[index].sales;//para durumunu güncelle
    }
    /// <summary>
    /// Seçili objenin gerekli kaynaklarý kullanarak ilgili objenin üretimini baþlatýr
    /// </summary>
    public void GenerateUse()
    {
        if (selectObjScript.preset.useRes<=resource&&selectObjScript.preset.water<=water&&selectObjScript.preset.energy<=energy)
        {
            if(!selectObjScript.isStartAwards)
            {
                resource -= selectObjScript.preset.useRes;
                water -= selectObjScript.preset.water;
                energy -= selectObjScript.preset.energy;
                selectObjScript.AwardsStart();
            }
        }
    }
    public void GenerateAward(int resAward,int waterAward,int energyAward,int earningAward)
    {
        resource += resAward;
        water += waterAward;
        energy += energyAward;
        coin += earningAward;
        lootSound.Play();
    }
    /// <summary>
    /// Belli bir sürede sürekli kaynaklarý harcar veya ekler
    /// </summary>
    private void ResourceLoop()
    {

        lastTime += Time.deltaTime;
        time = 50;
        if (lastTime > time)
        {
            water -= 50;
            energy -= 50;
            lastTime = 0;
         
        }
    }
    private void GameOver()
    {
        if (energy <= 0 && water <= 0)
        {
            //Gameover panaeli açýlýr bellibir süre sonra oyun menüye döner
            SceneManager.LoadScene(0);
        }
    }
    public void SoundActivate()
    {
        lootSound.enabled = buildSound.enabled = citySound.enabled = true;
    }

    private void OnDisable()
    {
        UýManager.OnbuttonClicked -= OnbuttonClicked;
    }


}
