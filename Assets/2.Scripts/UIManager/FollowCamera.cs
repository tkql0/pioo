using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Vector3 CamPos = new Vector3(0, 0, -15);

    public void Init()
    {
        targetCamera();
    }

    private void targetCamera()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, CamPos.z);
        transform.position = targetPosition;
    }
}
