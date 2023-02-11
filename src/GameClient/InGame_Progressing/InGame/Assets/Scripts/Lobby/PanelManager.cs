using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject modePanel;
    public GameObject optionPanel;
    public GameObject matchButton;
    public GameObject matchCancleButton;
    public GameObject soloButton;
    public GameObject duoButton;
    public GameObject squadButton;

    public Text matchText;
    public Text soloText;
    public Text duoText;
    public Text squadText;

    private bool isMatching = false;
    private bool isCancel = false;

    public bool[] seletedModes = { true, false, false };
    public int modsSum = 1;

    public static PanelManager panelManager;

    Coroutine changeMatchTextRoutine = null;
    private void Awake()
    {
        panelManager = this;
    }

    public void MatchSelect()
    {
        if (!isMatching)
        {
            isMatching = true;
            changeMatchTextRoutine = StartCoroutine(ChangeMatchText());
            StartCoroutine(StartMatch());
        }
    }

    IEnumerator StartMatch()
    {
        bool wait = true;
        bool isSuccess = false;
        StartCoroutine(LobbyNetworkManager.networkManager.MatchInitPost((callback) =>
        {
            isSuccess = callback;
            wait = false;
        }));

        while(wait)
        {
            yield return null;
        }

        wait = true;
        isSuccess = false;
        Debug.Log("MatchRoom: " + isSuccess);
        matchCancleButton.SetActive(true);
        int matchStatus = 0;
        while(true)
        {
            StartCoroutine(LobbyNetworkManager.networkManager.MatchStartPost(isCancel, (callback) =>
            {
                matchStatus = callback;
                wait = false;
            }));

            while (wait)
            {
                yield return null;
            }
            wait = true;

            if(matchStatus == 0)
            {
                continue;
            }
            else if(matchStatus == 1)
            {
                //���� ���� �� �� ��ȯ
                LobbyInfo.lobbyInfo.userNo = LobbyGameManager.gameManager.userNo;
                LobbyInfo.lobbyInfo.room = 1;
            }
            else
            {
                Debug.Log("��ġ����ŷ ��ҵ�");
                StopCoroutine(changeMatchTextRoutine);
                matchText.text = "��ġ �����..";

                StartCoroutine(LobbyNetworkManager.networkManager.MatchCancelPost((callback) =>
                {
                    isSuccess = callback;
                    wait = false;
                }));

                while (wait)
                {
                    yield return null;
                }

                isMatching = false;
                isCancel = false;
                
                matchText.text = "��ġ����ŷ";
                matchCancleButton.SetActive(false);
                break;
            }

            Debug.Log("��ġ ����: " + matchStatus);
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("While�� ����");
    }

    IEnumerator ChangeMatchText()
    {
        while(true)
        {
            matchText.text = "��ġ ��.";
            yield return new WaitForSeconds(1f);
            matchText.text = "��ġ ��..";
            yield return new WaitForSeconds(1f);
            matchText.text = "��ġ ��...";
            yield return new WaitForSeconds(1f);
        }
    }


    public void MatchCancle()
    {
        isCancel = true;
    }

    public void ModeSelect()
    {
        menuPanel.SetActive(false);
        modePanel.SetActive(true);
    }

    public void OptionSelect()
    {
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void BackFromMode()
    {
        menuPanel.SetActive(true);
        modePanel.SetActive(false);
    }

    public void BackFromOption()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void SoloOnClick()
    {
        if(!seletedModes[0])
        {
            soloText.text = "���õ�";
            seletedModes[0] = true;
            modsSum += 1;
        }
        else
        {
            soloText.text = "���ܵ�";
            seletedModes[0] = false;
            modsSum -= 1;
        }
    }

    public void DuoOnClick()
    {
        if (!seletedModes[1])
        {
            duoText.text = "���õ�";
            seletedModes[1] = true;
            modsSum += 2;
        }
        else
        {
            duoText.text = "���ܵ�";
            seletedModes[1] = false;
            modsSum -= 2;
        }
    }

    public void SquadOnClick()
    {
        if (!seletedModes[2])
        {
            squadText.text = "���õ�";
            seletedModes[2] = true;
            modsSum += 4;
        }
        else
        {
            squadText.text = "���ܵ�";
            seletedModes[2] = false;
            modsSum -= 4;
        }
    }
}
