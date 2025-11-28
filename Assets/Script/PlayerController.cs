using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 1;
    public Vector2 moveVector;
    public Rigidbody2D rb;
    public Animator anim;
    public float speed = 2f;
    public float jumpForce = 5f;
    private string scene;
    public bool faceRight = true;
    public LayerMask groundLayer;        // слой пола дл€ проверки на касание
    public Transform groundCheck;        // точка проверки касани€ с землей
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    private int coins = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene().name;

    }

    void Update()
    {
        Walk();
        CheckGround();
        Reflect();
        Jump();
    }

    void Walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");// ѕолучаем горизонтальное значение оси (например, стрелки или A/D)
        rb.linearVelocity = new Vector2(moveVector.x * speed, rb.linearVelocity.y); // ќбновл€ем физическую скорость по оси X
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));  // ќбновл€ем анимацию. abs знак "минус" у отрицательных чисел, дела€ их положительными, а положительные числа оставл€€ без изменений
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
        }

    }


    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("isGrounded", isGrounded);

    }



    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coins++;
            //coinsText.text = "Coins: " + coins.ToString();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("DeathZone"))
        {
            Die();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("—толкновение с врагом!");
            Die();
        }



    }

    private void Die()
    {
        if (health > 0)
        {

            health--;

            //Debug.Log("»грок умер. ќсталось жизней: " + health);

            RestartGame();
        }
    }

    private void RestartGame()
    {
        // «агружаем ту же сцену, котора€ активна в данный момент.
        SceneManager.LoadScene(scene);
    }
}