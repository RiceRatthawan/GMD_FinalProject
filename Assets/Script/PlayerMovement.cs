using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    // public GameObject Player;
    public float speed;
    public float rotationSpeed;
    public KeyCode Forward;
    public KeyCode Backward;
    public KeyCode LeftDir;
    public KeyCode RightDir;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new Vector3(0,0,0);
        
        if (Input.GetKey(Forward))
        {
            animator.SetBool("isWalking", true);
            movementDirection = Vector3.forward;
            movementDirection.Normalize();
            transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(Backward))
        {
            animator.SetBool("isWalking", true);
            movementDirection = Vector3.back;
            movementDirection.Normalize();
            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(LeftDir))
        {
            animator.SetBool("isWalking", true);
            movementDirection = Vector3.left;
            movementDirection.Normalize();
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetKey(RightDir))
        {
            animator.SetBool("isWalking", true);
            movementDirection = Vector3.right;
            movementDirection.Normalize();
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        if(movementDirection != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(Forward))
        {
            animator.SetBool("isWalking", false);
        }
        if (Input.GetKeyUp(Backward))
        {
            animator.SetBool("isWalking", false);
        }
        if (Input.GetKeyUp(LeftDir))
        {
            animator.SetBool("isWalking", false);
        }
        if (Input.GetKeyUp(RightDir))
        {
            animator.SetBool("isWalking", false);
        }
    }
}
