using System.Collections;
using UnityEngine;
using UnityEngine.AI;
/****************************************************Ara�lar� Y�neten Script***********************************/
public class CarAI : MonoBehaviour
{
    //----------------------------------Referans----------------------------//
    public Transform roadPoint;
    Transform currentWayPOint;
    NavMeshAgent agent;
    //---------------------------------De�i�ken------------------------------//
    int pointValue;
    Vector3 firstPosition;
    Quaternion firstRot;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();//NavmeshComponentini referans al
        currentWayPOint = roadPoint.GetChild(Random.Range(0,roadPoint.childCount));//�ocuk objelerden rastgele bir rota al
        firstPosition=transform.position;//ilk pozisyonu yakala
        firstRot=transform.rotation;
    }

    void Update()
    {
        CarDestination();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("destroycol"))
        {
            transform.position = firstPosition;
            transform.rotation = firstRot;
            pointValue = 0;
        }
    }
    private void CarDestination()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(0, 1f, 0);
        if (Physics.Raycast(transform.position+offset, transform.TransformDirection(Vector3.forward), out hit, 15f))
        {
            Debug.DrawRay(transform.position +offset, transform.TransformDirection(Vector3.forward) * 10f, Color.red);

            //Etiketlere de�er raycast varsa durdur.
            if (hit.collider.CompareTag("car") || hit.collider.CompareTag("trafficlight"))
            {
                agent.isStopped = true;
            }
        }
        else
        {
            //Herhangi bir �eye de�miyorsa ba�lat
            agent.isStopped = false;
        }

        if (!agent.isStopped)
        {
            agent.SetDestination(currentWayPOint.GetChild(pointValue).position);//�lgili noktaya hareket et
        }
        //Ajan�n pozisyonu ile varaca�� nokta aras�ndaki mesafe kontrol�
        if (Vector3.Distance(agent.transform.position, currentWayPOint.GetChild(pointValue).position) < 5f && pointValue < currentWayPOint.childCount - 1)
        {
            //Noktaya vard�ysa pointvalue de�erini artt�r
            pointValue++;
           
        }
    }
}
