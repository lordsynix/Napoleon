using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float cameraSpeed = 0.1f;
    private float zoomSpeed = 5f;
    private float rotationSpeed = 0.1f;

    private float maxHeight = 50;
    private float minHeight = 2;

    private Vector3 lastLegalPosition;
    private Vector3 legalPosition = new Vector3(300, 11, 285);

    private Vector2 p1;
    private Vector2 p2;

    // Update is called once per frame
    void Update()
    {
        float movementSpeedX = cameraSpeed * Input.GetAxis("Horizontal");
        float movementSpeedY = cameraSpeed * Input.GetAxis("Vertical");
        float scrollSpeed = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        // Limits to Vertical Movement
        if ((transform.position.y >= maxHeight) && (scrollSpeed > 0))
        {
            scrollSpeed = 0;
        }
        else if (transform.position.y <= minHeight && scrollSpeed < 0)
        {
            scrollSpeed = 0;
        }

        if ((transform.position.y + scrollSpeed) > maxHeight)
        {
            scrollSpeed = maxHeight - transform.position.y;
        }

        else if ((transform.position.y + scrollSpeed) < minHeight)
        {
            scrollSpeed = minHeight - transform.position.y;
        }

        Vector3 verticalMovement = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMovement = movementSpeedX * transform.right;
        Vector3 forwardBackwardMovement = transform.forward;

        forwardBackwardMovement.y = 0;
        forwardBackwardMovement.Normalize();
        forwardBackwardMovement *= movementSpeedY;

        Vector3 move = verticalMovement + lateralMovement + forwardBackwardMovement;
        transform.position += move;

        GetCameraRotation();
        CheckGround();
        CheckBorders();
    }

    void GetCameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotationSpeed;
            float dy = (p2 - p1).y * rotationSpeed;

            p1 = p2;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));
        }
    }

    private void CheckGround()
    {
        if (!Physics.Raycast(transform.position, -transform.up))
        {
            transform.position = lastLegalPosition;
            transform.position = new Vector3(lastLegalPosition.x, lastLegalPosition.y + 0.5f, lastLegalPosition.z);
        }
        lastLegalPosition = transform.position;
    }

    private void CheckBorders()
    {
        if (transform.position.x < 0)
        {
            legalPosition = transform.position;
            legalPosition.x = 1;
            transform.position = legalPosition;
        }
        if (transform.position.x > 1300)
        {
            legalPosition = transform.position;
            legalPosition.x = 1299;
            transform.position = legalPosition;
        }
        if (transform.position.z < 0)
        {
            legalPosition = transform.position;
            legalPosition.z = 1;
            transform.position = legalPosition;
        }
        if (transform.position.z > 750)
        {
            legalPosition = transform.position;
            legalPosition.z = 749;
            transform.position = legalPosition;
        }
    }
}
