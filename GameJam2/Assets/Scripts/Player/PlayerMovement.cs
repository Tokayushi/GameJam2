using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    public bool isGrounded;
    private Animator animator;

    // Coyote Time
    public float coyoteTime = 0.2f; // Tiempo extra para saltar despu�s de caer
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el Animator
        AudioManager.Instance.PlayMusic(AudioManager.Instance.presentMusic);
        
    }

    void Update()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Control de animaciones
        if (move != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Girar personaje seg�n la direcci�n del movimiento
        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Actualizar el temporizador de Coyote Time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Salto con Coyote Time
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0; // Evitar saltos m�ltiples en el aire
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jumpSound);
        }
    }

    // Detectar suelo
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("past") || collision.gameObject.CompareTag("present") || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("past") || collision.gameObject.CompareTag("present") || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

