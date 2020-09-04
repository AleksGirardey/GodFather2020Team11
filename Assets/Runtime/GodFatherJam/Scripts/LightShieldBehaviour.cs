using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightShieldBehaviour : MonoBehaviour
{
    [SerializeField] private int _numberOfHitShieldCanTakeMax;
    [SerializeField] private Light2D _pointLight;
    [SerializeField] private CircleCollider2D _lightCollider;
    [SerializeField] private BoxCollider2D _killCollider;
    [SerializeField] private float lightRadius;
    public float NormalLightEjectionForce;
    private int _numberOfHitShieldCanTake;


    [Header("Overload")]
    public int numberOfOverloadCharges;
    public float OverloadLightRadius;
    [SerializeField] private float OverloadTimeToGrow;
    [SerializeField] private float OverloadTimeOverloaded;
    [SerializeField] private float OverloadLightIntensity;
    public float OverloadEjectionForce;
    public bool IsOverloaded = false;

    private float currentOverloadGrowthTime = 0;
    private float currentLightRadius;
    private float normalLightIntensity;
    private bool _canBeKilled;

    public AudioSource activateVines;
    public PlayerController playerController;
    private void Start()
    {
        _numberOfHitShieldCanTake = _numberOfHitShieldCanTakeMax;
        _pointLight.pointLightOuterRadius = lightRadius;
        _lightCollider.radius = lightRadius;
        currentLightRadius = lightRadius;
        normalLightIntensity = _pointLight.intensity;
    }

    public void Overload()
    {
        if (numberOfOverloadCharges == 0) return;

        IsOverloaded = true;
        numberOfOverloadCharges -= 1;
        currentLightRadius = _lightCollider.radius;


        StartCoroutine(OverloadGrowth());
    }

    private IEnumerator OverloadGrowth()
    {
        //grow
        while (_lightCollider.radius < OverloadLightRadius)
        {
            currentOverloadGrowthTime += Time.deltaTime;
            float ratio = currentOverloadGrowthTime / OverloadTimeToGrow;
            _pointLight.pointLightOuterRadius = Mathf.Lerp(currentLightRadius, OverloadLightRadius, ratio);
            _pointLight.intensity = Mathf.Lerp(normalLightIntensity, OverloadLightIntensity, ratio);
            _lightCollider.radius = _pointLight.pointLightOuterRadius;
            yield return null;
        }

        currentOverloadGrowthTime =0;
        yield return new WaitForSeconds(OverloadTimeOverloaded);

            //shrink back
        while (_lightCollider.radius > currentLightRadius)
        {
            currentOverloadGrowthTime += Time.deltaTime;
            float ratio = currentOverloadGrowthTime / OverloadTimeToGrow;
            _pointLight.pointLightOuterRadius = Mathf.Lerp(OverloadLightRadius, currentLightRadius, ratio);
            _pointLight.intensity = Mathf.Lerp(OverloadLightIntensity, normalLightIntensity, ratio);
            _lightCollider.radius = _pointLight.pointLightOuterRadius;
            yield return null;
        }
        currentOverloadGrowthTime = 0;
        IsOverloaded = false;
    }

    public void HitPlayer(AudioSource MonsterHitAudio)
    {
        if (!IsOverloaded)
        {
            if (_numberOfHitShieldCanTake > 0)
            {
                if(MonsterHitAudio != null && !MonsterHitAudio.isPlaying)
                    MonsterHitAudio.Play();
                //shrink light radius and collider radius
                _lightCollider.radius -= _lightCollider.radius / _numberOfHitShieldCanTake;
                _pointLight.pointLightOuterRadius = _lightCollider.radius;
                _numberOfHitShieldCanTake -= 1;
            }
        }

        if (_canBeKilled)
        {
            playerController.ReSpawn();
            return;
        }

        //deactivate collider and light if no radius left
        if (_numberOfHitShieldCanTake <= 0)
        {
            _lightCollider.radius = 0;
            _pointLight.pointLightOuterRadius = 0;
            _numberOfHitShieldCanTake = 0;
            _killCollider.enabled = true;
            _canBeKilled = true;
        }
    }



    public void AddChargesToOverload(int chargesToAdd)
    {
        numberOfOverloadCharges += chargesToAdd;
    }

    //respawn light reset
    public void ResetLightAndColliderValues()
    {
        _lightCollider.radius = lightRadius;
        _pointLight.pointLightOuterRadius = lightRadius;
        _numberOfHitShieldCanTake = _numberOfHitShieldCanTakeMax;
        _lightCollider.enabled = true;
        _pointLight.enabled = true;
        _canBeKilled = false;
        _killCollider.enabled = false;
        numberOfOverloadCharges = playerController.GetLastCheckpoint().GetOverloadChargesToRefill();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lightRadius);   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, OverloadLightRadius);
    }
}
