using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using GameControl;

namespace PlayerControl
{
    public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float downSpeed;
    [SerializeField] Vector3 groundVelocity = new Vector3(0f, 0f, 3f);
    [SerializeField] Vector3 jumpVector = new Vector3(0f, 0.5f, 0.5f);
    
    public GameObject smokeParticlesPrefab;
    public GameObject confettiParticlesPrefab;
    public Text scoreText;
   
    private Animator ac;
    private Rigidbody rb;
   
    private int currentScore;
    private bool isMoving;
    private bool isGrounded = false;

    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int Jump = Animator.StringToHash("Jump");


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            return;
        }
        
        if (Input.GetMouseButton(0) && isGrounded)
        {
            rb.velocity = groundVelocity;
        }
        
        if (!isGrounded && Input.GetMouseButton(0))
        {
            rb.AddForce(Vector3.down * downSpeed, ForceMode.VelocityChange);
            ac.SetTrigger(Fall);
        }
    }

    
    public void MoveCharacter(bool move)
    {
        isMoving = move;
        
       if(move)  ac.SetTrigger(Run);
    }
    
    

    private void OnCollisionEnter(Collision collision)
    {
        if (!isMoving)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            ac.SetTrigger(Run);
           
            Instantiate (smokeParticlesPrefab, transform.position, transform.rotation,transform);

            if (!Input.GetMouseButton(0))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Instantiate (confettiParticlesPrefab, transform.position, transform.rotation,transform);
            isMoving = false;
           
            GameController.Instance.Win();
            ac.SetInteger("Dance" , Random.Range(1, 4));
        }
        
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            currentScore += 1;
            HandleScore();
            
            Destroy(collision.transform.gameObject);
        } 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isMoving)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (!Input.GetMouseButton(0))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (!isMoving)
        {
            return;
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            ac.SetTrigger(Jump);
            
        }
    }
    
    private void HandleScore()
    {
        scoreText.text = " " + currentScore;
    } 
    
}

}
