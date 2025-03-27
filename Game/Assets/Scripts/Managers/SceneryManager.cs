using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneryManager : Singleton<SceneryManager>
{
    [SerializeField] Image screenImg;
    public event Action loadEvent;

    private void Start()
    {
        
    }

    public IEnumerator AsynchLoad(int index)
    {
        screenImg.gameObject.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
    
        // <AsyncOperation.allowSceneActivation>
        // ����� �غ�� ��� ����� Ȱ��ȭ �Ǵ� ���� ����ϴ� ����
        asyncOperation.allowSceneActivation = false;

        Color color = Color.black;
        color.a = 0f;

        // <asyncOperation.isDone>
        // �ش� ������ �ϷḦ ��Ÿ���� bool
        while(!asyncOperation.isDone)
        {
            color.a += Time.deltaTime;
            screenImg.color = color;

            // <AsyncOperation.progress>
            // �۾��� ���� ���¸� ��Ÿ���� float
            if(asyncOperation.progress >= 0.9f)
            {
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime);
                screenImg.color = color;

                if(color.a >= 1f)
                {
                    asyncOperation.allowSceneActivation = true;

                    if(index == 1 )
                    {
                        //loadEvent += GameManager.Instance.BindTimeManagerEvent;
                    }

                    loadEvent?.Invoke();
                    loadEvent = null;
                    yield break;
                }
            }

            yield return null;
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private IEnumerator FadeIn()
    {
        Color newColor = screenImg.color;
        newColor.a = 1;
        screenImg.color = newColor;

        screenImg.gameObject.SetActive(true);
        

        while( screenImg.color.a > 0)
        {
            newColor.a -= Time.deltaTime;
            screenImg.color = newColor;
            yield return null;
        }
        screenImg.gameObject.SetActive(false);

    }

    private IEnumerator FadeOut()
    {
        Color newColor = screenImg.color;
        newColor.a = 0;
        screenImg.color = newColor;

        screenImg.gameObject.SetActive(true);


        while (screenImg.color.a < 1f)
        {
            newColor.a += Time.deltaTime;
            screenImg.color = newColor;
            yield return null;
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }

}
