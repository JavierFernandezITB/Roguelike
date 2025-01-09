using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityManagerService : ServicesReferences
{
    private void Awake()
    {
        base.Persist<EntityManagerService>();
    }

    public void Move(Transform transform, Vector3 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
