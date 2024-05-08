using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    public GameObject[] Points;
    bool[] IsPointOccupied;
    private int pointsIndex;


    // Start is called before the first frame update
    void Start()
    {

        IsPointOccupied = new bool[Points.Length];

        for (int i = 0; i < IsPointOccupied.Length; i++)
        {
            IsPointOccupied[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (pointsIndex <= Points.Length - 1)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed* Time.deltaTime);

        //    if (transform.position == Points[pointsIndex].transform.position)
        //    {
        //        pointsIndex++; 
        //    }
        //}
    }

    public bool IsPointAtIndexOccupied(int index)
    {
        if (IsPointOccupied[index])
        {
            return true;
        }
        return false;
    }
    public void TogglePointOccupation(int index)
    {
        IsPointOccupied[index] = !IsPointOccupied[index];
    }
}
