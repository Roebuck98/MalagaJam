using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPlayer : MonoBehaviour
{
    //public LevelManager LevelManager;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public Transform Player1SpawnPoint;
    public Transform Player2SpawnPoint;

    private string _gamepadLayout = "XInputControllerWindows";
    private int _maxPlayers = 2;
    private int _currentPlayers = 0;
    private bool _player1Spawned = false;
    GameObject _player;

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
                if (!_player1Spawned)
                {
                    var player = InstantiatePlayer(0, Player1Prefab, Player1SpawnPoint, allGamepads[0]);
                    _player = player;
                    //LevelManager.SetPlayer(player);
                    _player1Spawned = true;
                }
                else
                {
                    var player = InstantiatePlayer(1, Player2Prefab, Player2SpawnPoint, allGamepads[1]);
                    _player.GetComponent<Defender>().otherPlayer = player;
                    player.GetComponent<Defender>().otherPlayer = _player;
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