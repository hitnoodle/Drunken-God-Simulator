using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollBG : MonoBehaviour 
{
    public GameObject[] Backgrounds;

    public float Speed = 50;
    public float Factor = 1;

    public float DestroyWhenX = -15f;

    private List<Transform> BackgroundTransforms;
    private List<Transform> BackgroundToDelete;

	// Use this for initialization
	void Start() 
	{
        BackgroundTransforms = new List<Transform>();
        BackgroundToDelete = new List<Transform>();

        foreach (GameObject go in Backgrounds)
        {
            BackgroundTransforms.Add(go.transform);
        }
	}
	
	// Update is called once per frame
	void Update() 
	{
        foreach (Transform t in BackgroundTransforms)
        {
            t.Translate(Speed * Time.deltaTime * Factor, 0, 0);
            if (t.position.x <= DestroyWhenX) BackgroundToDelete.Add(t);
        }

        if (BackgroundToDelete.Count > 1)
        { 
            foreach(Transform t in BackgroundToDelete)
            {
                BackgroundTransforms.Remove(t);
                Destroy(t.gameObject); 
            }
            BackgroundToDelete.Clear();
        }
	}
}
