using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {
    private static FoodSpawner _instance;

    public static FoodSpawner Instance { get { return _instance; } }

    public delegate void SpawnFood(GameObject gameObject);
    public static event SpawnFood OnSpawn;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    Camera cam;
    [SerializeField]
    GameObject food;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject foodGameobject;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                foodGameobject = Instantiate(food, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
                OnSpawn(foodGameobject);
            }
            //Debug.DrawRay(ray);
        }
    }
}

