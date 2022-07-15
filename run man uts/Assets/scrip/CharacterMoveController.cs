using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("basic Movement")]
    public float moveSpeed;
    public float jumpforse;
    private bool isjumping;
    private float moveHorizontal;
    private float moveVertical;



    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        
    }

    private void OnDrawGizmos()
    {

    }

    private void GameOver()
    {

    }
    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {

            {
                rb2D.AddForce(Vector2.up * jumpforse, ForceMode2D.Impulse);
                
            }
        }
    }
}