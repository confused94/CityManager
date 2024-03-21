
using System;
using UnityEngine;
using UnityEngine.EventSystems;
    /************************************Objenin Seçim Ýþlemlerini Gerçekleþtirir************************/
public class SelectObject : MonoBehaviour
{
    //-----------------------------Referans-------------------------------------//
    Color colorNormal,colorChange;
    GameObject selectedObject;
    PresetScript preset;
    
    //----------------------------Event-------------------------------------//
    public static Action<bool> onSelect;
    //-----------------------------Kod----------------------------------------//
    private void Start()
    {
        colorNormal = Color.white;
        colorChange = Color.yellow;
    }
    private void Update()
    {
        SelectProcess();
    }
    /// <summary>
    /// Obje seçim iþlemini gerçekleþtirir
    /// </summary>
    private void SelectProcess()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && !GUIControl() && GameManager.instance.isClicked&&!GetComponent<DestructObject>().isDestruct)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Object"))//eðer object tagine sahip ise
                {
                    if (selectedObject != null)//Boþ deðil ise
                    {
                        //seçilen objeyi eski obje deðiþkenine at
                        MeshRenderer oldObject = selectedObject.GetComponent<MeshRenderer>();
                        oldObject.material.color = colorNormal;//Eski, objeyi Eski rengine çevir
                       
                    }
                    selectedObject = hit.collider.gameObject;//Iþýnýn çarptýðý objeyi selectedobject'e at
                    selectedObject.GetComponent<MeshRenderer>().material.color = colorChange;//Renk deðiþ
                    OnSelectedEvent(true);//Metodu yürüt
                    preset = selectedObject.GetComponent<PresetScript>();//Script componentine ulaþ
                    preset.ObjectInfo();//Scripteki metodu yürüt
                    GameManager.instance.selectObjScript = preset;
                }
                else
                {
                    if (selectedObject != null)//Çarpan obje object tagi deðilse
                    {
                        selectedObject.GetComponent<MeshRenderer>().material.color = colorNormal;//objeyi eski rengine çevir
                        OnSelectedEvent(false);//metodu yürüt
                        selectedObject = null;//Deðiþkeni boþalt
                    }
                }
            }
        }
      
    }
    /// <summary>
    /// Farenin uý elemanlarýnýn üzerinde olup olmadýðýnýn kontrolu
    /// </summary>
    /// <returns></returns>
    private bool GUIControl()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position=Input.mousePosition;
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void OnSelectedEvent(bool isOpenPanel)
    {
        onSelect?.Invoke(isOpenPanel);
    }

}
        
    
