using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float BulletSpeed = 20f;
    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
           // Vector3 m_mousePosition = Input.mousePosition;
           // m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
           // m_mousePosition.z = 0;
           // float m_fireAngle = Vector2.Angle(m_mousePosition - this.transform.position, Vector2.up)-90 ;
           //if (m_mousePosition.x > this.transform.position.x)
           //{
           //   m_fireAngle = -m_fireAngle;
           // }

            GameObject m_bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            if (PacmanMove.faceUp)
            {
                m_bullet.GetComponent<Rigidbody2D>().velocity = ((Vector2.up).normalized * BulletSpeed);
            }
            else if (PacmanMove.faceDown)
            {
                m_bullet.GetComponent<Rigidbody2D>().velocity = ((Vector2.down).normalized * BulletSpeed);
            }
            else if (PacmanMove.faceLeft)
            {
                m_bullet.GetComponent<Rigidbody2D>().velocity = ((Vector2.left).normalized * BulletSpeed);
            }
            else if (PacmanMove.faceRight)
            {
                m_bullet.GetComponent<Rigidbody2D>().velocity = ((Vector2.right).normalized * BulletSpeed);
            }
            //   m_bullet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);
        }
		
	}

    void Shoot()
    {

    }
}
