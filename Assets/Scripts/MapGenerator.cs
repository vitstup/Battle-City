using UnityEngine;
using Zenject;

public class MapGenerator : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private Vector2Int mapSize;

    [SerializeField] private Transform map;

    [SerializeField] private GameObject floorPrefab;

    [Header("Undestroyable walls")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private int wallsAmount;

    [Header("Destroyable boxes")]
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private int boxesAmout;

    [SerializeField] private DynamicObject[] dynamicObjects;

    private bool[,] tileUsage;

    [Inject] private DiContainer container;

    [System.Serializable]
    private class DynamicObject
    {
        [field: SerializeField] public GameObject obj { get; private set; }
        [field: SerializeField] public int amount { get; private set; }
    }

    private void Start()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        GenerateFloor();
        GenerateWall();
        CreateTileUsage();
        GenerateBoxes();
        GenerateWallesInMap();
        GenerateDymanicObjects();
    }

    private void GenerateFloor()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var obj = Instantiate(floorPrefab, map);
                obj.transform.localPosition = new Vector3(x, y, 0);
            }
        }
        map.transform.position = new Vector2(mapSize.x / -2 + 0.5f, mapSize.y / -2 + 0.5f);
    }

    private void GenerateWall()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            var obj = Instantiate(wallPrefab, map);
            var obj2 = Instantiate(wallPrefab, map);
            obj.transform.localPosition = new Vector3(x, mapSize.y, 0);
            obj2.transform.localPosition = new Vector3(x, -1, 0);
        }
        for (int y = 0; y < mapSize.y; y++)
        {
            var obj = Instantiate(wallPrefab, map);
            var obj2 = Instantiate(wallPrefab, map);
            obj.transform.localPosition = new Vector3(mapSize.x, y, 0);
            obj2.transform.localPosition = new Vector3(-1, y, 0);
        }
        Instantiate(wallPrefab, map).transform.localPosition = new Vector3(-1, -1);
        Instantiate(wallPrefab, map).transform.localPosition = new Vector3(-1, mapSize.y);
        Instantiate(wallPrefab, map).transform.localPosition = new Vector3(mapSize.x, -1);
        Instantiate(wallPrefab, map).transform.localPosition = new Vector3(mapSize.x, mapSize.y);
    }

    private void CreateTileUsage()
    {
        tileUsage = new bool[mapSize.x, mapSize.y];
    }
    
    private void GenerateBoxes()
    {
        GenerateThings(boxPrefab, boxesAmout);
    }

    private void GenerateWallesInMap()
    {
        GenerateThings(wallPrefab, wallsAmount);
    }

    private void GenerateDymanicObjects()
    {
        for (int i = 0; i < dynamicObjects.Length; i++)
        {
            GenerateThings(dynamicObjects[i].obj, dynamicObjects[i].amount);
        }
    }
    
    private void GenerateThings(GameObject prefab, int amout)
    {
        int attemps = 0;
        for (int i = 0; i < amout; i++)
        {
            while (true)
            {
                int x = Random.Range(0, mapSize.x);
                int y = Random.Range(0, mapSize.y);

                if (!tileUsage[x, y])
                {
                    attemps = 0;

                    var obj = container.InstantiatePrefab(prefab, map);
                    obj.transform.localPosition = new Vector2(x, y);
                    tileUsage[x, y] = true;
                    break;
                }

                attemps++;

                if (attemps >= 150)
                {
                    break;
                    throw new System.Exception("Too much attems to generate, maybe you're trying to generate too many objects for such size map");
                }
            }
        }
    }
}