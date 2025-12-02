using configs;
using data;
using entity;
using eventbus;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    [SerializeField] GameRunParam runParam;
    [SerializeField] GameSetting setting;

    private Game _game;

    void Awake()
    {
        Debug.Log("GameInit Awake");

        // 初始化Game单例
        _game = Game.Instance;

        // 初始化组件注册表
        EntityRegistryInitializer.Initialize();
        
        // 初始化事件总线
        EventBus.Instance.Initialize();
    }

    void Start()
    {
        Debug.Log("GameInit Start");

        if (runParam == null)
        {
            Debug.LogError("runParam Not Find");
            return;
        }

        if (setting == null)
        {
            Debug.LogError("setting Not Find");
            return;
        }

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject localPlayerObject = System.Array.Find(allObjects, obj => obj.name == "LocalPlayer");
        if (localPlayerObject == null)
        {
            Debug.LogError("localPlayerObject Not Find");
            return;
        }

        // todo 拿到玩家Id，初始化localPlayer的角色Entity
        var entity = EntityFactory.CreateEntity(EntityType.LocalPlayer, localPlayerObject);
        var playerData = entity.GetData<PlayerData>();
        playerData.PlayerId = "000001";

        // 玩家配置
        _game.RunParam = runParam;
        _game.Setting = setting;

        // 初始化
        _game.LocalPlayerData = new LocalPlayerData(entity.Id, playerData.PlayerId);

        // 获取PlayerController组件并设置entityId属性
        PlayerController playerController = localPlayerObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.entityId = entity.Id;
        }

        Debug.Log("localPlayerObject Active");
        localPlayerObject.SetActive(true);
    }

    void Update()
    {
    }
}