using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    [Header("Configurações de Movimento")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;

    [Header("Verificação de Chão")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount;
    private float moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Verifica se está tocando o chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Reinicia o contador de pulo quando toca o chão
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Pulo e pulo duplo
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Zera o Y para pulos consistentes
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }

        // Inverte sprite (opcional)
        if (moveInput > 0) { transform.localScale = new Vector3(1, 1, 1);}

        else if (moveInput < 0)
        { transform.localScale = new Vector3(-1, 1, 1);}
    }
}

    


