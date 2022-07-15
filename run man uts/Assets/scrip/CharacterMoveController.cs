using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("basic Movement")]
    public float moveSpeed;
    public float jumpforse;
    public float maxSpeed;
    public float jumpAmount;

    private bool isjumping;
    private float moveHorizontal;
    private float moveVertical;

    private bool isOnGround;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    [Header("Scoring")]
    public ScoreController score;
    public float scoringRatio;
    private float lastPositionX;

    [Header("GameOver")]
    public float fallPositionY;
    public GameObject gameOverScreen;

    [Header("Camera")]
    public CameraMoveController gameCamera;

    private Animator anim;
    private CharacterSoundController sound;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();

    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down *
        groundRaycastDistance), Color.white);
    }

    private void GameOver()
    {
        // set high score
        score.FinishScoring();
        // stop camera movement
        gameCamera.enabled = false;
        // show gameover
        gameOverScreen.SetActive(true);
        // disable this too
        this.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        // change animation
        anim.SetBool("isOnGround", isOnGround);

        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);
        if (scoreIncrement > 0)
        {
            score.IncreaseCurrentScore(scoreIncrement);
            lastPositionX += distancePassed;
        }
        if (transform.position.y < fallPositionY)
        {
            GameOver();
        }
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
                sound.PlayJump();
            }
        }

        // raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,
        groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if (!isOnGround && rb2D.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }
        // calculate velocity vector
        Vector2 velocityVector = rb2D.velocity;
        if (isjumping)
        {
            velocityVector.y += jumpforse;
            isjumping = false;
        }

    }
}