using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    public bool isFlying;

    [Space]


    public AIPath aiPath;
    public Rigidbody2D rb;
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

    //activate pathfinding when velocity is close to zero after being pushed away
    private IEnumerator WaitUntilVelocityGoesTo0()
    {
        yield return new WaitUntil(() => (rb.velocity.x <= 0.5f && rb.velocity.y <= 0.5f));
        rb.velocity = Vector2.zero;
        aiPath.canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("light"))
        {
            //ejected
            LightShieldBehaviour light = other.GetComponent<LightShieldBehaviour>();

            //case when enemy is not flying and light is not overloaded
            if (!light.IsOverloaded && !isFlying)
            {
                aiPath.canMove = false;
                Vector2 direction = (transform.position - other.transform.position).normalized;
                rb.AddForce(direction * light.NormalLightEjectionForce, ForceMode2D.Impulse);
                StartCoroutine(WaitUntilVelocityGoesTo0());

            }
            //overload
            else if (light.IsOverloaded)
            {
                aiPath.canMove = false;
                Vector2 direction = (transform.position - other.transform.position).normalized;
                float distanceToPlayer = light.OverloadLightRadius - Vector2.Distance(other.transform.position, transform.position);
                rb.AddForce(direction * distanceToPlayer * light.OverloadEjectionForce, ForceMode2D.Impulse);
                //when ejection velocity goes under x velocity, reactivate pathfinding
                StartCoroutine(WaitUntilVelocityGoesTo0());
            }
        }
    }
}
