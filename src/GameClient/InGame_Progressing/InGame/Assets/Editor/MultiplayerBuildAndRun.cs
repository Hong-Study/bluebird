using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// <���� �ڵ�ȭ>
//��Ƽ�÷��� �׽�Ʈ ȯ�濡�� Build�� �ϰ� ������ �׽�Ʈ�ϸ� ���ŷӴ�
//���� ��ũ��Ʈ�� �ۼ��Ͽ� �޴� �����ۿ� ����ϸ� �����ϰ� �ڵ����� ���带 �Ͽ� ����â�� ������ ų �� �ִ�. ��, ���� �ڵ�ȭ
public class MultiplayersBuildAndRun
{
    [MenuItem("Tools/Run Multiplayer/2 Players")]
    static void PerformWin64Build2()
    {
        PerformWin64Build(2);
    }



    static void PerformWin64Build(int playerCount)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);

        for (int i = 1; i <= playerCount; i++)
        {
            BuildPipeline.BuildPlayer(GetScenePaths(), "Build/Win64/" + GetProjectName() + i.ToString() + "/" + GetProjectName() + i.ToString() + ".exe",
                BuildTarget.StandaloneWindows64, BuildOptions.AutoRunPlayer);
        }
    }

    static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        //���� ������Ʈ ��� ����. Assst����
        return s[s.Length - 2];
    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        //BuildSetting�� Scene���� ����� ���� ���Եȴ�.

        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }


        return scenes;
    }


}
