using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToLevel : MonoBehaviour
{
    public void ToLevel(){
        SceneManager.LoadScene("Game");
    }
}
