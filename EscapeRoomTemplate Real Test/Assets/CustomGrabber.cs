using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGrabber : OVRGrabber
{
    public UnityEvent checkGrabbedObject;
    public UnityEvent letGoOfObject;
    public BehaviourManager behaviourManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void GrabBegin()
    {
        base.GrabBegin();
        behaviourManager.grabbedObject = grabbedObject.gameObject;
        checkGrabbedObject?.Invoke();
    }

    protected override void GrabEnd()
    {
        base.GrabEnd();
        letGoOfObject?.Invoke();
    }
}
