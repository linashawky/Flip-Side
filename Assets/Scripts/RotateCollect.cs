﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(100 * Time.deltaTime, 0, 0, Space.Self);
    }
}
