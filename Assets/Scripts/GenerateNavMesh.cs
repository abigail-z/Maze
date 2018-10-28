using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateNavMesh : MonoBehaviour
{
    private NavMeshSurface surface;

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        StartCoroutine(BuildMesh());
    }

    IEnumerator BuildMesh ()
    {
        yield return null;
        surface.BuildNavMesh();

        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.StartPathing();
        }
    }
}
