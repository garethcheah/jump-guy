using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _platformTemplates = new List<GameObject>();
    [SerializeField] private List<GameObject> _activePlatforms = new List<GameObject>();
    [SerializeField] private GameObject _backgroundTemplate;
    [SerializeField] private List<GameObject> _activeBackgrounds = new List<GameObject>();

    public void GenerateNextPlatform(GameObject currentPlatform)
    {
        GameObject newPlatform;

        // Set start marker to inactive to prevent unnecessary generation of new platforms
        //transform.Find("Damage Indicator").GameObject()
        GameObject currentStartMarker = currentPlatform.transform.Find("Start Marker").GameObject();
        currentStartMarker.SetActive(false);

        // Instantiate new platform
        int randomIndex = Random.Range(0, _platformTemplates.Count);
        Vector3 newPlatformPosition = new Vector3(currentPlatform.transform.position.x + 20.0f, currentPlatform.transform.position.y);
        newPlatform = Instantiate(_platformTemplates[randomIndex], newPlatformPosition, Quaternion.identity);
        newPlatform.SetActive(true);

        // Add new platform to active platforms
        _activePlatforms.Add(newPlatform);

        // Generate next background if applicable
        GenerateNextBackground(_activeBackgrounds[_activeBackgrounds.Count - 1]);
    }

    private void GenerateNextBackground(GameObject currentBackground)
    {
        if (IsTimeToGenerateNextBackground())
        {
            GameObject newBackground;

            // Generate new background
            Vector3 newBackgroundPosition = new Vector3(currentBackground.transform.position.x + 64.0f, currentBackground.transform.position.y);
            newBackground = Instantiate(_backgroundTemplate, newBackgroundPosition, Quaternion.identity);

            // Add new background to active backgrounds
            _activeBackgrounds.Add(newBackground);
        }
    }

    private bool IsTimeToGenerateNextBackground()
    {
        // Generate new background for every 3 active platforms
        return _activePlatforms.Count % 3 == 0;
    }
}
