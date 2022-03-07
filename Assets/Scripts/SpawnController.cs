using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject objToSpawn;
    private GameObject stairs;
    private float currentTime;
    private float timeToSpawn;
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        
        stairs = GameObject.Find("Stairs");
        score = GameObject.Find("Player").GetComponent<PlayerController>().score;
        timeToSpawn = Random.Range(50/(score+25),100/(score+25));
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(timeToSpawn<currentTime)
        {
            score = GameObject.Find("Player").GetComponent<PlayerController>().score;
            timeToSpawn = Random.Range(50/(score+25),100/(score+25));
            currentTime = 0;
            if(GameObject.Find("Player").GetComponent<PlayerController>().stable)
            {
                if(Random.Range(0.0f,4.0f) > 3)
                {
                    GameObject Enemy = Instantiate(objToSpawn, stairs.transform.GetChild(stairs.transform.childCount - 3).transform.position + new Vector3 (0,1,Random.Range(-2,2)) ,Quaternion.identity);
                    Enemy.transform.SetParent(gameObject.transform);
                }
            }
            else
            {
                GameObject Enemy = Instantiate(objToSpawn, stairs.transform.GetChild(stairs.transform.childCount - 3).transform.position + new Vector3 (0,1,Random.Range(-2,2)) ,Quaternion.identity);
                Enemy.transform.SetParent(gameObject.transform);
            }
        }
    }
}
