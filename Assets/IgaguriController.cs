using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgaguriController : MonoBehaviour
{
    public int[] resultMap = { 3, 2, 4, 1, 5, 0 };

    Rigidbody rigidBody;
    ParticleSystem particle;
    ResultController resultLabel;
    IgaguriGenerator igaguriGenerator;
    GameObject targetRoulette;
    AudioSource audioSourceFail;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
        resultLabel = GameObject.Find("ResultLabel").GetComponent<ResultController>();
        igaguriGenerator = GameObject.Find("igaguriGenerator").GetComponent<IgaguriGenerator>();
        targetRoulette = GameObject.Find("TargetRoulet");
        audioSourceFail = GetComponent<AudioSource>();
    }

    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    float loopRange(float x, float max)
    {
        return ((x % max) + max) % max;
    }

    void OnCollisionEnter(Collision other)
    {
        rigidBody.isKinematic = true;
        particle.Play();
        RouletteControl ctrl = other.gameObject.GetComponent<RouletteControl>();
        if (ctrl != null)
        {
            ctrl.rot_speed = 0;

            Vector2 center = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 sub = pos - center;
            float angle_center = other.gameObject.transform.localEulerAngles.y;
            float angle_shot = Mathf.Rad2Deg * Mathf.Atan2(sub.y, -sub.x) - 60;
            float angle = loopRange(angle_shot - angle_center, 360);

            //Debug.Log(angle_center + ": " + angle_shot);
            Debug.Log((int)(angle / 60));
            int res = resultMap[(int)(angle / 60)];
            resultLabel.ShowResult(res);
            igaguriGenerator.isHit = true;
            transform.parent = targetRoulette.transform;
            ctrl.GetComponent<AudioSource>().Play();
            GameDirector.Get().hitCount++;
            GameDirector.Get().Luck(res);
        }
        else
        {
            audioSourceFail.Play();
        }
    }
}
