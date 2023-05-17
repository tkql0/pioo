using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    Vector3 CamPos = new Vector3(0, 0, -10);
    public GameObject player;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    public float speed;

    float height;

    private void Start()
    {
        GameTree.Instance.gameManager.cameraControll = this;

        height = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        if (GameTree.Instance.gameManager.GameStart != true)
            return;
        LimitCamArea();
    }

    void LimitCamArea()
    {
        transform.position = Vector3.Lerp(transform.position,
            player.transform.position + CamPos, Time.fixedDeltaTime * speed);
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(transform.position.x, clampY, -10f);
    }
}
