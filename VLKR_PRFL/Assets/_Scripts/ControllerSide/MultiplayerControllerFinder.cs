using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class MultiplayerControllerFinder : MonoBehaviour
{
    // ============================================================================================== "public" variables
    [SerializeField] private GameObject[] player;
    [SerializeField] private Text[] playerText;
    [SerializeField] private MP_CharacterSelect characterSelect;

    // =============================================================================================== private variables
    private List<InputDevice> _actuallPlayers = new List<InputDevice>();
    private bool[] _playersConnected = new bool[2];
    private bool _updateOrNot = true;
    private bool _moveDown;

    // =========================================================================================================== Awake
    private void Awake()
    {
        InputManager.OnDeviceAttached += inputDevice => Debug.Log("Attached: " + inputDevice.Name);
        InputManager.OnDeviceDetached += inputDevice => Debug.Log("Detached: " + inputDevice.Name);
        InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log("Switched: " + inputDevice.Name);
    }

    // =========================================================================================================== Start
    private void Start()
    {
        ShowUi(1);
    }

    // ========================================================================================================== Update
    void Update()
    {
        if (!_playersConnected[0]) { ListenToControllers(0); }
        else if (!_playersConnected[1]) { ListenToControllers(1); }


        if (_playersConnected[0] && _playersConnected[1] && _updateOrNot)
        {
            _updateOrNot = false;
            print(_actuallPlayers[0].Name + " " + _actuallPlayers[1].Name);
            StartCoroutine(WaitTimer());
        }

        if (_moveDown)
        {
            for (int i = 0; i < 2; i++)
            {
                player[i].transform.position =
                    new Vector3(
                        player[i].transform.position.x,
                        Mathf.Lerp(player[i].transform.position.y, -5.5f, 0.05f),
                        player[i].transform.position.z);
                playerText[i].transform.position =
                    new Vector3(
                        playerText[i].transform.position.x,
                        Mathf.Lerp(playerText[i].transform.position.y, -10, 0.05f),
                        playerText[i].transform.position.z);
            }
            if (player[1].transform.position.y <= -5.5f+0.01f)
            {
                _moveDown = false;
                characterSelect.StartSelection();
            }
        }
    }

    // ================================================================================================ Find Controllers
    void ListenToControllers(int player)
    {

        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i].AnyButtonWasReleased)
            {
                if (player == 1) { if (_actuallPlayers[0] == InputManager.Devices[i]) { return; } }
                _actuallPlayers.Add(InputManager.Devices[i]);
                _playersConnected[player] = true;
                this.player[player].GetComponent<Animator>().SetBool("True", true);
                ShowUi(2);
                if (_playersConnected[0] && _playersConnected[1]) { SaveControllers(); ShowUi(3); }
            }
        }
    }

    // ======================================================================================================= UI update
    void ShowUi(int cntrlNmbr)
    {
        if (cntrlNmbr == 1) { playerText[0].text = "Press any button on controller 1"; }
        else if (cntrlNmbr == 2) { playerText[1].text = "Press any button on controller 2"; playerText[0].text = "CONNECTED"; }
        else if (cntrlNmbr == 3) { playerText[1].text = "CONNECTED"; }
    }

    // ================================================================================================ save controllers
    void SaveControllers()
    {
        GameObject.Find("@ControllerHolder").GetComponent<ControllerHolder>()._players = _actuallPlayers;
    }

    // =========================================================================================================== Timer
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(0.5f);
        _moveDown = true;
    }
}
