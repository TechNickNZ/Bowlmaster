using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public bool inPlay = false;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private Vector3 ballStartPosition;
    private Quaternion startRotation;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ballStartPosition = transform.position;
        startRotation = transform.rotation;
        Reset();
    }

    public void Launch(Vector3 velocity)
    {
        inPlay = true;

        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;
        audioSource.Play();
    }

    public void TestLaunch()
    {
        Launch(new Vector3(0f,0f,800f));
    }

    public void Reset()
    {
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
        transform.position = ballStartPosition;
        transform.rotation = startRotation;
        inPlay = false;
    }
}
