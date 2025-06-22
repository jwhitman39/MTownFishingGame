using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
using Color = UnityEngine.Color;

public class Ticker : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.
    //private var rayCastHit : RaycastHit;
    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).
    public Color rayColor = Color.blue;
    public Rigidbody2D rb2D;
    public RaycastHit2D hit;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.down, rayColor);
        // Cast a ray straight down.
        hit = Physics2D.Raycast(transform.position, Vector2.left, 4, LayerMask.GetMask("FishingRod"));
        Debug.Log("hit is " + hit + "and hit.collider is " + hit.collider);
        if (hit && hit.collider.name == "Lever")
        {
            Debug.Log("successful hit.");



            //// Calculate the distance from the surface and the "error" relative
            //// to the floating height.
            //float distance = Mathf.Abs(hit.point.x - transform.position.x);
            //float heightError = floatHeight - distance;

            //// The force is proportional to the height error, but we remove a part of it
            //// according to the object's speed.
            //float force = GetComponent<Transform>().line

            //// Apply the force to the rigidbody.
            //rb2D.AddForce(Vector2.left * force);
        }
    }
}
