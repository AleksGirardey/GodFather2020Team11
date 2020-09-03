using UnityEngine;
using CharacterController = GodFather.CharacterController;

public class Interuptor : MonoBehaviour
{
    public SpriteRenderer spriteDeLaToucheB;

    void Start(){
        spriteDeLaToucheB.enabled = false;
    }

    [SerializeField] public Activable objectToActivate;

    private void LateUpdate(){
        if (CharacterController.Instance.IsInteracting()){
            objectToActivate.Activate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            spriteDeLaToucheB.enabled = true;
            Debug.Log("enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            spriteDeLaToucheB.enabled = false;
            Debug.Log("exit");
        }
    }
}
