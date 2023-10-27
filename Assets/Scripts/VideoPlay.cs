using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    [SerializeReference] VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "tutorial4reflection.mov");
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
