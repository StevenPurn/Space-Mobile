using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipTracking : MonoBehaviour {

    [SerializeField]
    private float fireSpawnTime;
    private float untilFireSpawn;
    public static float currentRotationRate = 0.0f;
    public static float rotationRate = 2.0f;
    public static float maxRotation = 15.0f;
    public static GameObject go;
    public Scene sucessfulLanding, crashLanding;

    private static float currAltitude;
    private static float startingAltitude = 5000f;

    [SerializeField]
    private float fireSpawnChance = 0.5f;

	// Use this for initialization
	void Start () {
        untilFireSpawn = fireSpawnTime;
        go = gameObject;
        currAltitude = startingAltitude;
    }
	
    public void GameOver(bool playerWon)
    {
        if (playerWon)
        {

        }
    }

	// Update is called once per frame
	void Update () {
        CheckForFireSpawn();
        MonitorAltitude();
    }

    private void MonitorAltitude()
    {
        if(currAltitude <= 0)
        {
            GameOver(true);
        }
    }

    private void CheckForFireSpawn()
    {
        untilFireSpawn -= Time.deltaTime;

        if (untilFireSpawn <= 0)
        {
            untilFireSpawn = fireSpawnTime;
            if (Random.Range(0, 1f) < fireSpawnChance)
            {
                FireStarter();
            }
        }
    }

    public void FireStarter()
    {
        int randomSeed = Random.Range(0, GameControl.rooms.Count);
        GameControl.rooms[randomSeed].SpawnFire();
    }

    public void ClickTracker()
    {
        //raycast from mouse and check which room it collides with
        //also check inROOM varibales to determine if its the same room 
        //check time from last click to determine if the double function
        //should be called
        //maybe just count clicks between timer if all are in the same room
        //reset count if a new room is clicked

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Rooms room = hit.collider.gameObject.GetComponent<Rooms>();
                if (room.playerInRoom)
                {
                    Debug.Log("Do stuff");
                }
                else
                {
                    ResetClickCounter();
                }
            }
        }
    }

    public void ResetClickCounter()
    {

    }

    public void ClickTimingTracker()
    {
        //Get rid of this once implemented
        float clickTimerActual = 5.0f;
        clickTimerActual -= Time.deltaTime;
        int clicksInRoom = 0;

        if(clickTimerActual <= 0)
        {
            Rooms curRoom = null;
            foreach (var room in GameControl.rooms)
            {
                if (room.playerInPosition)
                {
                    curRoom = room;
                    break;
                }
            }
            int amount = 1;
            if (clicksInRoom > 4)
            {
                amount = 2;
            }

            curRoom.ActivateAction(amount);
            ResetClickCounter();
        }
    }

    public static void RotateShip(bool rotationControlled, int multiplier)
    {
        if (true) //!landedOrDead)
        {
            if (rotationControlled)
            {
                currentRotationRate -= 2 * multiplier * Time.deltaTime;
            }
            else
            {
                currentRotationRate += rotationRate * Time.deltaTime;
            }

            currentRotationRate = Mathf.Clamp(currentRotationRate, -maxRotation, maxRotation);

            go.transform.rotation = Quaternion.Euler(0, 0, currentRotationRate);
        }
    }
}
