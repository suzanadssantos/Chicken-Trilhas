using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContactHandle : MonoBehaviour
{
    public Image itemImage;

    bool canWinLevel = false;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Tocou!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Item")){
            Debug.Log("Pegou!");
            Destroy(other.gameObject);
            itemImage.color = Color.white;
            canWinLevel = true;
        }

        if(other.gameObject.CompareTag("FinalPoint")){
            if(canWinLevel){
                Debug.Log("Ganhou");
            }
        }
    }
}
