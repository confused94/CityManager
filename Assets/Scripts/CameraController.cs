
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //-----------------------------Referans------------------------------//
    private Camera cam;
    //---------------------------Degiþken------------------------------//
    private float rotateSpeed,moveSpeed,zoomSpeed;
    private float zoomValue,zoomMin, zoomMax;
    private Vector3 rotate,movement;
    //----------------------------Kod-----------------------------------//
    private void Start()
    {
        rotateSpeed = 50;
        moveSpeed= 50;
        zoomSpeed= 50;
        zoomMin = -90;
        zoomMax = -2;
        zoomValue = 0;
        cam=Camera.main;
        
    }
    private void Update()
    {
        CameraMove();
    }
    private void CameraMove()//Kameranýn hareket iþlemleri
    {
        //deðiþkenlerin inputlarýný al
        float mouseX = Input.GetAxis("Mouse X");
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //inputlar ile hareket et
        rotate = new Vector3(0, mouseX, 0);
        zoomValue += mouseWheel * zoomSpeed;
        zoomValue = Mathf.Clamp(zoomValue, zoomMin,zoomMax);
        cam.transform.localPosition = Vector3.forward*zoomValue;//Farenin orta tekeri ile yakýnlaþ uzaklaþ
        movement = cam.transform.forward * vertical + transform.right * horizontal;//klavyeden yön tuþlarý ile hareket et
        movement.y = 0;
        if (Input.GetMouseButton(1))//Kamerayý döndür
        {
            transform.Rotate(rotate * rotateSpeed * Time.deltaTime,Space.World);
        }
        transform.position += movement*moveSpeed * Time.deltaTime;
    }
}
