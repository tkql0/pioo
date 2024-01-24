using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 CamPos = new Vector3(0, 0, -15);

    public void Init()
    {
        targatCamera();
    }

    private void targatCamera()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, CamPos.z);
        transform.position = targetPosition;
    }
}
