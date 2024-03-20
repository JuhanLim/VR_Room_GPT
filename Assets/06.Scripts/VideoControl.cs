using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer; // Unity Editor에서 할당
    public Button pauseButton; // Unity Editor에서 할당
    public Button playButton; // Unity Editor에서 할당
    public Button stopButton; // Unity Editor에서 할당
    private void Start()
    {
        pauseButton.onClick.AddListener(PauseVideo);
        playButton.onClick.AddListener(PlayVideo);
        stopButton.onClick.AddListener(StopVideo);
    }

    void PlayVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    void StopVideo()
    {
        videoPlayer.Stop();
    }
}