using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsWindow : MonoBehaviour {

    [Header("Volume Settings")]
    [SerializeField] private Slider masterVolumeSlider = null;
    [SerializeField] private Slider soundVolumeSlider = null;
    [SerializeField] private Slider musicVolumeSlider = null;

    [Header("Graphics Settings")]
    [SerializeField] private Dropdown resolutionDropdown = null;
    [SerializeField] private Toggle fullscreenToggle = null;
    [SerializeField] private Dropdown qualityDropdown = null;

    [Header("Save Settings")]
    [SerializeField] private Button applyButton = null;

    private void Awake() {
        // Build resolutions
        resolutionDropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
            resolutions.Add(string.Format("{0} x {1}", Screen.resolutions[i].width, Screen.resolutions[i].height));
        resolutionDropdown.AddOptions(resolutions);

        // Build quality Levels
        qualityDropdown.ClearOptions();
        List<string> qualities = new List<string>();
        for (int i = 0; i < QualitySettings.names.Length; i++)
            qualities.Add(QualitySettings.names[i]);
        qualityDropdown.AddOptions(qualities);
        
    }

    private void OnEnable() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue);
        soundVolumeSlider.value = PlayerPrefs.GetFloat("Sound Volume", soundVolumeSlider.maxValue);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.maxValue);
        fullscreenToggle.isOn = Screen.fullScreen;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        applyButton.interactable = false;
    }

    public void SaveSettings() {
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("Sound Volume", soundVolumeSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);
        Screen.fullScreen = fullscreenToggle.isOn;
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }
}
