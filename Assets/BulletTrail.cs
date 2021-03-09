using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : ParticleDecay
{
    private SpriteRenderer renderer;
    public float decaySpeed = 2f;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Color color = renderer.color;
        color.a -= Time.deltaTime * decaySpeed;
        renderer.color = color;
    }
}
