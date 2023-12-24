using UnityEngine;

public class NewPlayerMove : MonoBehaviour
{
    #region マグさんの変数
    [SerializeField] ScoreManager scoreManager;
    PlayerHP playerHP;
    private Rigidbody2D rb;
    private Vector2 startScale;
    bool inDamage = false;
    Animator animator;
    private bool isGrounded;
    private float horizontalInput;
    private float groundCheckDistance = 0.3f;
    float downPercentage = 1.0f;

    private int groundLayer = 1 << 6; // Ground
    #endregion

    #region 新しい変数(受け取るパラメータ)
    private int _jumpValue = default;
    private float _jumpForce = 10.0f;
    private float _jumpPercentage = 0.5f;
    private int _speedValue = default;
    private float _speedForce = 5.0f;
    private readonly float _speedPercentage = 1.0f;

    private bool _canMove = false;
    #endregion

    public int JumpValue
    {
        set
        {
            _jumpValue = value;

            _jumpForce += _jumpValue * _jumpPercentage;
        }
    }
    public int SpeedValue
    {
        set
        {
            _speedValue = value;
            _speedForce += _speedValue * _speedPercentage;
        }
    }
    public bool CanMove
    {
        set
        {
            _canMove = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
        rb = GetComponent<Rigidbody2D>();
        startScale = transform.localScale;
        animator = GetComponent<Animator>();

        _canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canMove) return;

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

    }

    private void FixedUpdate()
    {
        if (inDamage || !_canMove) return;
        isGrounded = CheckGround();

        if (isGrounded || horizontalInput != 0)
        {
            rb.velocity = new Vector2(horizontalInput * _speedForce , rb.velocity.y);
            //rb.AddForce(Vector2.right * direction * _speedForce);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private bool CheckGround()
    {
        Vector2 positionLeft = (Vector2)transform.position - Vector2.left * 0.38f - Vector2.up;
        Vector2 positionRight = (Vector2)transform.position - Vector2.right * 0.38f - Vector2.up;
        Vector2 direction = Vector2.down;
        float distance = groundCheckDistance;

        RaycastHit2D hitLeft = Physics2D.Raycast(positionLeft, direction, distance, groundLayer);
        Debug.DrawRay(positionLeft, direction * distance, Color.red);
        RaycastHit2D hitRight = Physics2D.Raycast(positionRight, direction, distance, groundLayer);
        Debug.DrawRay(positionRight, direction * distance, Color.red);
        return hitLeft.collider != null || hitRight.collider != null;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
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
            _speedForce *= downPercentage;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        var recieve = collision.gameObject.GetComponent<IReceiveable>();

        if (recieve != null)
        {
            _speedForce /= downPercentage;
        }
    }

    private void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
