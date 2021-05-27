
using System.Collections.Generic;
using UnityEngine;



public class CubeSpawner : MonoBehaviour
{
    //Singleton class
    public static CubeSpawner Instance;

    Queue<CUbeId> CubesQeueu = new Queue<CUbeId>();
    [SerializeField] private int cubeQueueCapacity = 20;
    [SerializeField] private bool autoQueue = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;


    [HideInInspector] public int maxCubeNumber;
    //in our case its 4096 (2^12)

    private int MaxPower = 12;

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, MaxPower);

        InitializeCubesQueue();
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubeQueueCapacity; i++)
            AddCubeToQueue();
    }

    private void AddCubeToQueue()
    {
        CUbeId cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
                      .GetComponent<CUbeId>();

        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        CubesQeueu.Enqueue(cube);

    }

    public CUbeId Spawn(int number, Vector3 position)
    {
           if(CubesQeueu.Count == 0)
        {
            if (autoQueue)
            {
                cubeQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("[Cubes Queue]: no more cubes available in the pool");
                return null;
            }
        }
        CUbeId cUbeId = CubesQeueu.Dequeue();
        cUbeId.transform.position = position;
        cUbeId.SetCubeNumber(number);
        cUbeId.SetColor(GetColor(number));
        cUbeId.gameObject.SetActive(true);

        return cUbeId;


    }

    public CUbeId SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(),defaultSpawnPosition);

    }

public void DestroyCube(CUbeId cUbe)
    {
        cUbe.CubeRigidBody.velocity = Vector3.zero;
        cUbe.CubeRigidBody.angularVelocity = Vector3.zero;
        cUbe.transform.rotation = Quaternion.identity;
        cUbe.IsMainCube = false;
        cUbe.gameObject.SetActive(false);
        CubesQeueu.Enqueue(cUbe);
    }

    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    
    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}
