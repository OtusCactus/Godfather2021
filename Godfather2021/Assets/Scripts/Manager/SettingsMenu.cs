using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    //G�re le son
    public AudioMixer audioMixer;

    //G�re la r�solution de l'�cran
    public Dropdown resolutionDropdown;

    //R�cup�re sous forme de liste les diff�rentes r�solutions du pc qui lui sont disponibles
    private Resolution[] resolutions;

    public Slider musicSlider;

    public void Start()
    {
        //Permet de stocker la valeur de l'audioMixer dans la variable musicValueForSlider
        audioMixer.GetFloat("volume", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

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
    }

    /// <summary>
    /// Permet de changer le volume
    /// </summary>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
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
