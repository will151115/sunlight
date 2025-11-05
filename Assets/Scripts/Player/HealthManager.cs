using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;            // assign in inspector if you can
    public float healthAmount = 100f;
    public float maxHealth = 100f;
    public GameObject player;    
    [SerializeField] private PlayerMovement playerMovement;
      // assign or tag the player "Player"

    private void Start()
    {
        if (maxHealth <= 0f) maxHealth = 100f;
        healthAmount = Mathf.Clamp(healthAmount, 0f, maxHealth);

        // try to auto-find player and UI if not assigned
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        FindHealthBar();

        // initialize UI
        UpdateHealthUI();

        // ensure a default respawn if GameManager exists
        if (GameManager.instance != null && GameManager.instance.respawnPosition == Vector3.zero && player != null)
            GameManager.instance.respawnPosition = player.transform.position;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Try multiple heuristics to find the health bar image in the scene
    private void FindHealthBar()
    {
        if (healthBar != null) return;

        // 1) exact name
        var go = GameObject.Find("HealthBarFill");
        if (go != null)
        {
            healthBar = go.GetComponent<Image>();
            if (healthBar != null) { Debug.Log("HealthManager: Found HealthBarFill by name."); return; }
        }

        // 2) look through Canvas children for something with "health" or "hp" in the name
        var canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            var imgs = canvas.GetComponentsInChildren<Image>(true);
            foreach (var img in imgs)
            {
                string n = img.name.ToLower();
                if (n.Contains("health") || n.Contains("hp") || n.Contains("fill"))
                {
                    healthBar = img;
                    Debug.Log("HealthManager: Found healthBar by heuristic: " + img.name);
                    return;
                }
            }
        }

        Debug.LogWarning("HealthManager: healthBar not assigned and could not be found automatically. Assign it in the Inspector or name it 'HealthBarFill'.");
    }

    public void TakeDamage(float damage)
    {
        if(playerMovement.playerCanTakeDamage)
        {
            healthAmount -= damage;
            healthAmount = Mathf.Clamp(healthAmount, 0f, maxHealth);
            UpdateHealthUI();
        }
        
    }

    public void Heal(float amount)
    {
        healthAmount += amount;
        healthAmount = Mathf.Clamp(healthAmount, 0f, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar == null)
        {
            // will try to find it so subsequent frames can update it
            FindHealthBar();
            if (healthBar == null) return;
        }

        healthBar.fillAmount = Mathf.Clamp01(healthAmount / maxHealth);
    }

    private void Update()
    {
        // safety: ensure UI always matches health (cheap)
        UpdateHealthUI();

        if (healthAmount <= 0f)
        {
            // reload scene; OnSceneLoaded will handle repositioning and UI reset
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // re-find player and UI because scene objects were recreated
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        FindHealthBar();

        if (player != null && GameManager.instance != null)
        {
            player.transform.position = GameManager.instance.respawnPosition;
        }

        // reset health & UI
        healthAmount = maxHealth;
        UpdateHealthUI();
    }
}
