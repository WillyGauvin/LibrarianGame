using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoiseNPC : MonoBehaviour
{
    public GameManager gameManager;

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

        if (isLeaving)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        gameManager.deoccupyStation(noiseStation);
                        Destroy(gameObject);
                    }
                }
            }
        }

        else if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    noiseStation.GiveNPC(this);
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
