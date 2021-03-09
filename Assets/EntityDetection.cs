using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetection : MonoBehaviour
{
    public float viewAngle = 45f;
    public LayerMask mask;
    public float range = 10f;
    public List<Entity> detectedEntities = new List<Entity>();
    public GameObject sprite;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, mask);

        for (int i = 0; i < colliders.Length; i++)
        {   
            Vector2 delta = colliders[i].transform.position - transform.position;
            if (Vector2.Angle(delta, sprite.transform.right) < viewAngle)
            {
                Entity detectedEntity;
                if (colliders[i].TryGetComponent<Entity>(out detectedEntity))
                {
                    if (!detectedEntities.Contains(detectedEntity))
                    {
                        detectedEntities.Add(detectedEntity);
                    }
                }
            } else
            {
                Entity detectedEntity;
                if (colliders[i].TryGetComponent<Entity>(out detectedEntity))
                {
                    if (detectedEntities.Contains(detectedEntity))
                    {
                        detectedEntities.Remove(detectedEntity);
                    }
                }
            }
        }

        foreach(Entity entity in detectedEntities)
        {
            Debug.DrawLine(transform.position, entity.transform.position);
        }
    }
}
