using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerGrabbable : OVRGrabbable
{
    public Rigidbody drawerRb;
    public Transform handle;
    public float releaseValue;

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(Vector3.zero, Vector3.zero);
        
        //your code here
    }

    protected override void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
