using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private CharacterController controller;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        controller.SimpleMove(direction * speed);
	}
}
