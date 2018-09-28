using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour{

    List<GameObject> foods = new List<GameObject>();
    public Renderer []rend;

    void Start()
    {
        FoodSpawner.OnSpawn += NoticeFood;
        rend = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < rend.Length; i++)
        {
            rend[i].material.shader = Shader.Find("Custom/ThicShader");
        }
    }

    void Update()
    {
        if (foods.Count != 0)
        {
            MoveTowardsTarget();
        }
    }

    void NoticeFood(GameObject food)
    {
        foods.Add(food);
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector3 newDir = new Vector3(GetClosestEnemy().transform.position.x, 0, GetClosestEnemy().transform.position.z);
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0, transform.position.z), newDir, 0.01f);
        Quaternion rotation = Quaternion.LookRotation(newDir - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1f * Time.deltaTime);
        //transform.LookAt(newDir);
    }

    GameObject GetClosestEnemy()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in foods)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "food")
        {
            foods.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material.SetFloat("_Amount", rend[i].material.GetFloat("_Amount") + 0.01f);
            }
        }
    }
}

