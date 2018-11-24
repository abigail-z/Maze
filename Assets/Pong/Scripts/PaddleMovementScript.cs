using System.Collections;
using UnityEngine;

public class PaddleMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public float AIReturnSpeed;
    public float yConstraint;
    public string inputAxis;
    public float inputCheckPeriod;
    public float waitTime;
    public GameObject toFollow;
    public bool Following { get; set; }
    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool inputMade = false;
    private bool playerControlled = false;
    private bool waiting = true;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        StartCoroutine(CheckInput());
    }

    // note that this is NOT FixedUpdate
    // physics works normally
    // fixes jittering issue caused by pushing against walls
    void Update ()
    {
        // if player has made an input, move the paddle
        float input = Input.GetAxisRaw(inputAxis);
        if (input != 0)
        {
            inputMade = true;
            playerControlled = true;
        }

        // if the player control flag is false, use 'AI'
        if (playerControlled)
        {
            float movementVelocity = input * moveSpeed;
            rb.velocity = new Vector2(0, movementVelocity);

        }
        else
        {
            float direction;
            if (Following)
            {
                direction = Mathf.Clamp(toFollow.transform.position.y - transform.position.y, -1, 1);
            }
            else
            {
                direction = Mathf.Clamp(startPos.y - transform.position.y, -AIReturnSpeed, AIReturnSpeed);
            }
            rb.velocity = new Vector2(0, direction * moveSpeed);
        }

        // keep the paddle's position within the constraint
        Vector2 position = transform.position;
        if (position.y - startPos.y > yConstraint)
        {
            position.y = yConstraint;
        }
        else if (position.y - startPos.y < -yConstraint)
        {
            position.y = -yConstraint;
        }
        transform.position = position;
    }

    // checks in a set period whether the player has made an input
    IEnumerator CheckInput ()
    {
        Coroutine coroutine = null;
        while (true)
        {
            if (inputMade)
            {
                inputMade = false;
                waiting = false;
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
            else if (!waiting)
            {
                waiting = true;
                coroutine = StartCoroutine(WaitForInput());
            }
            yield return new WaitForSeconds(inputCheckPeriod);
        }
    }

    // if the player has not made an input, wait for a time before enabling the 'AI'
    IEnumerator WaitForInput ()
    {
#if UNITY_EDITOR
        Debug.Log(inputAxis + " is waiting for input");
#endif
        yield return new WaitForSeconds(waitTime);
#if UNITY_EDITOR
        Debug.Log(inputAxis + " is now AI controlled");
#endif
        playerControlled = false;
    }
}
