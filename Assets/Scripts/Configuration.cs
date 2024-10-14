using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Configuration")]

public class Configuration : ScriptableObject
{
  [field:SerializeField] public int Radius {get; private set;} = 4;
  [field:SerializeField] public GameObject ball {get; private set;}
}
