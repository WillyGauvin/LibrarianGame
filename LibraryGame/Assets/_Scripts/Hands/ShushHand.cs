using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class ShushHand : MonoBehaviour
{
    [SerializeField] float decreasePerSecond;

    [SerializeField] Camera cam;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode shushKey;

    [SerializeField] GameObject shushText_gameObject;


    public float playerReach;

    // Start is called before the first frame update

    private void Start()
    {
        shushText_gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, playerReach))
        {
            Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();

            if (hitInfo.collider.GetComponent<NoiseNPC>())
            {
                NoiseNPC noiseNPC = hitInfo.collider.GetComponent<NoiseNPC>();

                if (noiseNPC.noiseStation.isEnabled)
                {
                    shushText_gameObject.SetActive(true);

                    //Enabled text that tells the player they can hold left mouse to shush.
                    if (Input.GetKey(shushKey))
                    {
                        noiseNPC.noiseStation.DecreaseNoise(Time.deltaTime * decreasePerSecond);
                        shushText_gameObject.SetActive(false);
                    }
                    else
                    {
                        noiseNPC.noiseStation.isBeingShushed = false;
                    }
                }
            }
            else
            {
                shushText_gameObject.SetActive(false);
            }
        }
        else
        {
            shushText_gameObject.SetActive(false);
        }
    }

    public void SwitchedHands()
    {
        shushText_gameObject.SetActive(false);
    }

}
