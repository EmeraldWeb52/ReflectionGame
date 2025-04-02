using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed;
    private bool facingright = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private playerInput playerInp;
    float averaged;
    [SerializeField] AudioClip[] moveSounds;
    [SerializeField] AudioSource mainPlayerSounds;
    void Start()
    {
        playerInp = GetComponent<playerInput>();
        rb = GetComponent<Rigidbody2D>();
        if (moveSounds.Length > 0)
        {
            foreach (AudioClip ac in moveSounds)
            {
                 averaged += ac.length;
            }
            averaged /= moveSounds.Length;
        }
        StartCoroutine(Playinging());
    }
    IEnumerator Playinging()
    {

         while (true)
         {
              yield return new WaitUntil(() => (playerInp.horiMove != 0 && this.gameObject.GetComponent<playerJump>().isGrounded));
              yield return new WaitForSeconds(0.05f);
              if (playerInp.horiMove != 0)
              {
                   yield return StartCoroutine(Playing());
              }
         }
    }

    //Randomizes moving sounds in averaged intervals (bad idea? :((( )
    IEnumerator Playing()
    {
         while (playerInp.horiMove != 0 && moveSounds.Length > 0 && this.gameObject.GetComponent<playerJump>().isGrounded)
         {
              int Randomized = Random.Range(0, moveSounds.Length);
              makeSound(moveSounds[Randomized], this.transform.position, Random.Range(0.8f, 1.1f));
              yield return new WaitForSeconds(averaged);
         }
         yield break;
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
