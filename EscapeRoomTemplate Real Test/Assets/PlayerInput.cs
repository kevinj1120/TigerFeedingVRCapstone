using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public UnityEvent buttonpressed;
    public bool isReadyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A) && isReadyToSpawn)
        {
            buttonpressed?.Invoke();
            isReadyToSpawn = false;
            StartCoroutine(WaitToSpawn());
        }
    }

    IEnumerator WaitToSpawn()
    {
        //wait
        yield return new WaitForSeconds(1f);

        //Do something
        isReadyToSpawn = true;
    }
    
}
