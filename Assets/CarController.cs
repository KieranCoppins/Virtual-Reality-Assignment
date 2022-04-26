using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;

    int targetWP = 0;

    Rigidbody rb;

    public bool go;
    public float initialDelay;

    public float maxSpeed;
    public float acceleration;
    public float breakingPower;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        GameObject wp;
        wp = GameObject.Find("Car WP1");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP2");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP3");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP4");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP5");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP6");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP7");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP8");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP9");
        wps.Add(wp.transform);
        wp = GameObject.Find("Car WP10");
        wps.Add(wp.transform);
        rb = GetComponent<Rigidbody>();
        SetRoute();
        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Had weird behaviour with the rigidbodys interal velocity made a weird reverse gliding effect, this helps battle that
        if (rb.velocity.magnitude <= 0){
            rb.velocity = new Vector3(0,0,0);
        }

        if (!go){
            initialDelay -= Time.deltaTime;
            //If we're not good to go yet we shouldnt be accelerating
            speed = 0;
            if (initialDelay <= 0.0f){
                go = true;
                SetRoute();
            }
            else return;
        }
        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        //Increased dist size since sometimes cars wherent registering they arrived at their position
        if (dist < 0.2f){
            targetWP++;
            if (targetWP >= route.Count){
                SetRoute();
                return;
            }
        }

        //calculate velocity for this frame
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 2.5f;
        //apply velocity
        Vector3 newPosition = transform.position;
        //Added a speed variable here
        newPosition += velocity * (Time.deltaTime * speed);
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation); 
        
    }

    void SetRoute(){
        //randomise the next route
        routeNumber = Random.Range(0, 6);
        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1]};
        else if (routeNumber == 1) route = new List<Transform> { wps[0], wps[6], wps[2]};
        else if (routeNumber == 2) route = new List<Transform> { wps[4], wps[5]};
        else if (routeNumber == 3) route = new List<Transform> { wps[4], wps[9], wps[2]};
        else if (routeNumber == 4) route = new List<Transform> { wps[3], wps[8], wps[5]};
        else if (routeNumber == 5) route = new List<Transform> { wps[3], wps[7], wps[1]};

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.0f, route[0].position.z);
        targetWP = 1;
    }

    //Increases speed by time * acceleration
    public void Accelerate(){
        if (speed < maxSpeed){
            speed += Time.deltaTime * acceleration;
            if (speed > maxSpeed){
                speed = maxSpeed;
            }
        }
    }

    //Decreases speed by time * breakingPower
    public void Decelerate(){
        if (speed > 0){
            speed -= Time.deltaTime * breakingPower;
            if (speed <= 0){
                speed = 0;
            }
        }
    }
}
