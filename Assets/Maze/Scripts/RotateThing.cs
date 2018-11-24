using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThing : MonoBehaviour
{
    public Vector3 rotation;
    public float bobFrequency;
    public float bobDistance;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(rotation * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.y = Mathf.Sin(Time.time * bobFrequency) * bobDistance + startY;
        transform.position = pos;
    }
}
