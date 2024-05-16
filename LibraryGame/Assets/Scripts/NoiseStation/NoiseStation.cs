using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseStation : MonoBehaviour
{

    public bool isEnabled = false;

    [SerializeField] float increasePerSecond;
    public float noiseLevel;


    private bool isCoolingDown = false;
    [SerializeField] float coolDownTime;
    [SerializeField] float coolDownRemaining;
    public bool isBeingShushed = false;

    public NoiseNPC npc;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (Mathf.Approximately(noiseLevel, 0.0f) && isCoolingDown) 
            {
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
        }
    }


}
