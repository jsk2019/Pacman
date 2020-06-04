using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    private Vector2 dest = Vector2.zero;
    public float speed = 0.35f;

    public static bool faceUp = false;
    public static bool faceDown = false;
    public static bool faceRight = false;
    public static bool faceLeft = false;


    private void Start()
    {

        dest = transform.position;
    }

    private void FixedUpdate()
    {

        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);

        GetComponent<Rigidbody2D>().MovePosition(temp);

        if ((Vector2)transform.position == dest)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;
                faceUp = true;
                faceRight = false;
                faceLeft = false;
                faceDown = false;


            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
                faceUp = false;
                faceRight = false;
                faceLeft = false;
                faceDown = true;

            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
                faceUp = false;
                faceRight = false;
                faceLeft = true;
                faceDown = false;

            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
                faceUp = false;
                faceRight = true;
                faceLeft = false;
                faceDown = false;
            }
                Vector2 dir = dest - (Vector2)transform.position;

                GetComponent<Animator>().SetFloat("DirX", dir.x);

                GetComponent<Animator>().SetFloat("DirY", dir.y);
            }

    }
    private bool Valid(Vector2 dir)
    {
        
        Vector2 pos = transform.position;
        
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
      
        return (hit.collider == GetComponent<Collider2D>());
    }
}
