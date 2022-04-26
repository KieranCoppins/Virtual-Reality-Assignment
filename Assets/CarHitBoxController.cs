using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controlls the car hit box collider that looks out for pedestrians. Communicates directly to the car controller 
public class CarHitBoxController : MonoBehaviour
{
    public CarController carController;

    public int hazards;

    // Update is called once per frame
    void Update()
    {
        if (hazards > 0){
            carController.Decelerate();
        }
        else{
            carController.Accelerate();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Pedestrian"){
            hazards++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Pedestrian"){
            hazards--;
        }
    }
}
