using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentInteractor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentInteractor(GameObject interactor)
    {
        currentInteractor = interactor;
    }

    public void ClearCurrentInteractor(GameObject interactor)
    {
        if (currentInteractor == interactor)
        {
            currentInteractor = null;
        }
    }
}
