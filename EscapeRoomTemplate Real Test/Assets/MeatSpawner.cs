using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSpawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject Meat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMeat()
    {
        var MeatInstance = Instantiate(Meat,SpawnPoint.position,Quaternion.identity);
        
    }
}
