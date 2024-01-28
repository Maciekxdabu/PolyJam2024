using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas defaultCanvas = null;

    private Canvas currentCanvas = null;
    private Canvas thisCanvas = null;

    // ---------- Unity messages

    private void Awake()
    {
        thisCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        DisableAllEnabledCanvases();
        ChangeCanvas(defaultCanvas);
    }

    // ---------- public methods

    public void ChangeCanvas(Canvas newCanvas)
    {
        if (currentCanvas != null)
            currentCanvas.gameObject.SetActive(false);

        currentCanvas = newCanvas;
        newCanvas.gameObject.SetActive(true);
    }

    public void Btn_OpenScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Btn_Quit()
    {
        Application.Quit();
    }

    // ---------- private methods

    private void DisableAllEnabledCanvases()
    {
        Canvas[] canvases = GetComponentsInChildren<Canvas>(false);

        foreach (Canvas can in canvases)
        {
            if (can != thisCanvas)
                can.gameObject.SetActive(false);
        }
    }
}
