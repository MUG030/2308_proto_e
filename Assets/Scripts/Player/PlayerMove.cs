using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMove : MonoBehaviour
{
    // ÉoÉOèCê≥ÇÃÇΩÇﬂstatic
    static float moveSpeed = 5.0f;
    static float jumpForce = 5.0f;
    float downPercentage = 1.0f;
    float _statusLevel = 5.0f;
    private float groundCheckDistance = 1.6f;
    float horizontalInput = 0.0f;
    public LayerMask groundLayer;

    [SerializeField] private float _knockForce = 1.0f;
    bool inDamage = false;

    private bool isGrounded;

    private Rigidbody2D rb;

    [SerializeField] PlayerHP playerHP;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] PlayerStatus playerStatus;
    private Vector2 startScale;

    private static bool puzzle;

    [SerializeField] Animator animator;


    //NEW ADD
    public float velLimit;
    public float JumpLimit;
    public int Hp;
  //  public float gravityLimit;

     float Speed = 150;
    float JumpForce = 50;
    

    public float[] SpeedData;
    public float[] JumpData;
    public int[] HartData;
   // public float[] GravityData;

    int direction;

    [SerializeField] Collider2D col;


    public bool Puzzle
    {
        set { puzzle = value; }
    }

    public void SettingStatus()
    {
        moveSpeed += (float)playerStatus.MoveSpeed * _statusLevel;
        jumpForce += (float)playerStatus.JumpForce * _statusLevel;
        Debug.Log("playerStatus.moveSpeed : " + (float)playerStatus.MoveSpeed);
        Debug.Log("statesLevel : " + _statusLevel);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startScale = transform.localScale;
        puzzle = false;
    }

    void Update()
    {
        if (!puzzle) return;
        if (inDamage)
        {
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));
        animator.SetBool("isGround", isGrounded);

        animator.SetBool("isJump", true);

        if (rb.velocity.y > 0.4f)
        {
            animator.SetBool("IsJump", true);
        }
        else
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", true);
        }
        if (isGrounded)
        {
            animator.SetBool("IsFall", false);
        }


        if (horizontalInput > 0.0f)
        {
            transform.localScale = new Vector2(startScale.x, startScale.y);
        }
        else if (horizontalInput < 0.0f)
        {
            transform.localScale = new Vector2(startScale.x * -1, startScale.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
       
        if(rb.velocity.x > velLimit)
        {
            rb.velocity = new Vector2(velLimit, rb.velocity.y);    
        }
        if (rb.velocity.x < -velLimit)
        {
            rb.velocity = new Vector2(-velLimit, rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            direction = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        if (rb.velocity.y > JumpLimit)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpLimit);
        }
       
       
    }

    private void FixedUpdate()
    {
        if (inDamage || !puzzle) return;
        isGrounded = CheckGround();

        if (isGrounded || horizontalInput != 0)
        {
            // rb.velocity = new Vector2(horizontalInput * moveSpeed , rb.velocity.y);
            rb.AddForce(Vector2.right * direction * Speed);
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }

    private bool CheckGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        return hit.collider != null;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<IDamageable>();
        var scoreTarget = collision.GetComponent<IScore>();
        if (target != null)
        {
            if (inDamage) return;

            int DamageNum = target.AddDamage();
            playerHP.SetLifeGauge2(DamageNum);
            inDamage = true;

            rb.velocity = new Vector2(0, 0);
            Vector2 hitDirect = (collision.transform.position - transform.position).normalized;
            if (transform.localScale.x > 0)
            {
                hitDirect.x *= -1;
            }
           // rb.AddForce(hitDirect * _knockForce, ForceMode2D.Impulse);

            Invoke("DamageEnd", 0.5f);
        }
        else if (scoreTarget != null)
        {
            int ScoreNum = scoreTarget.AddScore();
            scoreManager.AddScore(ScoreNum);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        var recieve = collision.gameObject.GetComponent<IReceiveable>();

        if (recieve != null)
        {
            downPercentage = recieve.Collide();
            moveSpeed *= downPercentage;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        var recieve = collision.gameObject.GetComponent<IReceiveable>();

        if (recieve != null)
        {
            moveSpeed /= downPercentage;
        }
    }

    private void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
