using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold;
    public static float distanceToRaise = 40f;

    private new Rigidbody rigidbody;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public bool IsStanding()
    {
        //Check X and Z rotation angle
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        
        float tiltInX = Mathf.Abs(Mathf.DeltaAngle(rotationInEuler.x,270));
        float tiltInZ = Mathf.Abs(Mathf.DeltaAngle(rotationInEuler.z,0));

        if(tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void RaiseIfStanding()
    {
        // raise standing pins only by distanceToRaise
        if (IsStanding())
        {
            rigidbody.useGravity = false;
            transform.Translate(new Vector3(0f, distanceToRaise, 0f), Space.World);
        }
        


        //Idea for using animator later
        //foreach(Pin pins in GameObject.FindObjectsOfType<Pin>())
        //{
        //    if (!pins.IsStanding())
        //    {
        //        Destroy(pins);
        //    }
        //}
        //if (FindObjectsOfType<Pin>().Length == 0)
        //{
        //    //all pins knocked over
        //}
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0f, 0f, -distanceToRaise));
        rigidbody.useGravity = true;
    }
}
