using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private Rigidbody rb;
    private AudioSource sound;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
	}

    void OnEnable()
    {
        rb.velocity = Vector3.zero;
        StartCoroutine(DespawnTimer());
    }

    IEnumerator DespawnTimer ()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        sound.Play();

        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.IncreaseScore();
            Destroy(gameObject);
        }
    }

    public void Throw (Vector3 startPos, Vector3 direction, Vector3 inheritVelocity)
    {
        transform.position = startPos;
        rb.velocity = direction.normalized * speed + inheritVelocity;
    }
}
