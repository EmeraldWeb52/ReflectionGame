using System.Collections;
using Unity.VisualScripting;
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
    public bool isGrounded = false;
    public float jumpQueueTime;

    float averaged;
    [SerializeField] AudioClip[] jumpSounds;
    [SerializeField] AudioSource mainJumpSounds;
    private playerInput playerInp;

    void Start()
    {
        playerInp = GetComponent<playerInput>();
        rb = GetComponent<Rigidbody2D>();
        if (jumpSounds.Length > 0)
        {
            foreach (AudioClip ac in jumpSounds)
            {
                averaged += ac.length;
            }
            averaged /= jumpSounds.Length;
        }
        StartCoroutine(Playinging());
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
    IEnumerator Playinging()
    {

        while (true)
        {
            yield return new WaitUntil(() => playerInp.doJump && !isJumping);
            if (playerInp.doJump && !isJumping)
            {
                yield return StartCoroutine(Playing());
            }
        }
    }

    //Randomizes moving sounds in averaged intervals (bad idea? :((( )
    IEnumerator Playing()
    {
        while (playerInp.doJump && !isJumping && jumpSounds.Length > 0 && this.gameObject.GetComponent<playerJump>().isGrounded)
        {
            int Randomized = Random.Range(0, jumpSounds.Length);
            makeSound(jumpSounds[Randomized], this.transform.position, Random.Range(0.8f, 1.1f));
            yield return new WaitForSeconds(averaged);
        }
        yield break;
    }
    void makeSound(AudioClip clip, Vector2 position, float pitch = 1)
    {
        GameObject tragfge = new GameObject();
        tragfge.transform.position = position;
        AudioSource audioski = tragfge.AddComponent<AudioSource>();
        audioski.pitch = pitch;
        audioski.clip = clip;
        audioski.Play();
        Destroy(tragfge, (clip.length / pitch) * 1.1f);
    }
}
