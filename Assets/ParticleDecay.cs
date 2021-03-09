using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecay : MonoBehaviour
{
    public float destroyTime = 2f;
    float timer = 0f;

    // Update is called once per frame
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime)
            Destroy(gameObject);
    }
}
