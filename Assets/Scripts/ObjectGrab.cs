using System;
using UnityEngine;
using UnityEngine.UI;

public enum CurrentController { CameraController, TransformController, CustomController}

public class ObjectGrab : MonoBehaviour {

    [Serializable]
    public class RaycastSettings
    {

        [Tooltip("The transform position of where the raycast that detects interacible objects.")]
        public Transform raycastTransform;

        [Tooltip("Detection range of the raycast.")]
        public float raycastDistance;

        [Space]
        [Tooltip("Layer used for interacible objects.")]
        public LayerMask objectLayer;
        
    }


    [SerializeField]
    CurrentController currentController;



    [Tooltip("Only used if you are using a Custom Transform.")]
    [SerializeField] Transform customTransform;

    public Transform holdPos;
    [SerializeField] float throwForce = 0;

    [Space]

    [SerializeField]
    RaycastSettings raySettings = new RaycastSettings();

    [Space]

    [SerializeField] Image crossHair;

    MoveableObject m_obj = null;

    //Audio Things
    public AudioClip Clip;
    private AudioSource source;

    private void Awake()
    {
        switch(currentController)
        {
            case CurrentController.CameraController:
                raySettings.raycastTransform = Camera.main.transform;
                break;

            case CurrentController.TransformController:
                raySettings.raycastTransform = GetComponent<Transform>();
                break;

            case CurrentController.CustomController:
                raySettings.raycastTransform = customTransform;
                break;

            default:
                if(raySettings.raycastTransform == null)
                {
                    raySettings.raycastTransform = GetComponent<Transform>();
                }
                break;
        }

        source = GetComponent<AudioSource>();

    }

    private void Update()
    {
        RaycastHit hit;
        //holdPos = raySettings.raycastTransform.InverseTransformPoint(holdPos);
        if (Input.GetMouseButtonDown(0))
       {
            grabObject();
       }

       if(Input.GetMouseButtonDown(1))
        {
            if(m_obj != null)
            {
                source.PlayOneShot(Clip);
                throwObject();
            }
        }

       if(Input.GetMouseButtonUp(0))
       {
            if(m_obj != null)
            {
                releaseObject();
            }
            crossHair.color = Color.black;
            Debug.DrawRay(raySettings.raycastTransform.position, raySettings.raycastTransform.TransformDirection(Vector3.forward) * raySettings.raycastDistance, Color.white);
            //Debug.Log("Currently Not Hitting");
        }
    }

    private void grabObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(raySettings.raycastTransform.position, raySettings.raycastTransform.TransformDirection(Vector3.forward), out hit, raySettings.raycastDistance, raySettings.objectLayer))
        {
            if ((hit.collider.GetComponent("MoveableObject") as MoveableObject) != null)
            {
               crossHair.color = Color.red;
               if(m_obj != null)
                {
                    m_obj.attractState = MoveableObject.CurrentAttractState.NoneSeleceted;
                    m_obj = hit.collider.GetComponent("MoveableObject") as MoveableObject;
                    m_obj.attractState = MoveableObject.CurrentAttractState.PlayerSelected;
                    m_obj.grabTransform = holdPos;
                }
               else
                {
                    m_obj = hit.collider.GetComponent("MoveableObject") as MoveableObject;
                    m_obj.attractState = MoveableObject.CurrentAttractState.PlayerSelected;
                    m_obj.grabTransform = holdPos;
                }

            }
            

        }
        else
        {
            crossHair.color = Color.black;
        }
    }

    private void releaseObject()
    { 
        m_obj.attractState = MoveableObject.CurrentAttractState.NoneSeleceted;
        m_obj = null;
    }

    private void throwObject()
    {

        m_obj.rb.AddForce(holdPos.forward * throwForce, ForceMode.Impulse);
        m_obj.attractState = MoveableObject.CurrentAttractState.NoneSeleceted;
        m_obj = null;
    }
}
