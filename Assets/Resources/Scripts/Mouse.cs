using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Camera Camera;
    public Vector3 WorldPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.ScreenToWorldPoint(Input.mousePosition);
        WorldPoint = Camera.WorldToScreenPoint(transform.position);
    }
}
