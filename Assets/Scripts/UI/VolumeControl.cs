using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private Slider volumeSlider; // Drag the volume slider here in the Inspector

    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        // Set the slider's value to the current volume level
        volumeSlider.value = AudioListener.volume;

        // Add listener to slider to adjust volume when it changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Method to set the volume
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}