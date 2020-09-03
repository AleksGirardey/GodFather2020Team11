using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    private float lightReductionOnCollision;
    [SerializeField]
    private Light2D pointLight;
    [SerializeField]
    private CircleCollider2D lightCollider;

    private float startRadius;

    private void Start()
    {
        startRadius = pointLight.pointLightOuterRadius;
        lightCollider.radius = startRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            lightCollider.radius -= lightReductionOnCollision;
            pointLight.pointLightOuterRadius -= lightReductionOnCollision;
            if (lightCollider.radius < 0)
            {
                lightCollider.radius = 0;
                lightCollider.enabled = false;
            }
            if (pointLight.pointLightOuterRadius < 0)
                pointLight.pointLightOuterRadius = 0;
        }
    }

    public void ResetLightAndColldierValues()
    {
        lightCollider.radius = startRadius;
        pointLight.pointLightOuterRadius = startRadius;
    }

    public bool IsOverloaded() {
        return false;
    }
}
