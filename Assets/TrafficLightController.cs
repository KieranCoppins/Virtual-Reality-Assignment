using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;
    public GameObject t1green;
    public GameObject t1red;
    //Added a collider box that physically blocks the road when lights are red
    public GameObject t1Box;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t2Box;

    public GameObject t3green;
    public GameObject t3red;
    public GameObject t3Box;



    public float stateTimer;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");

        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t1Box = t1.Find("Traffic Box").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t2Box = t2.Find("Traffic Box").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;
        t3Box = t3.Find("Traffic Box").gameObject;
        stateTimer = 10.0f;
        //To begin with since state 1 will always be called first, put box 2 and 3 5 units in the air
        t2Box.transform.position += new Vector3 (0, 5, 0);
        t3Box.transform.position += new Vector3 (0, 5, 0);
        SetState(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (stateTimer > 0.0f){
            stateTimer -= Time.deltaTime;
        }
        else{
            if (state == 1){
                SetState(0);
            }
            else{
                SetState(1);
            }
            stateTimer = 10.0f;
        }
    }

    void SetState(int c)
    {
        state = c;
        if (c == 1)
        {
            //If t1 is green, move the box out of the way and block t2 and t3
            t1green.active = true;
            t1red.active = false;
            t1Box.transform.position += new Vector3 (0, 5, 0);
            t2green.active = false;
            t2red.active = true;
            t2Box.transform.position -= new Vector3 (0, 5, 0);
            t3green.active = false;
            t3red.active = true;
            t3Box.transform.position -= new Vector3 (0, 5, 0);
        }
        else
        {
            //If t1 is red, move the box in the way and open t2 and t3
            t1green.active = false;
            t1red.active = true;
            t1Box.transform.position -= new Vector3 (0, 5, 0);
            t2green.active = true;
            t2red.active = false;
            t2Box.transform.position += new Vector3 (0, 5, 0);
            t3green.active = true;
            t3red.active = false;
            t3Box.transform.position += new Vector3 (0, 5, 0);
            }
    }
}
