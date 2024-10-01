using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectileSelector : MonoBehaviour
{
    public Dropdown projectileDropdown; // Assign in Inspector
    public GameObject[] projectilePrefabs; // Assign your projectile prefabs here
    public string[] projectileNames; // Custom names for the dropdown options
    public Button confirmButton; // Assign the confirm button in Inspector

    private int selectedProjectileIndex; // Store selected index

    void Start()
    {
        // Ensure projectileNames has the same length as projectilePrefabs
        if (projectilePrefabs.Length != projectileNames.Length)
        {
            Debug.LogError("Projectile names array must be the same length as projectile prefabs array.");
            return;
        }

        // Populate dropdown options
        projectileDropdown.ClearOptions();
        List<string> options = new List<string>(projectileNames);
        projectileDropdown.AddOptions(options);
        
        // Add listener for dropdown selection
        projectileDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        
        // Add listener for the confirm button
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    private void OnDropdownValueChanged(int index)
    {
        selectedProjectileIndex = index; // Store the selected index
        Debug.Log("Selected projectile: " + projectileNames[index]);
    }

    private void OnConfirmButtonClicked()
    {
        // Save the selected projectile index (or prefab reference) before transitioning
        PlayerPrefs.SetInt("SelectedProjectileIndex", selectedProjectileIndex); // Store index
        Debug.Log("Confirmed projectile index: " + selectedProjectileIndex);

        // Load the game scene (replace "GameScene" with the actual name of your game scene)
        SceneManager.LoadScene("Game");
    }
}


