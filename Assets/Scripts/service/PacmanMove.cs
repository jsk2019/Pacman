using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    private Vector2 dest = Vector2.zero;
    public float speed = 0.35f;



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
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
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
