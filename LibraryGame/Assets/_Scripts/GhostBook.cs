using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBook : MonoBehaviour
{
    [SerializeField] Material ghostBookMaterial;

    public bool placable = false;

    private Color redalpha = new Color(1, 0, 0, 0.5f);
    private Color greenalpha = new Color(0, 1, 0, 0.5f);

    private void OnTriggerEnter(Collider other)
    {
        placable = false;
        ghostBookMaterial.color = redalpha;
    }

    private void OnTriggerExit(Collider other)
    {
        placable = true;
        ghostBookMaterial.color = greenalpha;
    }
}
