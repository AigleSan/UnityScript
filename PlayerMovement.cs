using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;


    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer SpriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement ;


    private void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;    //permet les déplacements horizontaux du personnages, Horizontal est une fonctionalités d'unity qui perlet de controler avec les fleches du clavier

        if (Input.GetButtonDown("Jump") && isGrounded)                       //condition qui permet de faire sauter le personnage si le bouton Jump donc espace est presse et si le personnage est au sol
        {
            isJumping = true;
        }


        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);                 // pour que la valeur de la vitesse renvoyee soit toujours positive on la rend absolue
        animator.SetFloat("Speed", characterVelocity);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        MovePlayer(horizontalMovement);

    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {

        if(_velocity > 0.1f)
        {
            SpriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            SpriteRenderer.flipX = true;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
