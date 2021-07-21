using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    public CustomGrabber leftHand;
    public CustomGrabber rightHand;
    public GameObject grabbedObject;
    public GameObject cube;
    
    public List<FollowPlayer> tigers = new List<FollowPlayer>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private bool CheckForMeatGrab()
    {
        //this updates and looks for grabbing of the meat
        //returns true if grabbing meat.
        return false;
    }
    public void EnableTigerSearch()
    {
        if (tigers.Count <= 0)
            return;

        foreach(FollowPlayer tiger in tigers)
        {
            tiger.EnableFollow();
        }
    }

    public void DisableTigerSearch()
    {
        if (tigers.Count <= 0)
            return;

        foreach (FollowPlayer tiger in tigers)
        {
            tiger.DisableFollow();
        }
    }

    private void SetTigersToFollow()
    {
        //go through each tiger in list.
        //check if distance is close enough
        //if yes, set to follow.
    }
}
