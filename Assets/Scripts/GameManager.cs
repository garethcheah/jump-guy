using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private List<GameObject> _platformTemplates = new List<GameObject>();
    [SerializeField] private List<GameObject> _activePlatforms = new List<GameObject>();
    [SerializeField] private GameObject _backgroundTemplate;
    [SerializeField] private List<GameObject> _activeBackgrounds = new List<GameObject>();
    [SerializeField] private int _cleanUpThresholdPlatforms = 9;
    [SerializeField] private int _cleanUpCountPlatforms = 3;
    [SerializeField] private int _cleanUpThresholdBackgrounds = 4;
    [SerializeField] private int _cleanUpCountBackgrounds = 1;

    private List<float> _gapLengths = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        _gapLengths.Add(0.0f);
        _gapLengths.Add(2.0f);
        _gapLengths.Add(4.0f);
    }

    public void GenerateNextPlatform(GameObject currentPlatform)
    {
        GameObject newPlatform;

        // Set start marker to inactive to prevent unnecessary generation of new platforms
        //transform.Find("Damage Indicator").GameObject()
        GameObject currentStartMarker = currentPlatform.transform.Find("Start Marker").GameObject();
        currentStartMarker.SetActive(false);

        // Set index to select a random platform template
        int randomPlatformIndex = Random.Range(0, _platformTemplates.Count);
        Debug.Log($"randomPlatformIndex={randomPlatformIndex}.");

        // Set index to select a random gap length between platforms
        int randomGapLengthIndex = Random.Range(0, _gapLengths.Count);
        Debug.Log($"randomGapLengthIndex={randomGapLengthIndex}.");

        // Instantiate new platform        
        Vector3 newPlatformPosition = new Vector3(currentPlatform.transform.position.x + 20.0f + _gapLengths[randomGapLengthIndex], currentPlatform.transform.position.y);
        newPlatform = Instantiate(_platformTemplates[randomPlatformIndex], newPlatformPosition, Quaternion.identity);
        newPlatform.SetActive(true);

        Debug.Log("New platform generated.");

        // Add new platform to active platforms
        _activePlatforms.Add(newPlatform);

        if (_activePlatforms.Count > _cleanUpThresholdPlatforms)
        {
            CleanupActivePlatforms();
        }

        if (IsTimeToGenerateNextBackground())
        {
            GenerateNextBackground(_activeBackgrounds[_activeBackgrounds.Count - 1]);
        }
    }

    public void UpdateCollectibleCount()
    {
        _statsManager.UpdateCollectibleCount();
    }

    public void UpdateLastDamagedTime()
    {
        _statsManager.UpdateLastDamagedTime();
    }

    private void GenerateNextBackground(GameObject currentBackground)
    {
        GameObject newBackground;

        // Generate new background
        Vector3 newBackgroundPosition = new Vector3(currentBackground.transform.position.x + 64.0f, currentBackground.transform.position.y);
        newBackground = Instantiate(_backgroundTemplate, newBackgroundPosition, Quaternion.identity);
        newBackground.SetActive(true);

        Debug.Log("New background generated.");

        // Add new background to active backgrounds
        _activeBackgrounds.Add(newBackground);

        if (_activeBackgrounds.Count > _cleanUpThresholdBackgrounds)
        {
            CleanUpActiveBackgrounds();
        }
    }

    private bool IsTimeToGenerateNextBackground()
    {
        int remainderValue = _activePlatforms.Count % _cleanUpCountPlatforms;

        Debug.Log($"IsTimeToGenerateNextBackground() remainder value: {remainderValue}.");

        return remainderValue == 0;
    }

    private void CleanupActivePlatforms()
    {
        for (int i = 0; i < _cleanUpCountPlatforms; i++)
        {
            Destroy(_activePlatforms[i]);
        }

        _activePlatforms.RemoveRange(0, _cleanUpCountPlatforms);

        Debug.Log($"{_cleanUpCountPlatforms} platforms have been destroyed.");
    }

    private void CleanUpActiveBackgrounds()
    {
        for (int i = 0; i < _cleanUpCountBackgrounds; i++)
        {
            Destroy(_activeBackgrounds[i]);
        }

        _activeBackgrounds.RemoveRange(0, _cleanUpCountBackgrounds);

        Debug.Log($"{_cleanUpCountBackgrounds} backgrounds have been destroyed.");
    }
}
