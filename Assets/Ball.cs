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
using Microsoft.Win32.SafeHandles;
using System;
public class Ball : MonoBehaviour
{
    public float speed;
    public GameObject ball;
    public GameObject launchPoint;
    public GameObject dangerZone;
    public GameObject goalZone;
    public GameObject fishingReel;
    public List<GameObject> findFish;
    public GameObject fish;
    public GameObject chosenFish;
    public GameObject fishDisplay;
    public Rigidbody2D rb;
    public Vector3 launchPosition;
    public Vector2 reel;
    PlayerControls controls;


    public float gravity;
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Reeling.performed += ctx => reel = ctx.ReadValue<Vector2>();
        controls.Gameplay.Reeling.canceled += ctx => reel = Vector2.zero;
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

    public void Start()
    {
        gravity = ball.GetComponent<Rigidbody2D>().gravityScale;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 m = new Vector3 (reel.x * 5, 0, reel.y * 5);
        fishingReel.GetComponent<Transform>().Rotate(Vector3.up * reel.y * .2f);

    }
    public void Launch()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = gravity;
        rb.AddForceY(500f);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        PolygonCollider2D ballBox = ball.GetComponent<PolygonCollider2D>();
        BoxCollider2D failure = dangerZone.GetComponent<BoxCollider2D>();
        if ((other.gameObject == dangerZone) && (ballBox.bounds.Intersects(failure.bounds)))
        {
            Debug.Log("ball has landed in the danger zone, resetting now...");
            Reset();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Goal")))
        {
            Debug.Log("Looks like you got something! Reel it in!");
            rb.linearVelocityX = 0;
            rb.linearVelocityY = 0;
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX
                        & RigidbodyConstraints2D.FreezePositionY
                        & RigidbodyConstraints2D.FreezeRotation;
            BeginFishing();
            StartCoroutine(DelayReelIn(5f));
            return;
        }
    }
    public IEnumerator DelayLaunch(float delay)
    {
        yield return new WaitForSeconds(delay);
        Launch();
    }
    public IEnumerator DelayReelIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Reset();
    }
    public void Reset()
    {
        Debug.Log("ball is now being reset");
        launchPosition = new Vector2(launchPoint.transform.position.x, launchPoint.transform.position.y);
        transform.position = launchPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX 
                        & RigidbodyConstraints2D.FreezePositionY 
                        & RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DelayLaunch(2f));
        return;
    }
    // function for second part of fishing mini game, in which the player must reel in the fish using a button prompt
    public void BeginFishing()
    {
        // add to the findFish list all fishes with the correct tag
        findFish.AddRange(GameObject.FindGameObjectsWithTag("FishList"));
        //  loop through just once and find a random fish in the list
        for (int i = 0; i < 2; i++)
        {
            // the index for the loop selects a random piece within the list
            int index = UnityEngine.Random.Range(1, findFish.Count);
            Debug.Log("index is " + index);
            if (findFish[index] != null)
            {
                Debug.Log("the randomly chosen fish is " + chosenFish);
                // the chosen fish is the random fish that was found
                chosenFish = findFish[index];
                // instantiate the chosen fish at the position of the display
                GameObject newFish = Instantiate(chosenFish, new Vector3(fishDisplay.transform.position.x,
                                                                     fishDisplay.transform.position.y,
                                                                     fishDisplay.transform.position.z),
                                                                     fishDisplay.transform.rotation);
                newFish.name = findFish[i].name;

            }
            else if (index > findFish.Count)
            {
                Debug.Log("the index of the chosen piece is " + index);
                return;
            }
            else
            {
                return;
            }
        }
    }
}
