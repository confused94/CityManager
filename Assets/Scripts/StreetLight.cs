
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    [SerializeField] Transform directLight;

    
    void FixedUpdate()
    {
        if (directLight.eulerAngles.x >= 180)
        {
            transform.GetChild(0).gameObject.SetActive(true);
         
        
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
