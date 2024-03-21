using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] GameObject mainCam, carCam,marketBtn,destructBtn,market,infopanel;
    CarController carController;
    bool isPlayer, isCar;
    private void Start()
    {
        carController = FindObjectOfType<CarController>();
        isPlayer = true;
        isCar = false;
    }
    public void Switch()
    {
        isPlayer = !isPlayer;
        isCar = !isCar;
        mainCam.SetActive(isPlayer);
        carCam.SetActive(isCar);
        carController.enabled = isCar;
        marketBtn.SetActive(isPlayer);
        destructBtn.SetActive(isPlayer);
        infopanel.SetActive(false);
        market.SetActive(isPlayer);
        GameManager.instance.GetComponent<SelectObject>().enabled = isPlayer;

     
        if (isCar == false)
        {
            carController.SetPos();
            carController.SetKinematic();
            
        }


    }
}
