using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObj : MonoBehaviour
{
    [SerializeField] private CharacterController CC;
    private float pushPower = 0.5f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -1.0f)
        {
            return;
        }
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        Vector3 collisionPoint = hit.point;

        body.AddForceAtPosition(pushDir * pushPower, collisionPoint, ForceMode.Impulse);
    }
}
