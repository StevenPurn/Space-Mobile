using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour {

    [SerializeField]
    private float fireSpawnTime;
    private float untilFireSpawn;
    public static float currentRotationRate = 0.0f;
    public static float rotationRate = 1.0f;
    public static float maxRotation = 15.0f;
    public static GameObject go;
    public string sucessfulLanding, crashLanding;
    public static float currentSpeed = 5;
    public static float maxSpeed = 10;

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
            SceneManager.LoadScene(sucessfulLanding);
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
            //Set up logic to determine if the player won or not.
            GameOver(true);
        }
        else
        {

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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Rooms room = hit.collider.gameObject.GetComponent<Rooms>();
                if (room.playerInRoom)
                {
                    Debug.Log("change multiplier");
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
        if (true)
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

    public static void SetSpeed(int speed)
    {
        currentSpeed = speed;
    }
}
