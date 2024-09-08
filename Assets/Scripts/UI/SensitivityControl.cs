using UnityEngine;
using UnityEngine.UI;
public class SensitivityControl : MonoBehaviour
{
    private Slider sensitivitySlider; // Drag the sensitivity slider here in the Inspector

    void Start()
    {
        sensitivitySlider = GetComponent<Slider>();

        // Set the slider's value to the current camera sensitivity
        sensitivitySlider.value = CameraManager.Instance.GetMouseSensitivity();

        // Add listener to slider to adjust sensitivity when it changes
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    // Method to set the camera sensitivity
    public void SetSensitivity(float sensitivity)
    {
        CameraManager.Instance.SetMouseSensitivity(sensitivity);
    }
}
