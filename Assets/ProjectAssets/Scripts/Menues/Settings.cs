using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro; 

public class Settings : MonoBehaviour
{
    public AudioMixer MusicMix;
    public AudioMixer SoundMix;
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown ResDropDown;
    int currentRes = 0;
    private void Start()
    {
        resolutions = Screen.resolutions;

        ResDropDown.ClearOptions();

       
        List<string> Resolutions = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string resolution = resolutions[i].width + "x" + resolutions[i].height;
            Resolutions.Add(resolution);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i; 
            }
        }
      
        ResDropDown.AddOptions(Resolutions);
        ResDropDown.value = currentRes;
        ResDropDown.RefreshShownValue(); 
    }
    public void MusicVolume(float MusicVol)
    {
        MusicMix.SetFloat("MusicVol", MusicVol); 
    }

    public void SoundVolume(float SoundVol)
    {
        SoundMix.SetFloat("SoundVol", SoundVol);
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex); 
    }

    public void setRes(int resIndex)
    {
     
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen); 
    }
}
