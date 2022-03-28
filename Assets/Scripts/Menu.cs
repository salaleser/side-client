using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartButton()
    {
		GameManager.Instance.currentAddress.id = 5; // TODO
        GameManager.Instance.currentCitizen.id = 263; // TODO
        SceneManager.LoadScene("Main");
    }
}
