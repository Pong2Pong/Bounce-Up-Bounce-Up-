using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class EnemyCubeController : MonoBehaviour
{
    [SerializeField] AnimationCurve forwardJump;
    private Vector3 jumpPos;
    private float totalTimeForward;
    private float currentTime;
 
    void Start()
    {
        totalTimeForward = forwardJump.keys[forwardJump.keys.Length - 1].time;
        jumpPos = transform.position;
    }
 
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward,90*Time.deltaTime/totalTimeForward);
        transform.position=jumpPos + new Vector3 (-currentTime/totalTimeForward,forwardJump.Evaluate(currentTime),0);
        currentTime += Time.deltaTime;
        if(currentTime >= totalTimeForward)
            {
                transform.position= jumpPos + new Vector3 (-1,-1,0);
                currentTime = 0;
                jumpPos = transform.position;
            }
    }
}