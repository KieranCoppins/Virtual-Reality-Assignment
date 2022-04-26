using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Controlls the pedestrain hit box, also communicates directly to the pedestrian route script
public class PedestrainHitBoxController : MonoBehaviour
{

    public PedestrainRoute pedestrainRoute;

    float standardSpeed;

    // Start is called before the first frame update
    void Start()
    {
        standardSpeed = pedestrainRoute.speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Car"){
            pedestrainRoute.speed = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Car"){
            pedestrainRoute.speed = standardSpeed;
        }
    }
}
