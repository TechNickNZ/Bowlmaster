using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    
    private void OnTriggerExit(Collider other)
    {
        GameObject thingLeft = other.gameObject;
        if (thingLeft.GetComponent<Pin>())
        {
            Destroy(thingLeft);
        }
    }
}
