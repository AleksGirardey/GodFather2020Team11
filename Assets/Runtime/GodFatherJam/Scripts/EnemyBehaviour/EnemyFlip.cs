using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    public AIPath aiPath;
    private SpriteRenderer _sr;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            _sr.flipX = false;
        }
        else if (aiPath.desiredVelocity.y <= -0.01f)
        {
            _sr.flipX = true;
        }
    }
}
