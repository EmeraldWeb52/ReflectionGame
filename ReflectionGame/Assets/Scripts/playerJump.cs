using System.Collections;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    private int extraJumps;
    public float jumpForce;
    public int extraJumpsValue;
    private bool isJumping;
    private bool isGrounded = false;
    public float jumpQueueTime;

    private playerInput playerInp;

    void Start()
    {
        playerInp = GetComponent<playerInput>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);

        if(playerInp.doJump && isJumping)
        {
            StartCoroutine(jumpQueue(jumpQueueTime));
        }
        
    }
    void FixedUpdate()
    {
        
        if (playerInp.doJump && !isJumping)
        {
            playerInp.doJump = false;
            isJumping = true;
            jump(jumpForce);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
    public void jump(float force)
    {
        rb.AddForce(new Vector2(rb.linearVelocity.x, force));
    }

    private IEnumerator jumpQueue(float queueTime)
    {
        yield return new WaitForSeconds(queueTime);
        playerInp.doJump = false;
    }
}
