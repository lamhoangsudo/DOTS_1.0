using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    [SerializeField] private GameObject ShootText;
    private void Start()
    {
        //GetExistingSystemManaged for systemBase
        //GetExistingSystem for ISystem
        //system alway singleton 
        PlayerShootingSystem playerShootingSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
        playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    }

    private void PlayerShootingSystem_OnShoot(object sender, System.EventArgs e)
    {
        Entity playerEntity = (Entity)sender;
        //get copoment data from enity
        LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
        Instantiate(ShootText, localTransform.Position, Quaternion.identity);
    }
}
