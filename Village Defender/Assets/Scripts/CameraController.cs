using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform gameObjectToFollow;
    private Vector3 initialOffset = Vector3.zero;
    public float rotationAngle;
    public Vector3 cameraTranslation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - gameObjectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationAxis;
        gameObjectToFollow.rotation.ToAngleAxis(out rotationAngle, out rotationAxis);
        RotateCamera(rotationAngle);

        transform.position = initialOffset + gameObjectToFollow.position + cameraTranslation;
        transform.eulerAngles = gameObjectToFollow.eulerAngles;
    }

    void RotateCamera(float rotationAngle)
    {
        cameraTranslation.z = 7.5f * Mathf.Cos((rotationAngle + 180) * Mathf.PI / 180);
        cameraTranslation.x = 7.5f * Mathf.Sin((rotationAngle + 180) * Mathf.PI / 180);
    }
}
