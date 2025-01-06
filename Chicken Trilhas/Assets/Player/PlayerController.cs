using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputs inputActions;

    public float speed = 2.7f;
    public float jumpForce = 5;

    public GameObject shotPrefab;
    public float shotForce = 10;

    bool canJump = true;
    bool canAttack = true;

    SpriteRenderer sprite;
    Animator animator;
    Rigidbody2D body;

    void Awake()
    {
        inputActions = new PlayerInputs();
    }

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var moveInputs = inputActions.Player_Map.Movement.ReadValue<Vector2>();

        transform.position += Time.deltaTime * new Vector3(moveInputs.x, 0, 0) * speed;

        animator.SetBool("B_Walking", moveInputs.x != 0);

        if(moveInputs.x != 0){
            sprite.flipX = moveInputs.x < 0;
        }
        canJump = Mathf.Abs(body.velocity.y) <= 0.001f;
        HandleJumpAction();
        HandleAttack();
    }

    void HandleJumpAction(){
        var jumpPressed = inputActions.Player_Map.Jump.IsPressed();

        if(canJump && jumpPressed){
            body.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    void HandleAttack(){
        var attackPressed = inputActions.Player_Map.Attack.IsPressed();

        if(canAttack && attackPressed){
            canAttack = false;

            animator.SetTrigger("T_Shot");
        }
    }

    public void ShotNewEgg(){
        var newShot = GameObject.Instantiate(shotPrefab);
            newShot.transform.position = transform.position;
            
        var isLookingRight = !sprite.flipX;
        Vector2 shotDirection = shotForce * new Vector2(isLookingRight ? -1 : 1, 0);
        newShot.GetComponent<Rigidbody2D>().AddForce(shotDirection, ForceMode2D.Impulse);
        newShot.GetComponent<SpriteRenderer>().flipY = !isLookingRight;
    }

    public void SetCanAttack(){
        canAttack = true;
    }
}
