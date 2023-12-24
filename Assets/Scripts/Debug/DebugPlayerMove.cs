using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DebugPlayerMove : MonoBehaviour
{
    float moveSpeed = 200.0f;
    float jumpForce = 10.0f;
    float downPercentage = 1.0f;
    [SerializeField] float _statusLevel = 1.0f;
    private float groundCheckDistance = 1.6f;
    float horizontalInput = 0.0f;
    public LayerMask groundLayer;

    [SerializeField] private float _knockForce = 1.0f;
    bool inDamage = false;

    private bool isGrounded;

    private Rigidbody2D rb;

    [SerializeField] PlayerHP playerHP;
    //[SerializeField] ScoreManager scoreManager;
    [SerializeField] PlayerStatus playerStatus;
    private Vector2 startScale;

    [SerializeField] Animator animator;

    public void SettingStatus()
    {
        moveSpeed += moveSpeed * _statusLevel;
        jumpForce += jumpForce * _statusLevel;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startScale = transform.localScale;
    
    }

    void Update()
    {

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

        if(rb.velocity.y > 0.4f)
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
        
    }

    private void FixedUpdate()
    {

        isGrounded = CheckGround();

        if (isGrounded || horizontalInput != 0)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, rb.velocity.y);
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
            rb.AddForce(hitDirect * _knockForce, ForceMode2D.Impulse);

            Invoke("DamageEnd", 0.5f);
        }
        else if (scoreTarget != null)
        {
            int ScoreNum = scoreTarget.AddScore();
         //   scoreManager.AddScore(ScoreNum);
        }
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
