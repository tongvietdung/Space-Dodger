using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

// For mobile build change the mouse click to touch
public class MovementControll : MonoBehaviour
{
    float rotateAngle;

    public float floatSpeed;

    public Rigidbody2D rb;
    public ParticleSystem burstParticle;
    public AudioSource burstAudio;
    public AudioSource deathAudio;
    public Animator animator;

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetMouseButtonDown(0))//Input.touchCount > 0
        {
            animator.SetTrigger("MoveActive");

            //Get first touch information 
            //Touch touch = Input.GetTouch(0);

            // Get the position of touch in world cordinate (in unit)
            //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Make z = 0f cause in 2d there is no z axis;
            //touchPosition.z = 0f;
            clickPos.z = 0f;

            // Get the move direction
            Vector3 moveDirection = clickPos - transform.position; //touchPosition - transform.position;
            moveDirection = moveDirection.normalized;

            // Get the rotation angle
            rotateAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

            // Rotate and add force to move to the touch position.
            transform.rotation = Quaternion.AngleAxis(-rotateAngle, Vector3.forward);

            // Get the distance between player and touch 
            int distance = (int)Mathf.Round(moveDirection.magnitude);
            BurstSmoke(distance);

            // Player floats !!!
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * (floatSpeed + distance * 2) * Time.deltaTime;
        }
    }
    void BurstSmoke(int distance)
    {

        // Play Audio
        burstAudio.Play();

        // Play Particle System
        burstParticle.Emit(distance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        deathAudio.Play();
        StartCoroutine(waitForAudio(3));
    }

    IEnumerator waitForAudio(int levelIndex)
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.43f);
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }
}
