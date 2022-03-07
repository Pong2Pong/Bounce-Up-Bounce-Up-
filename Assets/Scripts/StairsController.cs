using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject stairPrefab;
    private int numOfDeletedSteps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStair()
    {
        GameObject stair = Instantiate(stairPrefab, transform.GetChild(transform.childCount-1).position+ new Vector3(1,1,0), Quaternion.identity);
        stair.transform.SetParent(transform);
        DeleteStair();
    }
    public void DeleteStair()
    {
        Destroy(transform.GetChild(0).gameObject);
        numOfDeletedSteps++;
    }
}
