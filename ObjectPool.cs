using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab; //El prefab que vamos a usar
    private List<GameObject> activeObjects = new List<GameObject>();//Objetos
    private Queue<GameObject> objectQueue = new Queue<GameObject>();//de la cola
    private int maxObjects = 9; //Maximo de SQ en pantalla (Mas el que aparece al principio)
    private float respawnTime = 0.7f; // Tiempo de spawn de los SQ
    public float delayTimer = 1f;
    float timer;

    private void Start()
    {
        timer = delayTimer;
        InitializeObjectPool();    //Inicializar variables
        StartCoroutine(RespawnObjects());
    }

    private void InitializeObjectPool() //Llenar la cola con los SQ
    {
        for (int i = 0; i < maxObjects; i++)
        {
            Vector3 SQPos = new Vector3(Random.Range(6.78f, -6.78f), transform.position.y, transform.position.z);
            GameObject obj = Instantiate(objectPrefab, SQPos, transform.rotation);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }
    //Funcion para retirar el mas antiguo y colocarlo de vuelta en la cola
    private IEnumerator RespawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);

            if (activeObjects.Count < maxObjects)
            {
                GameObject obj = objectQueue.Dequeue();
                obj.SetActive(true);   //Colocar y sacar de la cola
                activeObjects.Add(obj);
            }

            if (activeObjects.Count >= maxObjects)
            {
                GameObject oldestObject = activeObjects[0];
                activeObjects.RemoveAt(0);
                oldestObject.SetActive(false);  //Retirar el mas antiguo y poner en la cola
                objectQueue.Enqueue(oldestObject);
            }
        }
    }

// Update
void Update()
    {
    }
}
