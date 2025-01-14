using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 스크립트에서는 이 매니저(Manager.X)를 통해 다양한 컨트롤 가능
public class Manager : MonoBehaviour
{
    
    // Singleton Pattern

    static Manager s_instance;  // static을 통한 매니저 유일성 보장	
    public static Manager Instance { get { Init(); return s_instance; } }   // 유일성이 보장된 매니저 객체 반환

    // 다른 스크립트에서 Manager.Input.X로 매니저 접근 가능
    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    // 다른 스크립트에서 Manager.UI.X로 매니저 접근 가능
    UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    InventoryManager _inventory = new InventoryManager();
    public static InventoryManager Inventory { get { return Instance._inventory; } }

    // 다른 스크립트에서 Manager.Interaction.X로 매니저 접근 가능
    InteractionManager _interaction = new InteractionManager();
    public static InteractionManager Interaction { get { return Instance._interaction; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    void Awake() {
        _data.setDataPath(Application.persistentDataPath + "/");
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        // 키 입력이 있는지 없는지 프레임마다 체크하며 키 입력 발생 시 Action 실행
        _input.OnUpdate();
    }

    // 게임 실행 시 초기 게임 환경 구성
    static void Init()
    {
        if (s_instance == null)
        {
            // 혹시 매니저가 이미 게임 상에 존재하는지 체크
            GameObject go = GameObject.Find("@Manager");

            // 게임 실행 시 매니저 생성
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Manager>();
            }

            // 매니저 파괴 방지
            DontDestroyOnLoad(go);

            // 동적으로 생성한 매니저 객체에 Manager 컴포넌트 부여
            s_instance = go.GetComponent<Manager>();
        }
    }
}
