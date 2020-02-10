using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InControl;

public class MultiplayerControllerFinder : MonoBehaviour
{
    public Text controllerText;
    public GameObject[] _player;
    public Text[] playerText;

    List<InputDevice> controllers = new List<InputDevice>();
    bool[] playersConnected = new bool[2];
    bool moveDown = false;

    List<InputDevice> actuallPlayers = new List<InputDevice>();

    void Start()
    {
        InputManager.OnDeviceAttached += inputDevice => Debug.Log("Attached: " + inputDevice.Name);
        InputManager.OnDeviceDetached += inputDevice => Debug.Log("Detached: " + inputDevice.Name);
        InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log("Switched: " + inputDevice.Name);
        ShowUI(1);
    }

    bool updateOrNot = true;
    // Update is called once per frame
    void Update()
    {
        if (!playersConnected[0]) { listenToControllers(0); }
        else if (!playersConnected[1]) { listenToControllers(1); }


        if (playersConnected[0] && playersConnected[1] && updateOrNot)
        {
            updateOrNot = false;
            print(actuallPlayers[0].Name + " " + actuallPlayers[1].Name);
            StartCoroutine(WaitTimer());
        }

        if (moveDown)
        {
            for (int i = 0; i < 2; i++)
            {
                _player[i].transform.position =
                    new Vector3(
                        _player[i].transform.position.x,
                        Mathf.Lerp(_player[i].transform.position.y, -5, 0.05f),
                        _player[i].transform.position.z);
                playerText[i].transform.position =
                    new Vector3(
                        playerText[i].transform.position.x,
                        Mathf.Lerp(playerText[i].transform.position.y, -10, 0.05f),
                        playerText[i].transform.position.z);
            }
            if(_player[1].transform.position.y == -10)
            {
                moveDown = false;
                GameObject.Find("CharacterSelect").GetComponent<MP_CharacterSelect>().StartSelection();
            }
        }
    }

    void listenToControllers(int player)
    {

        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i].AnyButtonWasReleased)
            {
                if (player == 1) { if (actuallPlayers[0] == InputManager.Devices[i]) { return; } }
                actuallPlayers.Add(InputManager.Devices[i]);
                playersConnected[player] = true;
                _player[player].GetComponent<Animator>().SetBool("True", true);
                //print("added: " + controllers[i]);
                ShowUI(2);
                if (playersConnected[0] && playersConnected[1]) { SaveControllers(); ShowUI(3); }
            }
        }
    }

    void ShowUI(int cntrlNmbr)
    {
        if (cntrlNmbr == 1) { playerText[0].text = "Press any button on controller 1"; }
        else if (cntrlNmbr == 2) { playerText[1].text = "Press any button on controller 2"; playerText[0].text = "CONNECTED"; }
        else if (cntrlNmbr == 3) { playerText[1].text = "CONNECTED"; }
    }

    void SaveControllers()
    {
        GameObject.Find("ControllerHolder").GetComponent<ControllerHolder>()._players = actuallPlayers;
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(2);
        moveDown = true;
    }
}
