using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 1;
    public GameObject target;
    public Animator anim;
    public float playerPosition;
    public float targetPosition;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInput();
    }

    //Listen for new taps
    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Rooms room = hit.collider.GetComponent<Rooms>();
                if (hit.collider.GetComponent<Rooms>() != null)
                {
                    room = hit.collider.GetComponent<Rooms>();
                }
                else
                {
                    room = hit.collider.GetComponentInParent<Rooms>();
                }

                target = room.roomPosition;
            }
        }
    }

    //Move the player
    void FixedUpdate()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,-2), new Vector3(target.transform.position.x, gameObject.transform.position.y, -2), step);
            AnimationController.SetAnimation("Walking");

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
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * 250 * Time.deltaTime);
    }

    //Inform rooms that player has arrived in the room & the active position
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rooms")
        {
            other.GetComponent<Rooms>().playerInRoom = true;
        }
        else if (other.tag == "Position")
        {
            other.GetComponentInParent<Rooms>().playerInPosition = true;
        }
    }

    //Inform rooms when player leaves the room & the active position
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Rooms")
        {
            other.GetComponent<Rooms>().playerInRoom = false;
        }
        else if(other.tag == "Position")
        {
            other.GetComponentInParent<Rooms>().playerInPosition = false;
        }
    }
}
