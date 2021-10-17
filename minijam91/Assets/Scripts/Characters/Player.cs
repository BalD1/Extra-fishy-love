using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [Space]
    [Header("Player Related")]
    [SerializeField] private GameObject arm;
    [SerializeField] private SpriteRenderer currentWeaponRenderer;
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private GameObject weapon;

    private int playerSpriteOrder;

    [Header("Misc")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject interactionButton;

    private bool hasSeaweed;
    public bool HasSeaWeed { 
        get => hasSeaweed;
        set
        {
            hasSeaweed = value;
            //
        }
    }

    private float xDirection;
    private Vector2 direction;
    private Vector3 mousePosition;
    private Vector3 selfPosByCam;

    private void Start()
    {
        if(mainCamera == null)
            mainCamera = Camera.main;

        playerSpriteOrder = this.sprite.sortingOrder;
        CallStart();
        _Death += PlayerDeath;
        _TookDamages += DamagesTaken;
    }


    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Pause();

            if(Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow))
                Jump();

            GetMousePosition();

            if (interactionButton.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HasSeaWeed = true;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
            GameObject.FindObjectOfType<Fishtank>().TakeDamages(1);
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {
            Movements();
        }
    }

    #region Movements

    private void Jump()
    {
        body.velocity = Vector2.up * characterStats.jumpSpeed;
    }

    private void Movements()
    {
        xDirection = Input.GetAxis("Horizontal");
        direction.x = xDirection; 
        direction.y = this.body.velocity.y;

        Translate(direction);
    }

    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        selfPosByCam = mainCamera.WorldToScreenPoint(this.transform.position);

        mousePosition.x -= selfPosByCam.x;
        mousePosition.y -= selfPosByCam.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if(angle > -90f && angle < 90f)
        {
            sprite.flipX = false;
            armRenderer.sortingOrder = playerSpriteOrder + 2;
            currentWeaponRenderer.sortingOrder = playerSpriteOrder + 1;
            Vector2 armScale = arm.transform.localScale;
            armScale.y = 1;
            arm.transform.localScale = armScale;

        }
        else
        {
            sprite.flipX = true;
            armRenderer.sortingOrder = playerSpriteOrder - 1;
            currentWeaponRenderer.sortingOrder = playerSpriteOrder - 2;
            Vector2 armScale = arm.transform.localScale;
            armScale.y = -1;
            arm.transform.localScale = armScale;
        }
    }

    #endregion

    private void Pause()
    {
        if(GameManager.Instance.GameState == GameManager.GameStates.InGame)
            GameManager.Instance.GameState = GameManager.GameStates.Pause;
        else if(GameManager.Instance.GameState == GameManager.GameStates.Pause)
        {
            if(UIManager.Instance.OptionsMenu.activeSelf)
                UIManager.Instance.OptionsMenu.SetActive(false);
            else
                GameManager.Instance.GameState = GameManager.GameStates.InGame;
        }
    }

    private void DamagesTaken()
    {
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.player_hurt);
    }

    private void PlayerDeath()
    {
        GameManager.Instance.GameState = GameManager.GameStates.Gameover;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Seaweed"))
        {
            interactionButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                HasSeaWeed = true;
                Debug.Log("hasweed");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Seaweed"))
        {
            interactionButton.SetActive(false);
        }
    }
}
