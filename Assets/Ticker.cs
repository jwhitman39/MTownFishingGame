using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
using Color = UnityEngine.Color;

public class Ticker : MonoBehaviour
{
    public Color rayColor = Color.blue;
    public Color rayColor2 = Color.red;
    public Rigidbody2D rb2D;
    public RaycastHit2D hit;
    public RaycastHit2D     hit2;
    public float fishingScore = 0;
    public bool topLever = false;
    public bool bottomLever = false;
    public GameObject spoolEnd;
    public GameObject reelAmount;
    float length = 0.4f;
    public Fish FishLogic = null;



    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        reelAmount.GetComponent<TextMeshProUGUI>().text = fishingScore.ToString();
        Debug.DrawRay(spoolEnd.transform.position, Vector3.left * length, rayColor);
        Debug.DrawRay(spoolEnd.transform.position, Vector3.right * length, rayColor2);
        // Cast a ray straight down.
        hit = Physics2D.Raycast(spoolEnd.transform.position, Vector2.left, length, LayerMask.GetMask("FishingRod"));
        hit2 = Physics2D.Raycast(spoolEnd.transform.position, Vector2.right, length, LayerMask.GetMask("FishingRod"));
        //Debug.Log("hit is " + hit + "and hit.collider is " + hit.collider);
        if (hit && hit.collider.name == "Lever")
        {
            Debug.Log("successful hit.");
            hit.collider.attachedRigidbody.MoveRotation(100);
            bottomLever = true;
        }
        if (hit2 && hit2.collider.name == "TopLever")
        {
            Debug.Log("successful hit up top.");
            hit2.collider.attachedRigidbody.MoveRotation(100);
            topLever = true; 
            bottomLever= false;
        }
        if (bottomLever && topLever)
        {
            AddScore();
        }
    }
    void AddScore()
    {
            topLever = false; 
            fishingScore++;
            Debug.Log("you have reeled in " + fishingScore + " times");
    }
    //void CheckScore()
    // a function that will crosscheck between your reel-in score and the amount required for the specific fish
    //{
    //}
}
