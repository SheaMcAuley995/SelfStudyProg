using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour {

    public enum CurrentAttractState { PlayerSelected, ForceSelected, HauntedSelected, NoneSeleceted }
    public CurrentAttractState attractState = CurrentAttractState.NoneSeleceted;
    public Transform grabTransform;
    public float moveToSpeed = 2;
    public Rigidbody rb;
    Transform moveTo;
	void Start () {
        this.tag = "Important Item";
        this.gameObject.layer = 9;
        rb = GetComponent<Rigidbody>();
        if(attractState != null)
        {
            attractState = CurrentAttractState.NoneSeleceted;
        }
	}
	
	// Update is called once per frame
	void Update () {

        switch(attractState)
        {
            case CurrentAttractState.PlayerSelected:
                moveTo = grabTransform;
                rb.useGravity = false;
                rb.velocity = new Vector3(moveTo.position.x * moveToSpeed - transform.position.x * moveToSpeed, moveTo.position.y * moveToSpeed - transform.position.y * moveToSpeed, moveTo.position.z * moveToSpeed - transform.position.z * moveToSpeed)/ rb.mass;
                break;
            case CurrentAttractState.ForceSelected:
                break;
            case CurrentAttractState.HauntedSelected:
                break;
            case CurrentAttractState.NoneSeleceted:
                moveTo = null;
                rb.useGravity = true;
                break;
            default:
                moveTo = null;
                rb.useGravity = true;
                break;


        }
       
       //Debug.Log(Vector3.Magnitude(moveTo.position - transform.position));
	}

   // IEnumerable haunted
}
