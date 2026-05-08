using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour
{
    VideoPlayer vp;
    [SerializeField] string videoLocation;

    void Awake()
    {
#if !UNITY_EDITOR
        vp = GetComponent<VideoPlayer>();

        if (vp!= null)
        {
            vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoLocation);
        }
#endif
    }
}
