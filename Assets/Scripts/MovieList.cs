using UnityEngine;
using System.Collections;

public class MovieList : MonoBehaviour
{
    public MovieTexture[] movieTexture;
    private int PlayIndex;
    void Start()
    {
        foreach (MovieTexture elm in movieTexture)
        {
            elm.Play();
        }
    }

    void Update()
    {

    }
}
