using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public GameObject gameController;
    public int speed = 1;
    public GameObject target;
    public Animator anim;
    public float playerPosition;
    public float targetPosition;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController");
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,-2), new Vector3(target.transform.position.x, gameObject.transform.position.y, -2), step);
            anim.SetBool("Walking",true);
            anim.SetBool("MedBayHealing", false);
            anim.SetBool("CockpitNav", false);
            anim.SetBool("Idle", false);
            anim.SetBool("EngineControl", false);
            anim.SetBool("FireFighting", false);

            if (transform.position.x < target.transform.position.x)
            {
                Vector3 theScale = transform.localScale;
                theScale.x = -1;
                transform.localScale = theScale;
            }
            else
            {
                Vector3 theScale = transform.localScale;
                theScale.x = 1;
                transform.localScale = theScale;
            }

            if(transform.position.x == target.transform.position.x)
            {
                target = null;
                anim.SetBool("Walking", false);
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            anim.SetBool("Walking", false);
        }

        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * 250 * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rooms")
        {
            gameController.GetComponent<RoomTracking>().updateRoom(other.gameObject.name.ToString());
        }
        else if (other.tag == "Position")
        {
            switch (other.gameObject.name)
            {
                case "MedBayPosition":
                    GameObject.Find("MedBay").GetComponent<MedBayScript>().inPosition = true;
                    anim.SetBool("Walking", false);
                    break;
                case "EnginePosition":
                    GameObject.Find("Engine").GetComponent<EngineScript>().inPosition = true;
                    anim.SetBool("Walking", false);
                    break;
                case "CockpitPosition":
                    GameObject.Find("Cockpit").GetComponent<CockpitScript>().inPosition = true;
                    anim.SetBool("Walking", false);
                    break;
                case "EmptyPosition":
                    GameObject.Find("Empty").GetComponent<EmptyScript>().inPosition = true;
                    anim.SetBool("Walking", false);
                    break;
                default:
                    Debug.Log("something");
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Position")
        {
            GameObject.Find("MedBay").GetComponent<MedBayScript>().inPosition = false;
            GameObject.Find("Cockpit").GetComponent<CockpitScript>().inPosition = false;
            GameObject.Find("Engine").GetComponent<EngineScript>().inPosition = false;
            GameObject.Find("Empty").GetComponent<EmptyScript>().inPosition = false;
        }
    }
}
