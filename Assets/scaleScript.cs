using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleScript : MonoBehaviour
{
    public Camera mainCamera;
    public Player player;

    Vector2 initPosition;
    float initJumpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 plPos = player.transform.position;
        initPosition = new Vector2(plPos.x, plPos.y);
        initJumpSpeed = player.jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = player.transform;
        Transform t = GameObject.Find("Smaller").transform;

        float deltaX = playerTransform.position.x - initPosition.x;
        if (deltaX > 0)
        {
            float expDX = Mathf.Exp(-deltaX);
            mainCamera.orthographicSize = 5 * expDX  + 0.1f;
            playerTransform.localScale = new Vector3(mainCamera.orthographicSize / 5.0f, mainCamera.orthographicSize / 5.0f, 1);
            player.jumpSpeed = (initJumpSpeed - 3.5f) * expDX + 3.5f;
            player.GetComponent<Rigidbody2D>().gravityScale = (2.5f - 0.1f) * expDX + 0.1f; 
        }
        else
        {
            mainCamera.orthographicSize = 5;
            playerTransform.localScale = new Vector3(1, 1, 1);
        }
        mainCamera.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, mainCamera.transform.position.z);


    }
}
