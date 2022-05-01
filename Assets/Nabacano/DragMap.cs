﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMap : MonoBehaviour
{
    private float dist;
    private Vector3 MouseStart;
    private Vector3 derp;

    //void Start()
    //{
    //    dist = transform.position.z;  // Distance camera is above map
    //}

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dist = transform.position.z;  // Distance camera is above map
            MouseStart = new Vector3(-Input.mousePosition.x, -Input.mousePosition.y, dist); // ili umesto minusa ispred x i y stavi se samo minus ispred dist (mada nisam siguran da ce to da radi 100%)
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;

        }
        else if (Input.GetMouseButton(1))
        {
            var MouseMove = new Vector3(-Input.mousePosition.x, -Input.mousePosition.y, dist);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.z = transform.position.z;
            transform.position = transform.position - (MouseMove - MouseStart);
        }

        if (Input.GetMouseButtonDown(2))
        {
            dist = transform.position.z;  // Distance camera is above map
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;

        }
        else if (Input.GetMouseButton(2))
        {
            var MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.z = transform.position.z;
            transform.position = transform.position - (MouseMove - MouseStart);
        }
    }
}
