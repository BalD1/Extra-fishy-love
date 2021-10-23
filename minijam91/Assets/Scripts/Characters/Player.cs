using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [Space]
    [Header("Player Related")]
    [SerializeField] private GameObject arm;
    [SerializeField] private SpriteRenderer secondArm;
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private GameObject seaWeed;
    [SerializeField] private GameObject weaponHandler;
    [SerializeField] private GameObject currentWeapon;
    [SerializeField] private SpriteRenderer currentWeaponRenderer;
    private int currentWeaponIndex = 0;

    [SerializeField] private List<GameObject> unlockedWeapons;


    private int playerSpriteOrder;

    [Header("Misc")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject interactionButton;

    private bool hasSeaweed;
    public bool HasSeaWeed { 
        get => hasSeaweed;
        set
        {
            seaWeed.SetActive(value);
            hasSeaweed = value;
            //
        }
    }

    private float xDirection;
    private Vector2 direction;
    private Vector3 mousePosition;
    private Vector3 selfPosByCam;
    private Vector3 basePos;

    private void Start()
    {
        basePos = this.transform.position;
        if (mainCamera == null)
            mainCamera = Camera.main;

        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            GameObject weapon = Instantiate(unlockedWeapons[i], weaponHandler.transform);
            if (i != 0)
                weapon.SetActive(false);
        }
        currentWeaponIndex = 0;
        currentWeapon = weaponHandler.transform.GetChild(currentWeaponIndex).gameObject;
        currentWeapon.SetActive(true);

        if (this.currentWeaponRenderer == null)
            this.currentWeaponRenderer = this.currentWeapon.GetComponentInChildren<SpriteRenderer>();

        playerSpriteOrder = this.sprite.sortingOrder;
        CallStart();
        _Death += PlayerDeath;
        _TookDamages += DamagesTaken;
    }


    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
        {

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

            if (Input.GetKeyDown(KeyCode.E))
                NextWeapon();
            if (Input.GetKeyDown(KeyCode.Q))
                PreviousWeapon();
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

    private void NextWeapon()
    {
        if (currentWeaponIndex == unlockedWeapons.Count - 1)
            ChangeWeapon(0);
        else
        {
            for (int i = 0; i < unlockedWeapons.Count; i++)
            {
                if (i == (currentWeaponIndex + 1))
                {
                    ChangeWeapon(i);
                    return;
                }
            }
        }
    }

    private void PreviousWeapon()
    {
        if (currentWeaponIndex == 0)
            ChangeWeapon(unlockedWeapons.Count - 1);
        else
        {
            for (int i = unlockedWeapons.Count - 1; i >= 0; i--)
            {
                if (i == (currentWeaponIndex - 1))
                {
                    ChangeWeapon(i);
                    return;
                }
            }
        }
    }

    private void ChangeWeapon(int index)
    {
        currentWeapon.SetActive(false);

        currentWeaponIndex = index;
        currentWeapon = weaponHandler.transform.GetChild(index).gameObject;
        currentWeapon.SetActive(true);
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
            secondArm.sortingOrder = playerSpriteOrder - 1;
            currentWeaponRenderer.sortingOrder = playerSpriteOrder + 1;
            Vector2 armScale = arm.transform.localScale;
            armScale.y = 1;
            arm.transform.localScale = armScale;
        }
        else
        {
            sprite.flipX = true;
            armRenderer.sortingOrder = playerSpriteOrder - 1;
            secondArm.sortingOrder = playerSpriteOrder + 1;
            currentWeaponRenderer.sortingOrder = playerSpriteOrder - 2;
            Vector2 armScale = arm.transform.localScale;
            armScale.y = -1;
            arm.transform.localScale = armScale;
        }
    }

    #endregion

    private void DamagesTaken()
    {
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.player_hurt);
    }

    private void PlayerDeath()
    {
        UIManager.Instance.gameOverText = "GAME OVER \n YOU DIED";
        GameManager.Instance.GameState = GameManager.GameStates.Gameover;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Seaweed"))
        {
            if(!HasSeaWeed)
                interactionButton.SetActive(true);
            else
                interactionButton.SetActive(false);
            if (Input.GetKeyDown(KeyCode.E))
            {
                HasSeaWeed = true;
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
