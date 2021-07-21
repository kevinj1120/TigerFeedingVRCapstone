using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMover : MonoBehaviour
{
    private float distanceFromTarget;
    private Vector3 destination;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        destination = new Vector3(Random.Range(startPos.x + -5, startPos.x + 5), startPos.y, Random.Range(startPos.z + -5, startPos.z + 5));
    }

    // Update is called once per frame
    void Update()
    {
        faceTarget();
        distanceFromTarget = Vector3.Distance(destination, transform.position);
        if (distanceFromTarget < 0.2f)
        {
            destination = new Vector3(Random.Range(startPos.x + -5, startPos.x + 5), startPos.y, Random.Range(startPos.z + -5, startPos.z + 5));
        }
    }

    void faceTarget()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = destination - transform.position;
        // The step size is equal to speed times frame time.
        float singleStep = 1 * Time.deltaTime;
        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        // Draw a ray pointing at our target in
        //Debug.DrawRay(transform.position, newDirection, Color.red);
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
