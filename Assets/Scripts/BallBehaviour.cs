using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public AudioSource sound;
    private Rigidbody rb;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        StartCoroutine(DespawnTimer());
	}
	
	IEnumerator DespawnTimer ()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.IncreaseScore();
            Destroy(gameObject);
        }

        sound.Play();
    }

    public void Throw (Vector3 startPos, Vector3 direction, Vector3 inheritVelocity)
    {
        transform.position = startPos;
        rb.velocity = direction.normalized * speed + inheritVelocity;
    }
}
