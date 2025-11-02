using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector3 respawnPosition;

    void Awake()
    {
        // Singleton pattern so it persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don’t destroy this object when loading scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}
