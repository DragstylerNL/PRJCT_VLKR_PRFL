using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InControl;

public class MultiplayerControllerFinder : MonoBehaviour
{
    public Text controllerText;

    List<InputDevice> controllers = new List<InputDevice>();
    bool[] playersConnected = new bool[2];

    List<InputDevice> actuallPlayers = new List<InputDevice>();

    void Start()
    {
        InputManager.OnDeviceAttached += inputDevice => Debug.Log("Attached: " + inputDevice.Name);
        InputManager.OnDeviceDetached += inputDevice => Debug.Log("Detached: " + inputDevice.Name);
        InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log("Switched: " + inputDevice.Name); ;
        ShowUI(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playersConnected[0]) { listenToControllers(0); }
        else if (!playersConnected[1]) { listenToControllers(1); }

        if (playersConnected[0] && playersConnected[1])
        {
            print(actuallPlayers[0].Name + " " + actuallPlayers[1].Name);
            SceneManager.LoadScene("MultiplayerBattleZone");
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
                //print("added: " + controllers[i]);
                ShowUI(2);
                if (playersConnected[0] && playersConnected[1]) { RemoveUI(); SaveControllers(); }
            }
        }
    }

    void ShowUI(int cntrlNmbr)
    {
        controllerText.text = "Press any button on controller " + cntrlNmbr;
    }

    void RemoveUI()
    {
        controllerText.text = "&%#^Loading&^#%";
    }

    void SaveControllers()
    {
        GameObject.Find("MultiplayerControllerHolder").GetComponent<MultiplayerControllerHolder>()._players = actuallPlayers;
    }
}
