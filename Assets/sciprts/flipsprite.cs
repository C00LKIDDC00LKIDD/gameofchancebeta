using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 newScale = new Vector3(-1f, 1f, 1f);
    public Vector3 oldscale = new Vector3(1f, 1f, 1f);
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        Vector3 localVel = rb.linearVelocity; // already in world space
                                              // Rotate velocity into local space if needed
        Vector3 localVelLocal = Quaternion.Inverse(transform.rotation) * localVel;

        if (localVelLocal.x < -0.1f)
        {
            targetObject.transform.localScale = newScale;


        }
       else if(localVelLocal.x > 0.1f)
        {

            targetObject.transform.localScale = oldscale;

        }
    }

}