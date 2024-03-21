using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    void FixedUpdate()
    {
        
        transform.position =Vector3.Lerp(transform.position,target.position,.1f);
        transform.forward = Vector3.Lerp(transform.forward, target.forward, .3f);

    }
}
