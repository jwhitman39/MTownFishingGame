using System.Collections;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.WSA;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class Logic : MonoBehaviour
{
    public GameObject launchPoint;
    public GameObject dangerZone;
    public GameObject goalZone;
    public GameObject fishingReel;
    public GameObject ball;
    public GameObject tickers;
    public Vector3 launchPosition;
    public Vector2 reel;
    PolygonCollider2D ballBox;
    Ball BallLogic = null;
    Ticker TickerLogic = null;
    PlayerControls controls;
    private void Awake()
    {
        BallLogic = ball.GetComponent<Ball>();
        TickerLogic = tickers.GetComponent<Ticker>();
        Debug.Log("this is ball" + BallLogic.name);
        ballBox = BallLogic.GetComponent<PolygonCollider2D>();
        Debug.Log("the ball has a collider:" + ballBox);
        controls = new PlayerControls();
        controls.Gameplay.Reeling.performed += ctx => reel = ctx.ReadValue<Vector2>();
        controls.Gameplay.Reeling.canceled += ctx => reel = Vector2.zero;
        // just in case we do button prompts...
        //controls.Gameplay.PressA.performed += ctx => PressA();
        //controls.Gameplay.PressB.performed += ctx => PressB();
        //controls.Gameplay.PressY.performed += ctx => PressY();
        //controls.Gameplay.PressX.performed += ctx => PressX();
        //controls.Gameplay.PressL.performed += ctx => PressL();
        //controls.Gameplay.PressR.performed += ctx => PressR();
        //controls.Gameplay.PressZL.performed += ctx => PressZL();
        //controls.Gameplay.PressZR.performed += ctx => PressZR();
        //controls.Gameplay.PressLS.performed += ctx => PressLS();
        //controls.Gameplay.PressRS.performed += ctx => PressRS();

    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("(in)sanity check...");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 m = new Vector3(reel.x * 5, 0, reel.y * 5);
        fishingReel.GetComponent<Transform>().Rotate(Vector3.up * reel.y * .2f);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        BoxCollider2D failure = dangerZone.GetComponent<BoxCollider2D>();
        if ((other.gameObject == dangerZone) && (ballBox.bounds.Intersects(failure.bounds)))
        {
            Debug.Log("ball has landed in the danger zone, resetting now...");
            Reset();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PolygonCollider2D ballBox = BallLogic.GetComponent<PolygonCollider2D>();
        BoxCollider2D goal = goalZone.GetComponent<BoxCollider2D>();
        if ((other.gameObject == goalZone) && (ballBox.bounds.Intersects(goal.bounds)))
        {
            Debug.Log("Looks like you got something! Reel it in!");
            //BeginFishing();
        }
    }
    public IEnumerator DelayLaunch(float delay)
    {
        yield return new WaitForSeconds(delay);
        BallLogic.Launch();
    }
    public void Reset()
    {
        Debug.Log("ball is now being reset");
        launchPosition = new Vector2(launchPoint.transform.position.x, launchPoint.transform.position.y);
        transform.position = launchPosition;
        BallLogic.rb.linearVelocity = Vector2.zero;
        BallLogic.rb.gravityScale = 0f;
        BallLogic.rb.constraints = RigidbodyConstraints2D.FreezePositionX
                        & RigidbodyConstraints2D.FreezePositionY
                        & RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DelayLaunch(2f));
        return;
    }
    // function for second part of fishing mini game, in which the player must reel in the fish using the analog stick
    public void BeginFishing()
    {

    }
}
