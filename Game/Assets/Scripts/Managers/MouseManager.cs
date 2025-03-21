using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] private Texture2D texture2D;

    protected override void Awake()
    {
        base.Awake();
        texture2D = Resources.Load<Texture2D>("Default");
    }
    private void Start()
    {
        //texture2D = Resources.Load<Texture2D>("Default");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    public void SetState(int state)
    {
        if (state == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (state == 1)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.ForceSoftware);
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetState(scene.buildIndex);
    }

}
