using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
   public AudioSource checkpointSound;

   // A static field accessible by code anywhere
   static public bool goalMet = false;
   void OnTriggerEnter(Collider other){
    // When the trigger is hit by something
    // Check to see if it's a Projectile
    Projectile proj = other.GetComponent<Projectile>();  
    if (proj != null){
        // If so, set goalMet to true
        checkpointSound.Play();
        Goal.goalMet = true;
    }
 
  }
}
