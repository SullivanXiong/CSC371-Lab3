using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 translateOffset = Vector3.zero;
    private Vector3 rotationCamera = Vector3.zero;
    private Vector3 cameraInitialOffset;
    private Animator animatorComponent;
    private Rigidbody rigidBodyComponent;
    private float rotationOffset = 0;
    private bool grounded;

    [SerializeField] Transform cameraObject;
    [SerializeField] float translationSpeed = 5f;
    [SerializeField] float sensitivity = 5f;
    [SerializeField] float jumpForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animatorComponent = GetComponent<Animator>();
        rigidBodyComponent = GetComponent<Rigidbody>();
        cameraInitialOffset = cameraObject.position;
        grounded=true;
    }

    // Update is called once per frame
    void Update()
    {
        translateOffset = GetTranslate();
        rotationOffset = GetRotation() * sensitivity;

        Animate(translateOffset);

        transform.Translate(translateOffset * Time.deltaTime * translationSpeed);
        transform.eulerAngles += new Vector3(0, rotationOffset, 0);
    }
    Vector3 GetTranslate()
    {
        Vector3 moving = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moving += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.A))
        {
             moving += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
             moving += Vector3.back;
        }
        else if (Input.GetKey(KeyCode.D))
        {
             moving += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            animatorComponent.SetTrigger("Jump");
            rigidBodyComponent.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            grounded = false;
        }

        return moving;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    float GetRotation()
    {
        return Input.GetAxis("Mouse X");
    }

    void Animate(Vector3 translateOffset)
    {
        if (translateOffset != Vector3.zero)
        {
            animatorComponent.SetBool("isWalking", true);
        }
        else
        {
            animatorComponent.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animatorComponent.SetBool("isRunning", true);
            translationSpeed += 7.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animatorComponent.SetBool("isRunning", false);
            translationSpeed -= 7.5f;
        }

        if (Input.GetMouseButtonDown(0) && !animatorComponent.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !animatorComponent.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animatorComponent.SetTrigger("Attack1");
        }
        if (Input.GetMouseButtonDown(1) && !animatorComponent.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !animatorComponent.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animatorComponent.SetTrigger("Attack2");
        }
    }
}