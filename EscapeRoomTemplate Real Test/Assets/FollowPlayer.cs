using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsWithinBounds())
            agent.destination = player.transform.position;

    }

    private float GetDistanceToTarget(Transform target)
    {
        distanceFromPlayer = Vector3.Distance(transform.position, target.position);
        return distanceFromPlayer;
    }

    public bool IsWithinBounds()
    {
        if (GetDistanceToTarget(player) < distanceThreshold && isFollowing)
        {
            return true;
        }

        return false;
    }

    public void EnableFollow()
    {
        isFollowing = true;
    }

    public void DisableFollow()
    {
        isFollowing = false;
    }
}
