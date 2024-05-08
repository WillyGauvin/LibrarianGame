using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [Header("GameInfo")]
    public float gameScore;
    public float timeElapsed;

    [Space(20)]
    [Header("Books")]
    public List<Book> checkedInBooks = new List<Book>();
    public List<Book> checkedOutBooks = new List<Book>();

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
        timeElapsed += Time.deltaTime;

        if (timeElapsed > 2.0f)
        {
            CreateCheckOutEvent();
            timeElapsed = 0.0f;
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
            GameObject citizen = Instantiate(CheckOutNPCprefab, EnterPath.Points[0].transform.position, Quaternion.identity);
            CheckBookAI AI = citizen.GetComponent<CheckBookAI>();

            AI.EnterPath = EnterPath;
            AI.ExitPath = ExitPath;

            NPC npc = citizen.GetComponent<NPC>();
            npc.CitizenType = NPCType.CheckIn;
            npc.PickupBook(CheckInBook);
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
            GameObject citizen = Instantiate(CheckOutNPCprefab, EnterPath.Points[0].transform.position, Quaternion.identity);
            CheckBookAI AI = citizen.GetComponent<CheckBookAI>();
            AI.EnterPath = EnterPath;
            AI.ExitPath = ExitPath;
            
            NPC npc = citizen.GetComponent<NPC>();
            npc.CitizenType = NPCType.CheckOut;
            npc.BookRequest = CheckOutBook;
        }
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
