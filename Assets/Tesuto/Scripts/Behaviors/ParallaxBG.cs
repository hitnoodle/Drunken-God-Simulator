using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxBG : MonoBehaviour 
{
    public GameObject[] Backgrounds;
    public float Speed = 50;
    public float Factor = 1;

    // Ha ha
    public float LeftX;
    public float RightX;

    private List<Transform> BackgroundTransforms;

	// Use this for initialization
	void Start() 
	{
        BackgroundTransforms = new List<Transform>();
        foreach (GameObject go in Backgrounds)
            BackgroundTransforms.Add(go.transform);
	}
	
	// Update is called once per frame
	void Update() 
	{
        foreach (Transform t in BackgroundTransforms)
        {
            t.Translate(Speed * Time.deltaTime * Factor, 0, 0);
            if (t.position.x <= LeftX) t.position = new Vector3(RightX, t.position.y, t.position.z);
        }
	}
}
