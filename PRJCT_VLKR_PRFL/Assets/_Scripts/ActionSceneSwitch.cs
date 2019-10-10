using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSceneSwitch : MonoBehaviour
{
    bool _startToAppear = false;
    bool _startToDisappear = false;
    public bool _moveButtons = false;
    int _attackingButton = 0;
    float _timer = 0f, _animationTimeDuration = 0.5f;

    [Header("UI Camera and slider")]
    public Renderer _actionCam;
    public GameObject[] _actionSlides;
    [Header("UI Buttons")]
    public Transform[] player1 = new Transform[4];
    public Transform[] player2 = new Transform[4];
    public Vector3[] normalPos = new Vector3[4], attackingPos = new Vector3[4];

    public void StartAppearing(){
        _startToAppear = true;
        _startToDisappear = false;
    }
    public void StartDisAppearing(){
        _startToDisappear = true;
        _startToAppear = false;
    }
    public void SetButtonsPos(int button)
    {
        _moveButtons = true;
        _attackingButton = button;
    }

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            normalPos[i] = player1[i].localPosition;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_startToAppear)
        {
            ActionCamAdjust(1);
            ActionSliderAdjust(new float[2]{405, -405});

            // if Done
            if (_actionCam.material.GetFloat("_Transparency") >= 0.999) { _startToAppear = false; }
        }
        if (_startToDisappear)
        {
            _moveButtons = false;
            ActionCamAdjust(0);
            ActionSliderAdjust(new float[2] { 688.5f, -688.5f });

            // id done
            if (_actionCam.material.GetFloat("_Transparency") <= 0.001) { _startToDisappear = false; }
        }
        if (_moveButtons)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == _attackingButton - 1)
                {
                    player2[i].localPosition = player1[i].localPosition = new Vector3(
                        Mathf.Lerp(player1[i].localPosition.x, attackingPos[0].x, _animationTimeDuration / 2),
                        Mathf.Lerp(player1[i].localPosition.y, attackingPos[0].y, _animationTimeDuration / 2));
                    player2[i].localPosition -= new Vector3(100, 0);
                }
            }
            switch (_attackingButton)
            {
                case 1: MoveToAttack(new int[] { 1, 2, 3 });
                    break;
                case 2:
                    MoveToAttack(new int[] { 0, 2, 3 });
                    break;
                case 3:
                    MoveToAttack(new int[] { 0, 1, 3 });
                    break;
                case 4:
                    MoveToAttack(new int[] { 0, 1, 2, });
                    break;
            }
        }
        if (!_moveButtons)
        {
            MoveToNormal();
        }
    }

    void MoveToAttack(int[] normals)
    {
        for (int i = 0; i < 3; i++)
        {
            player2[normals[i]].localPosition = player1[normals[i]].localPosition = new Vector3 (
                Mathf.Lerp(player1[normals[i]].localPosition.x, attackingPos[i+1].x, _animationTimeDuration / 2),
                Mathf.Lerp(player1[normals[i]].localPosition.y, attackingPos[i+1].y, _animationTimeDuration / 2));
            player2[normals[i]].localPosition -= new Vector3(100, 0);
        }
    }

    void MoveToNormal()
    {
        for (int i = 0; i < 4; i++)
        {
            player2[i].localPosition = player1[i].localPosition = new Vector3(
                Mathf.Lerp(player1[i].localPosition.x, normalPos[i].x, _animationTimeDuration / 2),
                Mathf.Lerp(player1[i].localPosition.y, normalPos[i].y, _animationTimeDuration / 2));
            player2[i].localPosition -= new Vector3(100, 0);
        }
    }

    void ActionCamAdjust(int value)
    {
        _actionCam.material.SetFloat("_Transparency", Mathf.Lerp(_actionCam.material.GetFloat("_Transparency"), value, _animationTimeDuration / 4));
    }

    void ActionSliderAdjust(float[] yPos)
    {
        for (int i = 0; i < _actionSlides.Length; i++){
            _actionSlides[i].transform.localPosition = new Vector3(0, Mathf.Lerp(_actionSlides[i].transform.localPosition.y, yPos[i], _animationTimeDuration / 2.5f), 0);
        }
    }
}
