using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controlls the hit box used to make sure cars dont crash into eachother and stops at red lights.
//This works by adding a hazard to the car hit box controller.
public class SpaceHitBoxController : MonoBehaviour
{

    public CarHitBoxController carController;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Car"){
            carController.hazards++;
        }
        else if (other.tag == "Traffic Box"){
            carController.hazards++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Car"){
            carController.hazards--;
        }
        else if (other.tag == "Traffic Box"){
            carController.hazards--;
        }
    }
}
