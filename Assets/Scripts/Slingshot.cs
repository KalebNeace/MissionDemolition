using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
   [SerializeField] private LineRenderer lineRenderer;
   [SerializeField] private Transform rubberBandStart;
   [SerializeField] private Transform rubberBandEnd;
   [SerializeField] private Configuration configuration;

   // fields set in the Unity Inspector pane
   [Header("Inscribed")]
   public GameObject[] projectilePrefab;
   public float      velocityMult = 10f;
   public GameObject projLinePrefab;
   private int selectedProjectileIndex;
   public float maxStretch = 3.0f;
   private Vector3 initialPosition;
   private Vector3 pullPosition;

   // fields set dynamically
   [Header("Dynamic")] 
   public GameObject launchPoint;
   public Vector3    launchPos;
   public GameObject projctile;
   public bool       aimingMode;
   public AudioClip[] sounds;
   private AudioSource audioSource;
void Start(){
   selectedProjectileIndex = PlayerPrefs.GetInt("SelectedProjectileIndex", 0);
   audioSource = gameObject.AddComponent<AudioSource>();
   audioSource.clip = sounds[selectedProjectileIndex];

   initialPosition = transform.position;
   lineRenderer.positionCount = 2;
   UpdateRubberBand(initialPosition, initialPosition);

}

 void Awake(){
    Transform launchPointTrans = transform.Find("LaunchPoint");
    launchPoint = launchPointTrans.gameObject;
    launchPoint.SetActive(false);
    launchPos = launchPointTrans.position;
 }

 void OnMouseEnter(){
    //print("Slingshot:OnMouseEnter()");
    launchPoint.SetActive(true);
    
 }

 void OnMouseExit(){
    //print("Slingshot:OnMouseExit()");
    launchPoint.SetActive(false);
 }

 void OnMouseDown(){
   // The player has pressed the mouse button whiile over Slingshot
   aimingMode = true;
   // Instantiate a Projectile
   projctile = Instantiate(projectilePrefab[selectedProjectileIndex]) as GameObject;
   // Start it at the launchPoint
   projctile.transform.position = launchPos;
   // Set it to isKinematic for now
   projctile.GetComponent<Rigidbody>().isKinematic = true;
 }

 void Update(){
   if (Input.GetMouseButton(0)) // When mouse button is held down
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pullPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
            float stretch = Vector3.Distance(initialPosition, pullPosition);
            if (stretch > maxStretch)
            {
                pullPosition = initialPosition + (pullPosition - initialPosition).normalized * maxStretch;
            }
            UpdateRubberBand(initialPosition, pullPosition);
        }
   else if (Input.GetMouseButtonUp(0)) // When mouse button is released
        {
            // Implement the logic for launching the projectile
            UpdateRubberBand(initialPosition, initialPosition); // Reset the rubber band
        }
   // If Slingshot is not in aimingMode, don't run this code
   if (!aimingMode) return;

   // Get the current mouse position in 2D screen coordimates
   Vector3 mousePos2D = Input.mousePosition;
   mousePos2D.z = -Camera.main.transform.position.z;
   Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

   // Find the delta from the launchPos to the mousePos3D
   Vector3 mouseDelta = mousePos3D -launchPos;
   // Limit mouseDelta to the radius of the Slingshot SphereCollider
   float maxMagnitude = this.GetComponent<SphereCollider>().radius;
   if(mouseDelta.magnitude > maxMagnitude){
      mouseDelta.Normalize();
      mouseDelta *= maxMagnitude;
   }

   // Move the projectile to this new position
   Vector3 projPos = launchPos + mouseDelta;
   projctile.transform.position = projPos;

   if (Input.GetMouseButtonUp(0)) {
      // The mouse has been released
      aimingMode = false;
      Rigidbody projRB = projctile.GetComponent<Rigidbody>();
      projRB.isKinematic = false;
      projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
      projRB.velocity = -mouseDelta * velocityMult;

      // Switch to slingshot view immediately before setting POI
      FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);

      FollowCam.POI = projctile; // Set the MainCamera POI
      // Add a ProjectileLine to the Projectile
      Instantiate<GameObject>(projLinePrefab, projctile.transform);
      projctile = null;
      MissionDemolition.SHOT_FIRED();
      audioSource.Play();

   }
 }
 void UpdateRubberBand(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }


 Vector3 GetMousePositionInWorld(){
   Vector3 mousePosition = Input.mousePosition;
   mousePosition.z += Camera.main.transform.position.z;
   Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
   //configuration.Radius = 10;
   if (mousePosition.magnitude >  configuration.Radius){
      mousePositionInWorld.Normalize();
      mousePositionInWorld *= configuration.Radius;
   }
   return mousePositionInWorld - transform.position;
 }
}
