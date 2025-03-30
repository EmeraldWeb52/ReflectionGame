using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed;
    private bool facingright = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private playerInput playerInp;
    void Start()
    {
        playerInp = GetComponent<playerInput>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (facingright == false && playerInp.horiMove > 0)
        {
            flip();
        }
        else if (facingright == true && playerInp.horiMove < 0)
        {
            flip();
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerInp.horiMove * speed, rb.linearVelocity.y);
    }
    void flip()
    {
        facingright = !facingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
