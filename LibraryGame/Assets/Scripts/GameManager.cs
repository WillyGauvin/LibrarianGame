using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [Header("GameInfo")]
    public float gameScore;

    public float checkInTimer;
    public float checkOutTimer;

    [Space(20)]
    [Header("Books")]
    public List<Book> checkedInBooks = new List<Book>();
    public List<Book> checkedOutBooks = new List<Book>();

    //Books that are not able to be checked in or out.
    //Used to stop NPC's from returning checkedOut books that haven't even left the building yet.
    public List<Book> purgatoryBooks = new List<Book>();

    [Space(20)]
    [Header("NoiseStations")]
    public int NoiseStationsMax;
    public int NoiseStationsEnabled;
    public float TotalNoiseLevel;

    [Space(20)]
    [Header("CheckOutPaths")]
    public GameObject EnterCheckOutPath;
    public GameObject ExitCheckOutPath;

    [Space(20)]
    [Header("CheckInPaths")]
    public GameObject EnterCheckInPath;
    public GameObject ExitCheckInPath;

    [Space(20)]
    [Header("NPC Prefabs")]
    public GameObject CheckOutNPCprefab;
    public GameObject CheckInNPCprefab;


    //public List<NoiseStation> noiseStations = new List<NoiseStation>()

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkOutTimer += Time.deltaTime;
        checkInTimer += Time.deltaTime;

        if (checkOutTimer > 5.0)
        {
            CreateCheckOutEvent();
            checkOutTimer = 0.0f;
        }
        
        if (checkInTimer > 10.0f)
        {
            CreateCheckInEvent();
            checkInTimer = 0.0f;
        }
        

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

        //If the line isn't full
        Path EnterPath = EnterCheckInPath.GetComponent<Path>();
        Path ExitPath = ExitCheckInPath.GetComponent<Path>();
        if (EnterPath.IsPointAtIndexOccupied(0) == false)
        {
            Path Enter = EnterPath.GetComponent<Path>();
            Enter.TogglePointOccupation(0);

            GameObject citizen = Instantiate(CheckInNPCprefab, EnterPath.Points[0].transform.position, Quaternion.identity);


            CheckBookAI AI = citizen.GetComponent<CheckBookAI>();

            AI.EnterPath = EnterPath;
            AI.ExitPath = ExitPath;

            NPC npc = citizen.GetComponent<NPC>();
            npc.gameManager = this;
            npc.CitizenType = NPCType.CheckIn;
            npc.PickupBook(CheckInBook);

            //Remove the book from checkedOut books so other npc's can't also try to return it while this npc attempts to.
            AddToPurgatory(CheckInBook);
        }
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

        Path EnterPath = EnterCheckOutPath.GetComponent<Path>();
        Path ExitPath = ExitCheckOutPath.GetComponent<Path>();

        if (EnterPath.IsPointAtIndexOccupied(0) == false)
        {
            Path Enter = EnterPath.GetComponent<Path>();
            Enter.TogglePointOccupation(0);

            GameObject citizen = Instantiate(CheckOutNPCprefab, EnterPath.Points[0].transform.position, Quaternion.identity);


            CheckBookAI AI = citizen.GetComponent<CheckBookAI>();
            AI.EnterPath = EnterPath;
            AI.ExitPath = ExitPath;
            
            NPC npc = citizen.GetComponent<NPC>();
            npc.gameManager = this;
            npc.CitizenType = NPCType.CheckOut;
            npc.BookRequest = CheckOutBook;

            //Add to purgatory so other NPC's don't request the same book.
            AddToPurgatory(CheckOutBook);
        }
    }

    public void CheckBookIn(Book book)
    {
        Assert.IsFalse(checkedInBooks.Contains(book));

        RemoveFromPurgatory(book);
        checkedInBooks.Add(book);

    }

    public void CheckBookOut(Book book)
    {
        Assert.IsFalse(checkedOutBooks.Contains(book));

        RemoveFromPurgatory(book);
        checkedOutBooks.Add(book);

    }

    public void AddToPurgatory(Book book)
    {
        if (!purgatoryBooks.Contains(book))
        {
            if (checkedOutBooks.Contains(book))
            {
                checkedOutBooks.Remove(book);
            }
            else if (checkedInBooks.Contains(book))
            {
                checkedInBooks.Remove(book);
            }
            purgatoryBooks.Add(book);
        }
        else
        {
            Assert.IsTrue(false);
        }
    }

    public void RemoveFromPurgatory(Book book)
    {
        if (purgatoryBooks.Contains(book))
        {
            purgatoryBooks.Remove(book);
        }
        else
        {
            Assert.IsTrue(false);
        }
    }

    void CreateNoiseEvent()
    {

    }

    void CalculateNoiseLevel()
    {

    }

}
