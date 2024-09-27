using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
    [SerializeField] private Transform firstPoint;
    [SerializeField] private  Transform secondPoint;

    private LineRenderer      band;

    // Start is called before the first frame update
    void Start()
    {
        band = GetComponent<LineRenderer>();
        band.SetPosition(0, firstPoint.position);
        band.SetPosition(2, secondPoint.position);
        
   }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
