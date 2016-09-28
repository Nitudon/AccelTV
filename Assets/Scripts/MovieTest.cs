using UnityEngine;
using System.Collections;

public class MovieTest : MonoBehaviour
{
    [SerializeField]
    private MovieTexture[] movieTexture;

    void Start()
    {
        foreach(MovieTexture elm in movieTexture)
        {
            elm.Play();
        }
    }

    void Update()
    {

    }
}
