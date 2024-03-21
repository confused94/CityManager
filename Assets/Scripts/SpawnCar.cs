using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***************************************************Araçlarý Spawnlayan Script*****************************/
public class SpawnCar : MonoBehaviour
{
    //-----------------------------Referans-------------------------------//
    [SerializeField] GameObject[] cars;
    [SerializeField] Transform[] spawnPoint,roadPoints;
    //-----------------------------Liste----------------------------------//
    [SerializeField] List<GameObject> list=new List<GameObject>();
    private void Start()
    {
        StartCoroutine(SpawnCor());
    }
    /// <summary>
    /// Her 10 saniyede obje spawn et
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnCor()
    {

        int sayi = 0;
        while (sayi<5)
        {
            int randomTime = Random.Range(3, 20);
            CarChange();
            yield return new WaitForSeconds(randomTime);
            Spawn();
            sayi++;
        }
    }
    /// <summary>
    /// Cars dizisindeki araçlarý rastgele deðiþtir
    /// </summary>
    void CarChange()
    {
        cars[0] = list[Random.Range(0, list.Count - 1)];
        cars[1] = list[Random.Range(0, list.Count - 1)];
        cars[2] = list[Random.Range(0, list.Count - 1)];
        cars[3] = list[Random.Range(0, list.Count - 1)];

    }
    /// <summary>
    /// Araç spawn et
    /// </summary>
    private void Spawn()
    {
        for(int i=0;i<cars.Length; i++)
        {
            CarAI carScript=Instantiate(cars[i], spawnPoint[i].position, Quaternion.identity).GetComponent<CarAI>();
            carScript.gameObject.transform.forward = spawnPoint[i].forward;//Oluþan noktada objeyyi düz bakmasýný saðla
            carScript.roadPoint = roadPoints[i];//Script içerisidenki roadPoint noktasýna buradan referans ver.
        }
    }
}