using UnityEngine;
using System.Collections;

public class ControlMovie : MonoBehaviour
{
    [SerializeField]
     private MovieTexture movieTexture;

    void Start()
    {
        movieTexture.Play();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movieTexture.Pause();
        }
        else if (Input.GetKey(KeyCode.V))
        {
            movieTexture.Play();
        }
    }
}
