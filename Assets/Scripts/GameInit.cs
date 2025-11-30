using data;
using entity;
using UnityEngine;
using UnityEngine.Serialization;

public class GameInit : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;

    private Game _game;

    void Awake()
    {
        Debug.Log("GameInit Awake");

        // 初始化Game单例
        _game = Game.Instance;

        // 初始化组件注册表
        EntityRegistryInitializer.Initialize();
    }

    void Start()
    {
        Debug.Log("GameInit Start");

        // todo 拿到玩家Id，初始化localPlayer的角色Entity
        var entity = EntityFactory.CreateEntity(EntityType.Role);
        var playerData = entity.GetData<PlayerData>();
        playerData.PlayerId = "000001";

        // 玩家配置
        _game.PlayerConfig = playerConfig;

        // 初始化
        _game.LocalPlayerData = new LocalPlayerData(entity.Id, playerData.PlayerId);

        // 查找名为LocalPlayer的游戏对象并启用它
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject localPlayerObject = System.Array.Find(allObjects, obj => obj.name == "LocalPlayer");
        if (localPlayerObject != null)
        {
            // 获取PlayerController组件并设置entityId属性
            PlayerController playerController = localPlayerObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.entityId = entity.Id;
            }

            Debug.Log("localPlayerObject Active");
            localPlayerObject.SetActive(true);
        }
        else
        {
            Debug.LogError("localPlayerObject Not Find");
        }
    }

    void Update()
    {
    }
}