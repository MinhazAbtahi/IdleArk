using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public enum InputType
    {
        Swipe360,
        PointAndClick
    }
    public InputType inputType;
    private PlayerController player;
    public float speed = 6f;
    [Range(0f, 1f)]
    public float movemenetSmoothing = .5f;
    [Range(0f, 1f)]
    public float rotationSmoothing = .25f;
    private Vector3 mouseCurrentPos;
    private Vector3 mouseStartPos;
    private Vector3 moveDirection;
    private Vector3 targetDirection;
    private float currentDragDistance;
    public float maxDragDistance = 10f;

    void Start()
    {
        this.player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!GameManager.instance.startGame) return;
        if (player.isDead) return;

        switch (inputType)
        {
            case InputType.Swipe360:
                HandleSwie360();
                break;
            case InputType.PointAndClick:
                HandlePointAndClick();
                break;
            default:
                break;
        }
    }

    private void HandleSwie360()
    {
        mouseCurrentPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = mouseCurrentPos;
            if (!UIManager.instance.howtoPlayTapped)
            {
                GameManager.instance.startGame = true;
                UIManager.instance.tutorial.SetActive(false);
                UIManager.instance.howtoPlayTapped = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            currentDragDistance = (mouseCurrentPos - mouseStartPos).magnitude;

            if (currentDragDistance > maxDragDistance)
            {
                //mouseStartPos = mouseCurrentPos - moveDirection * maxDragDistance;
            }

            moveDirection = (mouseCurrentPos - mouseStartPos).normalized;
            targetDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
            //transform.position += transform.forward * speed * Time.deltaTime;
            transform.position += targetDirection * speed * Time.deltaTime;
            player.anim.SetBool("Run", true);
            player.SimulateFootsteps();
            if (!player.enemyContact)
            {
                if (targetDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    if (transform.rotation != targetRotation)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing);
                    }
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            player.anim.SetBool("Run", false);
        }
    }

    private void HandlePointAndClick()
    {
        if (Input.GetMouseButtonDown(0)) //&& !GameManager.instance.gameOver)
        {
            if (!UIManager.instance.howtoPlayTapped)
            {
                GameManager.instance.startGame = true;
                UIManager.instance.tutorial.gameObject.SetActive(false);
                UIManager.instance.howtoPlayTapped = true;
            }
            //UIManager.instance.sp.SetActive(false);

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(myRay, out hitInfo, 100, player.clickable))
            {
                player.myAgent.SetDestination(hitInfo.point);
                GameObject tap = Instantiate(GameManager.instance.tapFX, new Vector3(hitInfo.point.x, 1.35f, hitInfo.point.z), Quaternion.identity);
                Destroy(tap, 1f);
                player.ViewTapEffect();
                //transform.DOMove(new Vector3 (hitInfo.point.x,transform.position.y, hitInfo.point.z), 1f);
            }
        }
        if (player.myAgent.velocity.magnitude < 0.00001f)
        {
            player.running = false;
            player.anim.SetBool("run", false);
        }
        else
        {
            player.running = true;
            player.anim.SetBool("run", true);
        }
    }
}