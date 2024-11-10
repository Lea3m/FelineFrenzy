using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip gameOverSound;
    public AudioSource audioSource;

    public void RestartGame()
    {
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        Debug.Log("All characters are dead! Restarting the game...");

        // Reload the current scene
        Invoke("ReloadScene", 3f);
    }

    private void ReloadScene()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
