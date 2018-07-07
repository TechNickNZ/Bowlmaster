using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public Text standingDisplay;
    public GameObject pinsPrefab;

    private int lastSettleCount = 10;
    private int lastStandingCount = -1;
    private bool ballLeftBox = false;
    private float lastChangeTime;

    private Ball ball;
    private Animator animator;
    private ActionMaster actionMaster = new ActionMaster(); // We need it here as we only want 1 instance

    // Use this for initialization
    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();
        if (ballLeftBox)
        {
            standingDisplay.color = Color.red;
            UpdateStandingCountAndSettle();
        }
    }

    public void SetBallOutOfPlay(bool state)
    {
        ballLeftBox = state;
    }

    private void WaitForPinsToSettle()
    {
        float settleTime = 3f; //How long to wait to consider pins settled
        if ((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
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
        if ((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }
    
    public void RenewPins()
    {
        Debug.Log("Renewing pins");
        lastSettleCount = 10;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            Destroy(pin.gameObject);
        }
        GameObject newPins = Instantiate(pinsPrefab, new Vector3 (0,Pin.distanceToRaise,1829), Quaternion.identity);
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
    
    private void PinsHaveSettled()
    {
        int standing = CountStanding();
        int pinFall = lastSettleCount - standing;
        lastSettleCount = standing;

        ActionMaster.Action action = actionMaster.Bowl(pinFall);

        HandleAction(action);
        Debug.Log("Pinfall: " + pinFall + " " + action);

        ball.Reset();
        lastStandingCount = -1; //indicates pins have settled, and ball not back in box
        ballLeftBox = false;
        standingDisplay.color = Color.green;
    }

    private void HandleAction(ActionMaster.Action actionToHandle)
    {
        if (actionToHandle == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (actionToHandle == ActionMaster.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
        }
        else if (actionToHandle == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
        }
        else if (actionToHandle == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle end game yet");
        }
    }
    
    public int CountStanding()
    {
        int standing = 0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standing++;
            }
        }
        return standing;
    }
}
