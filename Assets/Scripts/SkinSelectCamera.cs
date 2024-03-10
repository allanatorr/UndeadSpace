using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelector : MonoBehaviour
{
    public Transform[] skins; // Array, das die Transforms der Skins enthält
    private int currentIndex = 0; // Index des aktuell ausgewählten Skins

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToNextSkin();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToPreviousSkin();
        }
    }

   public void MoveToNextSkin()
    {
        if (currentIndex < skins.Length - 1)
        {
            currentIndex++;
            Vector3 newPosition = new Vector3(skins[currentIndex].position.x, transform.position.y, transform.position.z);
            MoveCameraToPosition(newPosition);
        }
    }

    public void MoveToPreviousSkin()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Vector3 newPosition = new Vector3(skins[currentIndex].position.x, transform.position.y, transform.position.z);
            MoveCameraToPosition(newPosition);
        }
    }

    void MoveCameraToPosition(Vector3 targetPosition)
    {
        // Bewegen Sie die Kamera schrittweise zur Zielposition, ohne Interpolation
        transform.position = targetPosition;
    }

    public void SelectSkinButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
