using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public int lastStandingCount = -1;
    public Text standingDisplay;
    public GameObject pinsPrefab;

    private bool ballEnteredBox = false;
    private float lastChangeTime;
    private Ball ball;
    private Animator animator;

    // Use this for initialization
    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();
        if (ballEnteredBox)
        {
            UpdateStandingCountAndSettle();
        }

    }

    public void RenewPins()
    {
        Debug.Log("Renewing pins");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            Destroy(pin.gameObject);
        }
        GameObject newPins = Instantiate(pinsPrefab, new Vector3 (0,0.505f,1829), Quaternion.identity);
        foreach (Pin pin in newPins.GetComponentsInChildren<Pin>())
        {
            pin.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.RaiseIfStanding();
            }
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    private void UpdateStandingCountAndSettle()
    {
        // update the lastStanding count
        // Call PinsHaveSettled() when they have
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; //How long to wait to consider pins settled
        if((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }

    private void PinsHaveSettled()
    {
        ball.Reset();
        lastStandingCount = -1; //indicates pins have settled, and ball not back in box
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject thingHit = collision.gameObject;
        if (thingHit.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    public int CountStanding()
    {
        int standing = 0;
        foreach(Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if(pin.IsStanding())
            {
                standing++;
            }
        }
        return standing;
    }
}
