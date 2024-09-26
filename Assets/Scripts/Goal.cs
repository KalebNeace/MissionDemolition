using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
   // A static field accessible by code anywhere
   static public bool goalMet = false;
   void OnTriggerEnter(Collider other){
    // When the trigger is hit by something
    // Check to see if it's a Projectile
    Projectile proj = other.GetComponent<Projectile>();  
    if (proj != null){
        // If so, set goalMet to true
        Goal.goalMet = true;
    }
 
  }
}
