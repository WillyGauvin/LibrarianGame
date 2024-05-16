using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoiseNPC : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public NoiseStation noiseStation;
    public Transform exit;
    private bool isLeaving = false;

    // Update is called once per frame

    private void Start()
    {
        agent.SetDestination(noiseStation.transform.position);
    }
    private void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    noiseStation.isEnabled = true;
                    noiseStation.npc = this;
                }
            }
        }

        if (isLeaving)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

    }

    public void LeaveLibrary()
    {
        agent.SetDestination(exit.position);
        isLeaving = true;
    }

}
