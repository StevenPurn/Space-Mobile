using UnityEngine;

public class Engine : Rooms
{
    public GameObject malfunctionWarning;
    public bool hasMalfunction;

    [SerializeField]
    private float malfunctionTimer, malfunctionTime;
    [SerializeField]
    private float chanceOfMalfunction;

    public override void Init()
    {
        malfunctionWarning.SetActive(false);
        malfunctionTimer = malfunctionTime;
    }

    public override void RoomAction(int multiplier)
    {
        AnimationController.SetAnimation("EngineControl");
        EngineAction(multiplier);
    }

    public override void RoomUpdate()
    {
        if (hasMalfunction == false)
        {
            malfunctionTimer -= Time.deltaTime;

            if (malfunctionTimer <= 0)
            {
                malfunctionTimer = malfunctionTime;
                CheckMalfunctionSpawn();
            }
        }
        else
        {
            malfunctionTimer = malfunctionTime;
        }
    }

    private void CheckMalfunctionSpawn()
    {
        float roll = Random.Range(0f, 1f);
        if(roll <= chanceOfMalfunction)
        {
            SpawnMalfunction();
        }
    }

    private void SpawnMalfunction()
    {
        malfunctionWarning.SetActive(true);
        hasMalfunction = true;
        Ship.SetSpeed(Random.Range(0,10));
    }

    void EngineAction(int multiplier)
    {
        //Set ship speed
        malfunctionWarning.SetActive(false);
        hasMalfunction = false;
    }
}