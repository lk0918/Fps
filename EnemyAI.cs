using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float raza = 6f;
    [SerializeField] float razaDePierdere = 10f;
    NavMeshAgent navMeshAgent;
    float distantalaTel = Mathf.Infinity;
    bool esteProvocat = false; 
    void Start()
    {
         
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        distantalaTel = Vector3.Distance(target.position, transform.position);
        if (esteProvocat)
        {
            inAtentieTelul();
        } else if (distantalaTel <= raza)
           
        {
            esteProvocat = true;     
        }
      
      
    }

    private void inAtentieTelul()
    {
        if  (distantalaTel >= navMeshAgent.stoppingDistance) {
            UrmaresteTelul();
        
        }


        if (distantalaTel <= navMeshAgent.stoppingDistance) {
                AtacaTelul();
            }

        else if (distantalaTel > razaDePierdere)
        {
            esteProvocat = false;
       //     Debug.Log(name + " la praebit pe " + target.name + " din vizor " );
        }
    }

    private void UrmaresteTelul()
    {
        navMeshAgent.SetDestination(target.position);
     //   Debug.Log(name + " l-o spalit pe " + target.name + " si merge sa-i dea in ebala ");
    }

    private void AtacaTelul()
    {
   //     Debug.Log(name + " ii da in ebala lu " + target.name );
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raza);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, razaDePierdere);
    }
    
}
