using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    public bool isGrounded;
    private Animator animator;
    private bool isWalking;
    private bool inPast = false; // Controla en qué tiempo estamos

    // Coyote Time
    public float coyoteTime = 0.2f; // Tiempo extra para saltar despu�s de caer
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el Animator
        //AudioManager.Instance.PlayMusic(AudioManager.Instance.presentMusic);
        
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

            if (isGrounded && !isWalking)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WalkSound);
                isWalking = true;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            isWalking = false; // Detener el sonido de caminar
        }

        // Girar personaje seg�n la direcci�n del movimiento
        if (move > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else if (move < 0)
            transform.localScale = new Vector3(-2, 2, 2);

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            inPast = !inPast; // Alterna entre pasado y presente
            if (inPast)
            {
                AudioManager.Instance.PlayMusic(AudioManager.Instance.pastMusic);
            }
            else
            {
                AudioManager.Instance.PlayMusic(AudioManager.Instance.presentMusic);
            }
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
            isWalking = false;
        }
    }

    public void Die()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.deathSound);
    }
}

