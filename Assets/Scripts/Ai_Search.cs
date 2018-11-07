using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_Search : MonoBehaviour{


    public float searchTime;
    Animator animator;
    NavMeshAgent nav;

    private void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(searching());
    }

    private void Update()
    {
        float speedPrecent = nav.velocity.magnitude / nav.speed;
        animator.SetFloat("SpeedPercent", speedPrecent, .1f, Time.deltaTime);
    }
    IEnumerator searching()
    {
        
        while (true)
        {
            if (nav.remainingDistance < 10)
            {
                yield return new WaitForSeconds(searchTime);
                nav.SetDestination(PositionFinder.Instance.giveNewPosition());
               
            }


          //  Debug.Log(nav.remainingDistance.ToString());
            yield return null;
        }
    }

}
