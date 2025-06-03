using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class Ball : MonoBehaviour
{
    public float speed;
    public GameObject ball;
    public GameObject dangerZone;
    public Rigidbody2D rb;
    public GameObject launchPoint;
    public Vector3 launchPosition;
    private float thrust = 300f;

    public float gravity;

    public void Start()
    {
        gravity = ball.GetComponent<Rigidbody2D>().gravityScale;
    }
    // Update is called once per frame
    void Update()
    {

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
        Debug.Log("ball has landed in the danger zone, resetting now...");
        if ((other.gameObject == dangerZone) && (ballBox.bounds.Intersects(failure.bounds)))
        {
            Reset();
        }
    }
    public IEnumerator DelayLaunch(float delay)
    {
        yield return new WaitForSeconds(delay);
        Launch();
    }
    public IEnumerator DelayChop(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    public void Reset()
    {
        Debug.Log("ball is now being reset");
        launchPosition = new Vector2(launchPoint.transform.position.x, launchPoint.transform.position.y);
        transform.position = launchPosition;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX 
                        & RigidbodyConstraints2D.FreezePositionY 
                        & RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DelayLaunch(2f));
        return;
    }
}
