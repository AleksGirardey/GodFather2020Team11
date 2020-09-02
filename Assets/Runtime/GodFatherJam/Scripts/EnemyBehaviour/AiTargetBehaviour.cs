using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Pathfinding;

public class AiTargetBehaviour : MonoBehaviour
{
    public AIDestinationSetter targetSetter;
    public AIPath pathfinding;

    public float aggroRadius;
    public CircleCollider2D aggroCollider;

    public Transform[] patrolPoints;
    public float distanceTresholdToChangeTarget;

    private int currentTargetPoint;

    private Vector2 _startPosition;
    private bool isPatrolling = true;
    private bool playerInRange = false;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        currentTargetPoint = Random.Range(0, patrolPoints.Length);
        pathfinding.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[currentTargetPoint].position);
            if (distance < distanceTresholdToChangeTarget)
                currentTargetPoint = Random.Range(0, patrolPoints.Length);
            pathfinding.enabled = false;
        }
        else if (!isPatrolling && !playerInRange)
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[currentTargetPoint].position);
            if (distance < distanceTresholdToChangeTarget)
            {
                isPatrolling = true;
                currentTargetPoint = Random.Range(0, patrolPoints.Length - 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPatrolling = false;
            playerInRange = true;
            pathfinding.enabled = true;
            targetSetter.target = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            targetSetter.target = patrolPoints[currentTargetPoint];
        }
    }

    void OnDrawGizmosSelected()
    {
        //aggro circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
