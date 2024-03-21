using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*************************************************Araba Kontrol Script*************************************/
public class CarController : MonoBehaviour
{//-----------------------------------referans--------------------------//
    [SerializeField] GameObject[] meshes;
    [SerializeField] WheelCollider[] colsF;
    [SerializeField] WheelCollider[] colsR;
    Rigidbody rb;
//-----------------------------------Degiþken--------------------------//

    [SerializeField] float speed,maxSpeed;
    [SerializeField] float maxAngle;
    [SerializeField] float angleSpeed;
    Vector3 firstPos;
    Quaternion firstRot;
    private float inputX, inputZ;
    private bool isBrake;
    private float brake;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        
        rb.centerOfMass= Vector3.zero;
        rb.isKinematic = false;
        firstPos = transform.position;
        firstRot=transform.rotation;

    }
    private void OnEnable()
    {
        if (rb.isKinematic)
            rb.isKinematic = false;
    }
    private void LateUpdate()
    {
        Move();
        Braking();
        TurnCol();
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
    }
/// <summary>
/// Araba fren iþlemi
/// </summary>
    private void Braking()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { isBrake = true;}
        if(Input.GetKeyUp(KeyCode.Space))
        { isBrake = false;}
    }
    /// <summary>
    /// Araba hareket iþlemi
    /// </summary>
    private void Move()
    {
        float speedKMH = rb.velocity.magnitude * 3.6f;
        if (speedKMH <= maxSpeed)
        {
            brake = isBrake ? Mathf.Infinity : 0;
            colsR[0].motorTorque = speed * inputZ*Time.deltaTime* maxSpeed;
            colsR[1].motorTorque= speed * inputZ*Time.deltaTime*maxSpeed;
            colsR[0].brakeTorque = brake;
            colsR[1].brakeTorque = brake;
        }
    }
    /// <summary>
    /// Araba tekerlerini döndürme iþlemi
    /// </summary>
    private void TurnCol()
    {
        Vector3 pos;
        Quaternion rot;
        float _steerAngle = angleSpeed * inputX*maxAngle;
        for (int i=0; i<colsF.Length;i++)
        {
            colsF[i].steerAngle= Mathf.Lerp(colsF[i].steerAngle, _steerAngle, .1f);
            colsF[i].GetWorldPose(out pos, out rot);
            meshes[i].transform.position = pos;
            meshes[i].transform.rotation = rot;
        }
    }
    public void SetPos()
    {
        transform.position = firstPos;
        transform.rotation= firstRot;
    }
    public void SetKinematic()
    {
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("exit"))
        {
            SetPos();
        }
    }

}
