using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallDragLaunch : MonoBehaviour {

    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    private Ball ball;

	// Use this for initialization
	private void Start ()
    {
        ball = GetComponent<Ball>();
	}
	
    public void DragStart()
    {
        //Capture time and position of drag start
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd()
    {
        //Launch the ball
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;

        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

        Vector3 launchVeloctiy = new Vector3(launchSpeedX, 0, launchSpeedZ);
        ball.Launch(launchVeloctiy);
    }

    public void MoveStart(float xNudge)
    {
        //lane width 105
        if (!ball.inPlay)
        {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));
            float ballPositionX = Mathf.Clamp(ball.transform.position.x, -52.5f, 52.5f);
            float ballPositionY = ball.transform.position.y;
            float ballPositionZ = ball.transform.position.z;
            Vector3 newBallPosition = new Vector3(ballPositionX, ballPositionY, ballPositionZ);
            ball.transform.position = newBallPosition;
        }
    }
}
