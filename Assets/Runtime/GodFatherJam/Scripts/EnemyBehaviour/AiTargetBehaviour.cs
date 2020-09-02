using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AiTargetBehaviour : MonoBehaviour
{
    public float aggroRadius;
    public float patrolRadius;

    public Transform[] patrolPoints;

    private Vector2 _startPosition;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        //aggro circle
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, aggroRadius);

        //patrol circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, patrolRadius);
    }
}
