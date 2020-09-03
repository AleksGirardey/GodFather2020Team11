using UnityEngine;
using CharacterController = GodFather.CharacterController;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D _platformEffector2D;

    public float timeBeforeReset = 0.5f;
    private float _waitTime;
    
    private void Awake() {
        _platformEffector2D = GetComponentInChildren<PlatformEffector2D>();
    }

    private void LateUpdate() {
        if (_waitTime <= 0f) {
            _platformEffector2D.rotationalOffset = 0f;
        } else {
            _waitTime -= Time.deltaTime;
        }

        if (!CharacterController.Instance.HasReleaseFallingFromPlatform()) return;
        
        _platformEffector2D.rotationalOffset = 180f;
        _waitTime = timeBeforeReset;
    }
}