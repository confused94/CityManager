
using System;
using UnityEngine;
using UnityEngine.EventSystems;
    /************************************Objenin Se�im ��lemlerini Ger�ekle�tirir************************/
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
    /// Obje se�im i�lemini ger�ekle�tirir
    /// </summary>
    private void SelectProcess()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && !GUIControl() && GameManager.instance.isClicked&&!GetComponent<DestructObject>().isDestruct)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Object"))//e�er object tagine sahip ise
                {
                    if (selectedObject != null)//Bo� de�il ise
                    {
                        //se�ilen objeyi eski obje de�i�kenine at
                        MeshRenderer oldObject = selectedObject.GetComponent<MeshRenderer>();
                        oldObject.material.color = colorNormal;//Eski, objeyi Eski rengine �evir
                       
                    }
                    selectedObject = hit.collider.gameObject;//I��n�n �arpt��� objeyi selectedobject'e at
                    selectedObject.GetComponent<MeshRenderer>().material.color = colorChange;//Renk de�i�
                    OnSelectedEvent(true);//Metodu y�r�t
                    preset = selectedObject.GetComponent<PresetScript>();//Script componentine ula�
                    preset.ObjectInfo();//Scripteki metodu y�r�t
                    GameManager.instance.selectObjScript = preset;
                }
                else
                {
                    if (selectedObject != null)//�arpan obje object tagi de�ilse
                    {
                        selectedObject.GetComponent<MeshRenderer>().material.color = colorNormal;//objeyi eski rengine �evir
                        OnSelectedEvent(false);//metodu y�r�t
                        selectedObject = null;//De�i�keni bo�alt
                    }
                }
            }
        }
      
    }
    /// <summary>
    /// Farenin u� elemanlar�n�n �zerinde olup olmad���n�n kontrolu
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
        
    
