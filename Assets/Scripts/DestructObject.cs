
using UnityEngine;

public class DestructObject : MonoBehaviour
{
    //-----------------------------Referans------------------------------//
    Ray ray;
    Camera cam;
    [Header("Mouse Texture")]
    public Texture2D bulldozerTexture;
    public Texture2D normalTexture;
    [SerializeField] AudioSource destructSound;
    //---------------------------Degi�ken------------------------------//
    public bool isDestruct;
    //----------------------------Kod-----------------------------------//
    private void Start()
    {
        cam = Camera.main;
        ray=cam.ScreenPointToRay(Input.mousePosition);
        Cursor.SetCursor(normalTexture, Vector2.zero, CursorMode.Auto);//Fare imlecini de�i�
    
    }
    private void Update()
    {
        Destruct();
        
    }
    public void DestructProcess()//Yok etme butonunu aktif/deaktif et
    {
        isDestruct = !isDestruct;
        if (isDestruct)
            Cursor.SetCursor(bulldozerTexture, Vector2.zero, CursorMode.Auto);//Fare imlecini de�i�
        else
            Cursor.SetCursor(normalTexture,Vector2.zero,CursorMode.Auto);//Fare imlecini de�i�
    }
    private void Destruct()//Yok etme i�lemi
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
        {
            
            if (Input.GetMouseButtonDown(0) && isDestruct)
            {
                if (!GameObject.FindGameObjectWithTag("infopanel"))
                {
                    destructSound.Play();
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
