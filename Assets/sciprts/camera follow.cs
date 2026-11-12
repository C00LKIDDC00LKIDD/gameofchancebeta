using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAheadCamera : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public float smoothTime = 0.3f;

    [Header("Look Ahead")]
    public float lookAheadDistance = 2f;
    public float lookAheadSpeed = 2f;

    [Header("Bounds")]
    public BoxCollider2D mapBounds;

    private Vector3 velocity = Vector3.zero;
    private Vector2 targetVelocity = Vector2.zero;
    private Vector2 lookAheadOffset = Vector2.zero;
    private Vector3 lastTargetPosition;
    private Camera cam;
    private float camHeight;
    private float camWidth;

    void Start()
    {
        cam = GetComponent<Camera>();
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        // Calculate target velocity
        targetVelocity = (target.position - lastTargetPosition) / Time.deltaTime;
        lastTargetPosition = target.position;

        // Calculate look ahead offset
        Vector2 targetLookAhead = targetVelocity.normalized * lookAheadDistance;
        lookAheadOffset = Vector2.Lerp(lookAheadOffset, targetLookAhead, lookAheadSpeed * Time.deltaTime);

        // Calculate desired camera position
        Vector3 desiredPos = target.position + (Vector3)lookAheadOffset;

        // Apply bounds if mapBounds is set
       

        desiredPos.z = transform.position.z;

        // Smooth movement to desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
    }

    void OnDrawGizmos()
    {
        if (target != null)
        {
            // Draw look ahead direction
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.position + (Vector3)lookAheadOffset, 0.2f);
            Gizmos.DrawLine(target.position, target.position + (Vector3)lookAheadOffset);
        }
    }
}