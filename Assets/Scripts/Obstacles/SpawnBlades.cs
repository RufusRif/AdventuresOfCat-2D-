
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnBlades : MonoBehaviour
//{
//    [SerializeField] private GameObject[] spawnPoints;
//    [SerializeField] private GameObject blade; //(объявление переменной) Создается ссылка на объект типа GameObject. в стэке теперь  blade=null.
//    [SerializeField] private GameObject stoneFace;
//    [SerializeField] private float minSpawnInterval = 1.0f;
//    [SerializeField] protected float maxSpawnInterval = 4.0f;
//    [SerializeField] private float currentSpawnInterval;

//    void Start()
//    {
//        SpawnBladeOrFace();
//    }
//    private void SpawnBladeOrFace()
//    {
//        if (spawnPoints.Length == 0 || blade == null || stoneFace == null)
//        {
//            Debug.Log("Нет назначенных точек спавна или лезвия!");
//            return;
//        }
//        int index = Random.Range(0, spawnPoints.Length);

//        GameObject objectToSpawn = Random.value < 0.5f ? blade : stoneFace;

//        GameObject spawnedObject = Instantiate(objectToSpawn);

//        spawnedObject.transform.position = spawnPoints[index].transform.position;

//        Vector3 positionOfPointSpawn = spawnPoints[index].transform.position;

       

//        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
//        Invoke("SpawnBladeOrFace", currentSpawnInterval);
//    }
//}
///////////////////////////////////////
/////public class SpawnBlades : MonoBehaviour
////{
////    [SerializeField] private GameObject[] spawnPoints;
////    [SerializeField] private GameObject blade; //(объявление переменной) Создается ссылка на объект типа GameObject. в стэке теперь  blade=null.

////    [SerializeField] private float minSpawnInterval = 1.0f;
////    [SerializeField] protected float maxSpawnInterval = 4.0f;
////    [SerializeField] private float currentSpawnInterval;

////    void Start()
////    {
////        SpawnBlade();
////    }
////    private void SpawnBlade()
////    {
////        if (spawnPoints.Length == 0 || blade == null)
////        {
////            Debug.Log("Нет назначенных точек спавна или лезвия!");
////            return;
////        }
////        int index = Random.Range(0, spawnPoints.Length);

////        GameObject bl = Instantiate(blade); //Создает новый объект в памяти и возвращает ссылку на него.

////        Vector3 positionOfPointSpawn = spawnPoints[index].transform.position;

////        bl.transform.position = positionOfPointSpawn;

////        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
////        Invoke("SpawnBlade", currentSpawnInterval);
////    }
////}