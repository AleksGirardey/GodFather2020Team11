using System;
using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform sprite;
    public Transform colliders;
    
    public float movementSpeed = 4f;
    public float waitBeforeMove = 2f;

    private bool _isGoingDown = true;

    [Header("Platform movement points")]
    public float topPoint;
    public float bottomPoint;

    private float _targetMovementPoint;

    private Sequence _movementSequence;
    
    // [Serializable]
    // public class PlatformPoint {
    //     public float posX;
    //     public float posY;
    // }

    private void Start()
    {
        _targetMovementPoint = bottomPoint;

        StartMovement();
    }

    private void StartMovement() {
        _movementSequence = DOTween.Sequence();
        _movementSequence.Append(transform.DOLocalMoveY(_targetMovementPoint, movementSpeed));
        _movementSequence.PrependInterval(waitBeforeMove);
        _movementSequence.OnComplete(Switch);
    }
    
    private void Switch() {
        if (_isGoingDown)
        {
            _targetMovementPoint = topPoint;
            _isGoingDown = false;
        } else {
            _targetMovementPoint = bottomPoint;
            _isGoingDown = true;
        }
        StartMovement();
    }
    
    private void OnDrawGizmos()
    {
        float gizmosCubeSize = 0.3f;

        Vector3 position = transform.position;
        
        Gizmos.color = Color.yellow;
        Vector3 top = new Vector3(position.x, topPoint, position.z);
        Gizmos.DrawCube(top, Vector3.one * gizmosCubeSize);
        
        Gizmos.color = Color.red;
        Vector3 bottom = new Vector3(position.x, bottomPoint, position.z);
        Gizmos.DrawCube(bottom, Vector3.one * gizmosCubeSize);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(top, bottom);
    }
}