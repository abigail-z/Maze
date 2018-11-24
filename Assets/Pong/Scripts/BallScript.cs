using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public PaddleMovementScript leftPaddle;
    public PaddleMovementScript rightPaddle;
    public float startSpeed;
    public float speedupAmount;
    private Rigidbody2D rb;
    private Vector2 startPos;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        StartBall();
	}

    public void StartBall ()
    {
        transform.position = startPos;
        float xDirection = Random.value < .5 ? 1 : -1;
        float yDirection = Random.value - 0.5f;
        rb.velocity = new Vector2(xDirection, yDirection) * startSpeed;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("LeftPaddle"))
        {
            leftPaddle.Following = false;
            PaddleBounce(collision);
        }
        else if (collision.collider.CompareTag("RightPaddle"))
        {
            rightPaddle.Following = false;
            PaddleBounce(collision);
        }
    }

    void PaddleBounce (Collision2D collision)
    {
        Vector2 velocity = rb.velocity;
        velocity.y += collision.collider.attachedRigidbody.velocity.y / 2;
        velocity.y = Mathf.Clamp(velocity.y, -50, 50);
        velocity.x *= speedupAmount;
        rb.velocity = velocity;

    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("LeftCourt"))
        {
            leftPaddle.Following = true;
        }
        else if (collision.CompareTag("RightCourt"))
        {
            rightPaddle.Following = true;
        }
        else if (collision.CompareTag("LeftGoal"))
        {
            gameManager.RightScores();
            StartBall();
        }
        else if (collision.CompareTag("RightGoal"))
        {
            gameManager.LeftScores();
            StartBall();
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("LeftCourt"))
        {
            leftPaddle.Following = false;
        }
        else if (collision.CompareTag("RightCourt"))
        {
            rightPaddle.Following = false;
        }
    }
}
