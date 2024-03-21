
using System.Collections;
using UnityEngine;
/****************************************************Trafik I��klar�n� Kontrol Eden Script*****************************/
public class TrafficLight : MonoBehaviour
{
    //----------------------Referans----------------------------//
    [SerializeField] GameObject[] lights;
    

    private void Start()
    {
        StartCoroutine(LightSwitch());//Coroutine ba�lat
    }
    /// <summary>
    /// Lights dizisinden her 10 saniyede bir child objelerin g�r�n�rl���n� a�/kapa
    /// </summary>
    /// <returns></returns>
    IEnumerator LightSwitch()
    {
        int i = 0;
        while (true)
        {
            
            GameObject lightobj = lights[i];//Dizideki eleman� referans al
            Switch(lightobj);
            yield return new WaitForSeconds(10);
            BeforeSwitch(lightobj);
            i++;
            if (i >= lights.Length)
                i = 0;
        }

    }
    /// <summary>
    /// Belirtilen objelerin g�r�n�m�n� a�/kapa
    /// </summary>
    /// <param name="obj">Coroutineden gelen referans obje</param>
    void Switch(GameObject obj)
    {
         obj.transform.GetChild(0).gameObject.SetActive(false);
         obj.transform.GetChild(1).gameObject.SetActive(false);
         obj.transform.GetChild(2).gameObject.SetActive(true);
    }
    /// <summary>
    /// De�i�tririlen objeyi bir �nceki haline getir
    /// </summary>
    /// <param name="obj"></param>
    private void BeforeSwitch(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.GetChild(1).gameObject.SetActive(true);
        obj.transform.GetChild(2).gameObject.SetActive(false);
    }
}
