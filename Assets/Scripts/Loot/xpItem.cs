using UnityEngine;
using System;

public class xpItem : MonoBehaviour
{
    private float experience;

    [SerializeField] private Sprite lowXPSprite;
    [SerializeField] private Sprite mediumXPSprite;
    [SerializeField] private Sprite highXPSprite;
    [SerializeField] private float mediumXPThreshold = 200f;
    [SerializeField] private float highXPThreshold = 2000f;
    private playerXP playerXP;
    private Action<xpItem> onPickup;

    private ScoreManager scoreManager;

    public bool isPickedUp = false;

    private playerStats playerStats;

    [SerializeField] private AudioClip[] itemPickup;

    private float lastPickupTime;

    private float pickupSoundCooldown = 0.2f;


    void Start()
    {
        playerXP = playerXP.instance;
        scoreManager = ScoreManager.instance;
        playerStats = playerXP.GetComponent<playerStats>();
        lastPickupTime = 0;
    }

    void OnEnable()
    {
        isPickedUp = false;
    }

    void FixedUpdate()
    {

        Transform playerPosition = playerXP.transform;

        if (isPickedUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, Time.deltaTime);
        }
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            PlayOnPickup();

            float gainedXP = experience * playerStats.growth;

            playerXP.experience += gainedXP;
            onPickup(this);
            
            scoreManager.AddToScore(gainedXP);
        }
    }

    public void SetExperience(float xp)
    {
        var spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        
        experience = xp;

        if (xp < mediumXPThreshold)
        {
            spriteRenderer.sprite = lowXPSprite;
        }
        else if (xp < highXPThreshold)
        {
            spriteRenderer.sprite = mediumXPSprite;
        }
        else 
        {
            spriteRenderer.sprite = highXPSprite;
        }

    }

    public void OnPickUp(Action<xpItem> pickupAction)
    {
        onPickup = pickupAction;
    }

    private void PlayOnPickup()
    {
        float diff = Time.time - lastPickupTime;

        if (diff < pickupSoundCooldown) { return; }

        if (SoundFXManager.Instance == null)
        {
            Debug.LogWarning("SoundFXManager not found in scene.");
            return;
        }
        else
        {
            SoundFXManager.Instance.PlayRandomAudioClipsNoRandPitch(itemPickup, transform, 1f);
            lastPickupTime = Time.time;
        }
    }

}
