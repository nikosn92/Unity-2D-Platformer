using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    [SerializeField] private string scenetoLoad;
    
     private void OnTriggerEnter2D(Collider2D collision) {
         
     {
        if (collision.gameObject.tag == "Player") 
        {
            SceneManager.LoadScene(scenetoLoad);
        
        }

     }
}

}