using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;

public class AiTargetBehaviour : MonoBehaviour
{

    public AIDestinationSetter targetSetter;
    public AIPath pathfinding;

    public float aggroRadius;
    public CircleCollider2D aggroCollider;

    public Transform[] patrolPoints;
    private List<int> _unusedPatrolPoints = new List<int>();

    public float distanceTresholdToChangeTarget;
    public float patrolTranslationTime;
    public float waitBeforeNextPatrol;

    private int currentTargetPoint;

    [HideInInspector]
    public bool isPatrolling = true;
    private bool playerInRange = false;
    public Sequence _movementSequence;

    //animation
    public Animator anim;

    private Vector3 lastUpdatePos = Vector3.zero;
    [HideInInspector]
    public Vector3 dist;
    [HideInInspector]
    public float currentSpeed;



    // Start is called before the first frame update
    void Start()
    {
        pathfinding.enabled = false;
        //set aggro collider radius as aggroRadius
        aggroCollider.radius = aggroRadius;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            _unusedPatrolPoints.Add(i);
        }

        //set first patrol point and remove it the list for next random
        currentTargetPoint = _unusedPatrolPoints[Random.Range(0, _unusedPatrolPoints.Count)];
        _unusedPatrolPoints.Remove(currentTargetPoint);

        StartMovement();
    }

    private void FixedUpdate()
    {
        if(isPatrolling)
            anim.SetFloat("Speed", Mathf.Abs(currentSpeed));
        else
            anim.SetFloat("Speed", Mathf.Abs(pathfinding.velocity.x));
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            dist = transform.position - lastUpdatePos;
            currentSpeed = dist.magnitude / Time.deltaTime;
            lastUpdatePos = transform.position;
        }

        //if AI is returning from aggro, check if near target point and start patrol again
        if (!isPatrolling && !playerInRange)
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[currentTargetPoint].position);
            if (distance < distanceTresholdToChangeTarget)
            {
                isPatrolling = true;
                SetNewPatrolPoint();

                pathfinding.enabled = false;
                StartMovement();
            }
        }
    }

    private void StartMovement()
    {
        _movementSequence = DOTween.Sequence();
        _movementSequence.Append(transform.DOLocalMove(patrolPoints[currentTargetPoint].position, patrolTranslationTime));
        _movementSequence.PrependInterval(waitBeforeNextPatrol);
        _movementSequence.OnComplete(Switch);
    }

    private void Switch()
    {
        if (isPatrolling)
        {
            SetNewPatrolPoint();
        }
        StartMovement();
    }

    private void SetNewPatrolPoint()
    {
        int ancientPatrolPoint = currentTargetPoint;
        currentTargetPoint = _unusedPatrolPoints[Random.Range(0, _unusedPatrolPoints.Count)];
        _unusedPatrolPoints.Remove(currentTargetPoint);
        _unusedPatrolPoints.Add(ancientPatrolPoint);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //going toward player, not patrolling anymore
            isPatrolling = false;
            playerInRange = true;
            pathfinding.enabled = true;
            targetSetter.target = other.transform;
            //kill current patrol sequence
            _movementSequence.Kill();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //not in patrol, but player is not in aggro range, returning to last patrol point
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
