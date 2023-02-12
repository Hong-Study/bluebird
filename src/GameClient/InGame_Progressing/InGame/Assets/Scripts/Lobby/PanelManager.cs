using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject modePanel;
    public GameObject optionPanel;
    public GameObject errorGroup;
    public GameObject matchButton;
    public GameObject matchCancleButton;
    public GameObject soloButton;
    public GameObject duoButton;
    public GameObject squadButton;

    public Text matchText;
    public Text soloText;
    public Text duoText;
    public Text squadText;
    public Text errorText;

    private bool isMatching = false;
    private bool isCancel = false;

    public bool[] seletedModes = { true, false, false };
    public int modsSum = 1;

    public static PanelManager panelManager;

    Coroutine changeMatchTextRoutine = null;
    Coroutine errorTextRoutine = null;
    private void Awake()
    {
        panelManager = this;
    }

    public void MatchSelect()
    {
        if (modsSum == 0)
        {
            if(errorTextRoutine != null)
            {
                StopCoroutine(errorTextRoutine);
            }
            errorTextRoutine = StartCoroutine(ErrorTextOutput("�� �� �̻��� ��带 �������ּ���!"));
            return;
        }
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

        Debug.Log("Match Start: " + isSuccess);
        if(!isSuccess)
        {
            StartCoroutine(ErrorTextOutput("��Ī�� �����߽��ϴ�."));
            yield return new WaitForSeconds(2f);
            StopCoroutine(changeMatchTextRoutine);
            matchText.text = "��ġ����ŷ";
            isMatching = false;
            yield break;
        }
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

            if(matchStatus == -1)
            {
                continue;
            }
            else if(matchStatus == -2)
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

                if (!isSuccess)
                {
                    Debug.Log("��ġ����ŷ ��� ����");
                }

                isMatching = false;
                isCancel = false;

                matchText.text = "��ġ����ŷ";
                matchCancleButton.SetActive(false);
                break;
            }
            else
            {
                //���� ���� �� �� ��ȯ
                LobbyInfo.lobbyInfo.userNo = LobbyGameManager.gameManager.userNo;
                LobbyInfo.lobbyInfo.room = matchStatus;
                SceneManager.LoadScene("Stage1");
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

    IEnumerator ErrorTextOutput(string msg)
    {
        errorText.text = msg;
        errorGroup.SetActive(true);
        yield return new WaitForSeconds(2f);
        errorGroup.SetActive(false);
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
