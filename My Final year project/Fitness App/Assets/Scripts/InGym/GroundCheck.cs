using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadious;
    [SerializeField] LayerMask whatisGround;
    public bool isGrounded;
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadious, whatisGround);
    }
}
