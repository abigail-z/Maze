using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.Find("dude").GetComponent<Animator>();
        agent.speed = transform.localScale.x;
    }

    IEnumerator EnableAgent ()
    {
        agent.enabled = true;
        anim.Play("Walk", -1, 0.0f);
        while (true)
        {
            agent.destination = player.transform.position;
            anim.speed = agent.velocity.magnitude / agent.speed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartPathing ()
    {
        StartCoroutine(EnableAgent());
    }
}
