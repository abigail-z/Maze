using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnableAgent());
	}

    IEnumerator EnableAgent ()
    {
        yield return null;
        agent.enabled = true;
        while (true)
        {
            agent.destination = player.transform.position;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
