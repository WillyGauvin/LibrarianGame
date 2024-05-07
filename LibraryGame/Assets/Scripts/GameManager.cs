using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public float gameScore;
    public int timeElapsed;

    public List<Book> checkedInBooks = new List<Book>();
    public List<Book> checkedOutBooks = new List<Book>();

    public int NoiseStationsMax;
    public int NoiseStationsEnabled;
    public float TotalNoiseLevel;

    //public List<NoiseStation> noiseStations = new List<NoiseStation>()



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateCheckInEvent()
    {
        if (checkedOutBooks.Count < 1)
        {
            return;
        }

        int randomIndex;

        randomIndex = Random.Range(0, checkedOutBooks.Count - 1);

        Book CheckInBook = checkedOutBooks[randomIndex];

        //Spawn NPC with this random book request.
    }

    void CreateCheckOutEvent()
    {
        if (checkedInBooks.Count < 1)
        {
            return;
        }
        int randomIndex;

        randomIndex = Random.Range(0, checkedInBooks.Count - 1);

        Book CheckOutBook = checkedInBooks[randomIndex];

        //Spawn NPC with this random book request.
    }

    public void CheckBookIn(Book book)
    {
        Assert.IsTrue(checkedOutBooks.Contains(book));
        Assert.IsFalse(checkedInBooks.Contains(book));

        checkedOutBooks.Remove(book);
        checkedInBooks.Add(book);

    }

    public void CheckBookOut(Book book)
    {
        Assert.IsTrue(checkedInBooks.Contains(book));
        Assert.IsFalse(checkedOutBooks.Contains(book));

        checkedInBooks.Remove(book);
        checkedOutBooks.Add(book);

    }

    void CreateNoiseEvent()
    {

    }

    void CalculateNoiseLevel()
    {

    }

}
