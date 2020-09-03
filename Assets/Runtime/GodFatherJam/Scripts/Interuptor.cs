using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interuptor : MonoBehaviour
{
    public SpriteRenderer spriteDeLaToucheB;

    void Start(){
        spriteDeLaToucheB.enabled = false;
    }


    void Update(){
    }

    public Door doortoactivate;

    private void LateUpdate(){
        if (CharacterController.Instance.IsInteracting()){
            doortoactivate.OpenDoor();
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
