using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AnimationCurve forwardJump;
    [SerializeField] AnimationCurve sideJump;
    [SerializeField] private GameObject scoreUI;
    private Rigidbody rb;
    public float score = 0;
    public Vector3 jumpPos;
    public bool stable;
    private bool goingForward = false;
    private bool goingLeft = false;
    private bool goingRight = false;
    private float currentTime;
    private float totalTimeForward,totalTimeSide;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stable = true;
        totalTimeForward = forwardJump.keys[forwardJump.keys.Length - 1].time;
        totalTimeSide = sideJump.keys[sideJump.keys.Length - 1].time;
        jumpPos = transform.position;
        SwipeController.SwipeEvent += InputController;
    }   

    // Update is called once per frame
    void Update()
    {
        if(goingForward)
        {
            transform.position=jumpPos + new Vector3 (currentTime/totalTimeForward,forwardJump.Evaluate(currentTime),0);
            currentTime += Time.deltaTime;
            if(currentTime >= totalTimeForward)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                goingForward = false;
                stable = true;
                transform.position=jumpPos + new Vector3 (1,1,0);
                currentTime = 0;
                score ++ ;
                scoreUI.GetComponent<TMP_Text>().text = "Score: " + score;
            }
        }
        if(goingLeft)
        {
            transform.position=jumpPos + new Vector3 (0,sideJump.Evaluate(currentTime),currentTime/totalTimeSide);
            currentTime += Time.deltaTime;
            if(currentTime >= totalTimeSide)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                goingLeft = false;
                stable = true;
                transform.position=jumpPos + new Vector3 (0,0,1);
                currentTime = 0;
            }
        }
        if(goingRight)
        {
            transform.position=jumpPos + new Vector3 (0,sideJump.Evaluate(currentTime),-currentTime/totalTimeSide);
            currentTime += Time.deltaTime;
            if(currentTime >= totalTimeSide)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                goingRight = false;
                stable = true;
                transform.position=jumpPos + new Vector3 (0,0,-1);
                currentTime = 0;
            }
        }
    }

    private void InputController(SwipeController.SwipeType type) 
    {
        if(type == SwipeController.SwipeType.UP && (stable))
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            jumpPos = transform.position;
            goingForward = true;
            stable = false;
            GameObject.Find("Stairs").GetComponent<StairsController>().AddStair();
        }
        if(type == SwipeController.SwipeType.LEFT && (stable))
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            jumpPos = transform.position;
            goingLeft = true;
            stable = false;
        }
        if(type == SwipeController.SwipeType.RIGHT && (stable))
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            jumpPos = transform.position;
            goingRight = true;
            stable = false;
        }
    }       
    
    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Enemy")
        {
            stable = true;
            goingForward = false;
            goingLeft = false;
            goingRight = false;
            rb.useGravity = true;
            rb.isKinematic = false;
            transform.position = new Vector3(0,0.75f,0);
            GameObject.Find("Canvas").GetComponent<MenuController>().GameEnded();
        }
    }                     
}
