using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class VideoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public TextMeshProUGUI tm;
    // Start is called before the first frame update
    void Start()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "tutorial4reflection.mov");
        videoPlayer.url = filePath;
        Debug.Log(filePath);
    }

    public void Play()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            tm.text = "Play Instruction";
        } else
        {
            videoPlayer.Play();
            tm.text = "Stop Instruction";
        }
    }
}
