using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathText;
    public GameObject winText;
    public Volume volume;
    public string nextLvl;
    //Bloom bloomLayer = null;
    DepthOfField depthLayer = null;
    Vignette vignetteLayer = null;
    public static GameManager S;
    private string sceneNumb = "0";

    private void Awake()
    {
        S = this;
        Time.timeScale = 1;
        volume.profile.TryGet<DepthOfField>(out depthLayer);
        volume.profile.TryGet<Vignette>(out vignetteLayer);

    }

    void SceneLoader()
    {
        SceneManager.LoadScene("Scene "+sceneNumb);
    }

    IEnumerator deathAnim(float timer)
    {
        float progress = 0;

        while(progress < timer)
        {
            progress += Time.deltaTime;
            vignetteLayer.intensity.value = progress;
            
            yield return null;
        }
    }
    public void ReloadScene(bool isDead)
    {
        if(isDead)
        {
            deathText.SetActive(true);
            sceneNumb = "0";
            depthLayer.active = true;
            vignetteLayer.active = true;
            StartCoroutine(deathAnim(0.5f));
            Time.timeScale = 0.1f;
            Invoke("SceneLoader", 0.5f);
        }
        else
        {
            //winText.SetActive(true);
            sceneNumb = nextLvl;
            SceneLoader();
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
