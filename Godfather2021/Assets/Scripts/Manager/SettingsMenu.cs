using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    //Gère le son
    public AudioMixer audioMixer;

    //Gère la résolution de l'écran
    public Dropdown resolutionDropdown;

    //Récupère sous forme de liste les différentes résolutions du pc qui lui sont disponibles
    private Resolution[] resolutions;

    public Slider musicSlider;
    public Slider sfxSlider;

    private static float volumeSFX;
    private static float volumeMusic;

    //private bool valueSet = false;

    public void Start()
    {
        //Permet de stocker la valeur de l'audioMixer dans la variable musicValueForSlider
        audioMixer.GetFloat("volume", out float musicValueForSlider);

        if (volumeMusic == 0)
        {
            musicSlider.value = 0.01f;
            volumeMusic = 0.01f;
        }

        if(volumeSFX == 0)
        {
            sfxSlider.value = 0.5f;
            volumeSFX = 0.5f;
        }

        musicSlider.value = volumeMusic;
        sfxSlider.value = volumeSFX;

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        //Liste l ensemble des resolutions
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(volumeMusic + " / " + volumeSFX);
        musicSlider.value = volumeMusic;
        sfxSlider.value = volumeSFX;
        AudioManager.instance.ChangeVolumeMusic(volumeMusic);
        AudioManager.instance.ChangeVolumeSFX(volumeSFX);
    }

    /// <summary>
    /// Permet de changer le volume
    /// </summary>
    public void SetVolumeMusic(float volume)
    {
        AudioManager.instance.ChangeVolumeMusic(volume);
        volumeMusic = volume;
    }

    public void SetVolumeSFX(float volume)
    {
        AudioManager.instance.ChangeVolumeSFX(volume);
        volumeSFX = volume;
    }

    /// <summary>
    /// Permet d activer ou non le fullscreen
    /// </summary>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    /// <summary>
    /// Permet de changer la resolution
    /// </summary>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
