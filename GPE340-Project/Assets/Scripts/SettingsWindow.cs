using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SettingsWindow : MonoBehaviour {

    [Header("Audio Settings")]
    [SerializeField, Tooltip("The master audio mixer")]
    private AudioMixer audioMixer = null;
    [SerializeField, Tooltip("The slider value vs decibel volume curve")]
    private AnimationCurve volumeVsDecibels = null;

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
        // When enable this gameobject set all setting to our saved settings
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue);
        soundVolumeSlider.value = PlayerPrefs.GetFloat("Sound Volume", soundVolumeSlider.maxValue);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.maxValue);
        fullscreenToggle.isOn = Screen.fullScreen;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        applyButton.interactable = false;
    }

    // Set the Game Volumes
    public void SetVolume() {
        audioMixer.SetFloat("Master Volume", volumeVsDecibels.Evaluate(PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue)));
        audioMixer.SetFloat("Sound Volume", volumeVsDecibels.Evaluate(PlayerPrefs.GetFloat("Sound Volume", soundVolumeSlider.maxValue)));
        audioMixer.SetFloat("Music Volume", volumeVsDecibels.Evaluate(PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.maxValue)));
    }

    // Save all settings
    public void SaveSettings() {
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("Sound Volume", soundVolumeSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);
        Screen.fullScreen = fullscreenToggle.isOn;
        QualitySettings.SetQualityLevel(qualityDropdown.value);

        // Update Volume
        SetVolume();
    }
}
