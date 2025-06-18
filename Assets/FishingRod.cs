using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public string horizontalInputName = "Horizontal";
    public string verticalInputName = "Vertical";
    float currentYRotation;
    float currentXRotation;
    public Rigidbody2D rb;

    private void Start()
    {
        currentXRotation = transform.rotation.x;
        currentYRotation = transform.rotation.y;
        rb = GetComponent<Rigidbody2D>();
        RigidbodyConstraints2D constraints = rb.constraints;
        

    }
    void Update()
    {
        float horizontal = Input.GetAxis(horizontalInputName);
        float vertical = Input.GetAxis(verticalInputName);

        if (horizontal != 0 || vertical != 0)
        {
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, angle);
        }
    }
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }
}
