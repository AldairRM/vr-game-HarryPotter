using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alvo : MonoBehaviour
{

    //BEGIN C#    
    //VARIABLES
    public Transform originObject;
    public Transform lookingCameraTransform;
    [Range(0f, 1f)]
    //public float sensitivity = 0.4f;
    public float sensitivity = 0.15f;
    Vector3 forwardVectorTowardsCamera;
    bool cameraLooking = false;
    float dotProductResult;

    private void Start()
    {
        gameObject.tag = "Untagged";
        this.lookingCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        originObject = this.gameObject.transform;
    }

    void FixedUpdate()
    {
        //EXECUTE THE NEXT FUNCTION INSIDE UPDATE
        CheckIfCameraIsLooking();
    }

    //FUNCTIONS
    public void CheckIfCameraIsLooking()
    {

        forwardVectorTowardsCamera = (lookingCameraTransform.position - originObject.position).normalized;
        dotProductResult = Vector3.Dot(lookingCameraTransform.forward, forwardVectorTowardsCamera);
        if (cameraLooking)
        {
            if (dotProductResult > sensitivity || checkRayCast() == false)
            {
                cameraLooking = false;
                StartNotLooking();

            }
        }
        else
        {
            if (dotProductResult < -sensitivity)
            {
                cameraLooking = checkRayCast();
                if(cameraLooking)
                    StartLooking();
            }
        }
        if (cameraLooking)
        {
            PlayerIsLooking();
        }
        else
        {
            PlayerIsNotLooking();

        }
    }

    void StartLooking()
    {
        //Debug.Log("Camera starts looking");
        gameObject.tag = "Alvo";

    }
    void PlayerIsLooking()
    {
        //Debug.Log("Camera is currently looking");

    }

    void StartNotLooking()
    {

        gameObject.tag = "Untagged";
        //Debug.Log("Camera stops looking");
    }

    void PlayerIsNotLooking()
    {
        //Debug.Log("Camera is currently not looking");
    }
    //C# ENDS

    bool checkRayCast() 
    {
        RaycastHit hit;
        if (Physics.Raycast(lookingCameraTransform.position, originObject.position - lookingCameraTransform.position, out hit, Mathf.Infinity))
        {
            // Check if the hit object is the target object
            if (hit.collider.gameObject == this.gameObject)
            {
                return true; // Object is visible, not occluded by other objects
            }
            else
            {
                return false; // Object is occluded by another object
            }
        }
        else 
        {
            return false;
        }

    }
}