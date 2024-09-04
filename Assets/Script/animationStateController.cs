using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("isWalking", true);
            // movementDirection = Vector3.forward;
            // Player.transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        }
        if (!(Input.GetKeyDown(KeyCode.I)))
        {
            animator.SetBool("isWalking", false);
        }
    }
}
