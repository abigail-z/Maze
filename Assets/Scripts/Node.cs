using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node Left { get; set; }
    public Node Right { get; set; }
    public Node Up { get; set; }
    public Node Down { get; set; }

    private GameObject leftWall;
    private GameObject rightWall;
    private GameObject topWall;
    private GameObject bottomWall;

    void Start ()
    {
        leftWall = transform.Find("Left Wall").gameObject;
        rightWall = transform.Find("Right Wall").gameObject;
        topWall = transform.Find("Top Wall").gameObject;
        bottomWall = transform.Find("Bottom Wall").gameObject;
    }

    public void DestroyLeft ()
    {
        Destroy(leftWall);
    }

    public void DestroyRight()
    {
        Destroy(rightWall);
    }

    public void DestroyTop()
    {
        Destroy(topWall);
    }

    public void DestroyBottom()
    {
        Destroy(bottomWall);
    }
}
