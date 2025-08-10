using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeAgentDestination : MonoBehaviour
{
    public Vector3 destination;

    public NavMeshAgent target;

    private void Awake()
    {
        target = GetComponent<NavMeshAgent>();
    }

    public void Call ()
    {
        target.SetDestination(destination);
    }
}
