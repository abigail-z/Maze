using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node Left { get; set; }
    public Node Right { get; set; }
    public Node Up { get; set; }
    public Node Down { get; set; }
    public bool Visited { get; set; }

    private GameObject leftWall;
    private GameObject rightWall;
    private GameObject topWall;
    private GameObject bottomWall;

    void Awake ()
    {
        leftWall = transform.Find("Left Wall").gameObject;
        rightWall = transform.Find("Right Wall").gameObject;
        topWall = transform.Find("Top Wall").gameObject;
        bottomWall = transform.Find("Bottom Wall").gameObject;
    }

    public void DestroyLeftWall ()
    {
        if (leftWall != null)
        {
            Destroy(leftWall);
            leftWall = null;
        }
    }

    public void DestroyRightWall ()
    {
        if (rightWall != null)
        {
            Destroy(rightWall);
            rightWall = null;
        }
    }

    public void DestroyTopWall ()
    {
        if (topWall != null)
        {
            Destroy(topWall);
            topWall = null;
        }
    }

    public void DestroyBottomWall ()
    {
        if (bottomWall != null)
        {
            Destroy(bottomWall);
            bottomWall = null;
        }
    }
}
