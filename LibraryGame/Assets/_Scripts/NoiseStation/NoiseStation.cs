using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseStation : MonoBehaviour
{

    public bool isEnabled = false;

    [SerializeField] float increasePerSecond;
    public float noiseLevel;
    [SerializeField] float maxNoiseLevel = 100.0f;


    private bool isCoolingDown = false;
    [SerializeField] float coolDownTime;
    private float coolDownRemaining;
    public bool isBeingShushed = false;

    [SerializeField] int probabilityOfLeaving;
    public NoiseNPC npc;

    [SerializeField] Transform seat;

    private bool hasCheckedToLeave = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (Mathf.Approximately(noiseLevel, 0.0f) && isCoolingDown && !hasCheckedToLeave)
            {
                int randomNum = Random.Range(0, probabilityOfLeaving);
                if (randomNum == 0)
                {
                    npc.LeaveLibrary();
                    npc = null;
                    isEnabled = false;
                    coolDownRemaining = 0.0f;
                    isCoolingDown = false;
                    hasCheckedToLeave = false;
                }
                else
                {
                    hasCheckedToLeave = true;
                }
                //Way to check if the npc should leave the library or not.
                // 1 in x chance
            }
            if (!isBeingShushed && isCoolingDown)
            {
                coolDownRemaining -= Time.deltaTime;
                if (coolDownRemaining <= 0.0f)
                {
                    coolDownRemaining = 0.0f;
                    isCoolingDown = false;
                    hasCheckedToLeave = false;
                }
            }
            else if (!isBeingShushed)
            {
                IncreaseNoise();
            }



        }
    }

    public void DecreaseNoise(float amount)
    {
        if (isEnabled)
        {
            isBeingShushed = true;
            noiseLevel -= amount;

            if (noiseLevel <= 0.0f)
            {
                noiseLevel = 0.0f;
            }

            isCoolingDown = true;
            coolDownRemaining = coolDownTime;

        }
    }

    void IncreaseNoise()
    {
        if (isEnabled)
        {
            noiseLevel += increasePerSecond * Time.deltaTime;

            if (noiseLevel >= maxNoiseLevel)
            {
                noiseLevel = maxNoiseLevel;
            }
        }
    }

    public void GiveNPC(NoiseNPC _npc)
    {
        isEnabled = true;
        npc = _npc;
        npc.gameObject.transform.position = seat.transform.position;
        npc.gameObject.transform.rotation = seat.transform.rotation;
    }

}
