using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgaguriGenerator : MonoBehaviour {

    public bool isHit = false;
    public GameObject igaguriPrefab;
    AudioSource audioSource;

    Quaternion camRot;

    void Start()
    {
        camRot = Camera.main.transform.rotation;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        var mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        var mouseRot = Quaternion.Euler(-mouse.y * 10, mouse.x * 10, 0);
        Camera.main.transform.rotation = camRot * mouseRot;
        Camera.main.GetComponent<Animator>().SetBool("Enabled", Input.GetMouseButton(0));
		if (Input.GetMouseButtonUp(0))
        {
            if (!isHit)
            {
                GameObject igaguri = Instantiate(igaguriPrefab) as GameObject;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 worldDir = ray.direction;
                igaguri.GetComponent<IgaguriController>().Shoot(worldDir.normalized * 2000);
                audioSource.Play();
                GameDirector.Get().totalCount++;
            }
        }
	}
}
