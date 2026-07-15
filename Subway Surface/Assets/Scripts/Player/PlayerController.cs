using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] skinModels;
    [SerializeField] Animator moveAnimator;
    Animator modelAnimator;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float rollResetTime;
    [SerializeField] private Collider standingCollider;
    [SerializeField] private Collider rollingCollider;

    [Header("Sounds")]
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private AudioClip slideLeftSound;
    [SerializeField] private AudioClip slideRightSound;
    [SerializeField] private AudioClip slideUpSound;
    [SerializeField] private AudioClip slideDownSound;
    [SerializeField] private AudioClip fallSound;
    [SerializeField] private AudioClip walk1Sound;
    [SerializeField] private AudioClip walk2Sound;

    [Header("Lines")]
    [SerializeField] Transform[] linePositions;
    [SerializeField] float laneChangeSpeed = 10f;

    [Header("FOV")]
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] float fovWhileRunning;
    [SerializeField] float fovWhileSprinting;

    public int currentLine = 1;
    public bool isRolling = false;
    private float rollTimer = 0;

    private bool isGrounded = false;
    private bool isDead = false;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
        
        SkinData skinData = new SkinData();
        SaveLoadManager.singleton.Load(skinData, SaveLoadManager.singleton.skinDataFileName);
        foreach (GameObject model in skinModels)
            model.SetActive(false);
        skinModels[skinData.selectedSkin].SetActive(true);
        modelAnimator = skinModels[skinData.selectedSkin].GetComponent<Animator>();
    }

    private void FixedUpdate()
    { 
        if (isDead) return;
    
        if (isRolling)
        {
            rollTimer += Time.deltaTime;
            if (rollTimer >= rollResetTime) ResetRoll();
        }

        isGrounded = Physics.Raycast(
            groundCheckTransform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );

        Vector3 target = new Vector3(
            targetPosition.x,
            rb.position.y,
            rb.position.z);

        Vector3 newPosition = Vector3.MoveTowards(
            rb.position,
            target,
            laneChangeSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
        modelAnimator.SetBool("IsGrounded", isGrounded);
        bool isFalling = !isGrounded && rb.linearVelocity.y < -0.1f;
        modelAnimator.SetBool("IsFalling", isFalling);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 3)
        {
            Die();
        }
    }

    public void TryToMoveRight()
    {
        if (isDead) return;

        if (currentLine > 0)
        {
            currentLine--;

            targetPosition = new Vector3(
                linePositions[currentLine].position.x,
                transform.position.y,
                transform.position.z);

            moveAnimator.SetTrigger("MoveRight");
            SoundManager.singleton.PlaySoundEffect(slideRightSound);
        }
    }

    public void TryToMoveLeft()
    {
        if (isDead) return;

        if (currentLine < linePositions.Length - 1)
        {
            currentLine++;

            targetPosition = new Vector3(
                linePositions[currentLine].position.x,
                transform.position.y,
                transform.position.z);

            moveAnimator.SetTrigger("MoveLeft");
            SoundManager.singleton.PlaySoundEffect(slideLeftSound);
        }
    }

    public void TryToJump()
    {
        if (isDead) return;

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            modelAnimator.SetBool("IsGrounded", isGrounded);
            SoundManager.singleton.PlaySoundEffect(slideUpSound);
            if (isRolling) ResetRoll();
        }
    }

    public void TryToRoll()
    {
        if (isDead) return;

        if (isRolling == false && isGrounded)
        {
            isRolling = true;
            modelAnimator.SetTrigger("Roll");
            SoundManager.singleton.PlaySoundEffect(slideDownSound);
            rollingCollider.enabled = true;
            standingCollider.enabled = false;
        }
    }

    public void TryToSprint()
    {
        if (isDead || isRolling || !isGrounded) return;
        // play spirint animation
        modelAnimator.SetBool("Sprinting", true);
        // increase speed
        ChunkManager.singleton.SprintSpeed();
        // fov
        StartCoroutine(ChangeToSprintFov());
    }

    public void ResetSprint()
    {
        // play run animation
        modelAnimator.SetBool("Sprinting", false);
        // decrease speed
        ChunkManager.singleton.RunSpeed();
        // fov
        StartCoroutine(ChangeToRunFov());
    }

    private void ResetRoll()
    {
        isRolling = false;
        rollTimer = 0;
        rollingCollider.enabled = false;
        standingCollider.enabled = true;
    }

    public void Die()
    {
        isDead = true;
        modelAnimator.SetTrigger("Die");
        // SoundManager.singleton.PlaySoundEffect(loseSound, 0);
        SoundManager.singleton.PlaySoundEffect(impactSound, 0);
        rb.linearVelocity = Vector3.zero;
        ScreenShake.singleton.Shake(2f, 0.1f);
        GameManager.singleton.GameOver();
    }

    public void PlayWalkSound1() {
        // SoundManager.singleton.PlaySoundEffect(walk1Sound);
    }

    public void PlayWalkSound2() {
        // SoundManager.singleton.PlaySoundEffect(walk2Sound);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheckTransform.position, Vector3.down * groundCheckDistance);
    }

    public IEnumerator ChangeToSprintFov(float fadeDuration = 0.2f)
    {
        float startFov = cinemachineCamera.Lens.FieldOfView;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFov, fovWhileSprinting, elapsed / fadeDuration);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = fovWhileSprinting;
    }

    public IEnumerator ChangeToRunFov(float fadeDuration = 0.2f)
    {
        float startFov = cinemachineCamera.Lens.FieldOfView;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFov, fovWhileRunning, elapsed / fadeDuration);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = fovWhileRunning;
    }
}