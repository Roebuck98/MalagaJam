using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPlayer : MonoBehaviour
{
    //public LevelManager LevelManager;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public Transform Player1SpawnPointNormal;
    public Transform Player2SpawnPointNormal;
    
    public Transform Player1SpawnPointTuto;
    public Transform Player2SpawnPointTuto;

    private string _gamepadLayout = "XInputControllerWindows";
    private int _maxPlayers = 2;
    private int _currentPlayers = 0;
    private bool _player1Spawned = false;
    GameObject _player;
    IsTuto tuto;
    private void Start()
    {
        tuto = FindObjectOfType<IsTuto>();

    }

    void Update()
    {
        CheckConnectedGamepads();
    }

    private void CheckConnectedGamepads()
    {
        if (_currentPlayers < _maxPlayers)
        {
            var allGamepads = InputSystem.devices.Where(x => x.layout == _gamepadLayout).ToList();
            if (allGamepads.Count > _currentPlayers)
            {
                if(tuto.is_tuto)
                {
                    if (!_player1Spawned)
                    {
                        var player = InstantiatePlayer(0, Player1Prefab, Player1SpawnPointTuto, allGamepads[0]);
                        _player = player;
                        GameObject.FindObjectOfType<CameraFollower>().Target = player.transform;
                        BasicEnemyAI[] enemies = GameObject.FindObjectsOfType<BasicEnemyAI>();
                        foreach (var enemy in enemies)
                        {
                            enemy.target = player.transform;
                        }
                        //LevelManager.SetPlayer(player);
                        _player1Spawned = true;
                    }
                    else
                    {
                        var player = InstantiatePlayer(1, Player2Prefab, Player2SpawnPointTuto, allGamepads[1]);
                        _player.GetComponent<Defender>().otherPlayer = player;
                        player.GetComponent<Defender>().otherPlayer = _player;
                    }
                }
                else
                {
                    if (!_player1Spawned)
                    {
                        var player = InstantiatePlayer(0, Player1Prefab, Player1SpawnPointNormal, allGamepads[0]);
                        _player = player;
                        GameObject.FindObjectOfType<CameraFollower>().Target = player.transform;
                        BasicEnemyAI[] enemies = GameObject.FindObjectsOfType<BasicEnemyAI>();
                        foreach (var enemy in enemies)
                        {
                            enemy.target = player.transform;
                        }
                        //LevelManager.SetPlayer(player);
                        _player1Spawned = true;
                    }
                    else
                    {
                        var player = InstantiatePlayer(1, Player2Prefab, Player2SpawnPointNormal, allGamepads[1]);
                        _player.GetComponent<Defender>().otherPlayer = player;
                        player.GetComponent<Defender>().otherPlayer = _player;
                    }
                }
                _currentPlayers++;
            }
        }
    }

    private GameObject InstantiatePlayer(int playerIndex, GameObject playerPrefab, Transform transform, InputDevice device)
    {
        var allGamepads = InputSystem.devices.Where(x => x.layout == _gamepadLayout).ToList();
        PlayerInputManager.instance.playerPrefab = playerPrefab;
        var playerInput = PlayerInputManager.instance.JoinPlayer(playerIndex: playerIndex, pairWithDevice: device);

        playerInput.gameObject.transform.position = transform.position;
        playerInput.gameObject.transform.rotation = transform.rotation;

        return playerInput.gameObject;
    }
}