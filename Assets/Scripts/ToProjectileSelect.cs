using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToProjectileSelect : MonoBehaviour
{
    public void ToProjectileSelector(){
        SceneManager.LoadScene("ProjectileSelector");
    }
}