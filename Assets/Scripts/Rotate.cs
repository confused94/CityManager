
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 rotate;
    void Update()
    {
        transform.Rotate(rotate*Time.deltaTime);
    
    }
}
