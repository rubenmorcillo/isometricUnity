using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]


public class MotorJugador : MonoBehaviour
{
    NavMeshAgent agenteNav;
    void Start()
    {
        agenteNav = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        
    }

    public void MoverAlPunto(Vector3 punto)
    {
		agenteNav.SetDestination(punto);		
        
    }
}
