using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoSingleton<Scene_Manager>
{
    // 동기인지 비동기인지
    bool IsAsyc = true;
    AsyncOperation Operation = null;

    // 다음으로 이동하기위한
    eSceneType CurrentState = eSceneType.SCENE_LOGO;
    eSceneType NextState = eSceneType.SCENE_NONE;

    float StackTime = 0.0f;
    public eSceneType CURRENT_SCENE
    {
        get { return CurrentState; }
    }

    // 다음신으로 가기위한
    public void LoadScene(eSceneType _type, bool _async = true)
    {
        // 만약 있는곳과 가고자 하는 신이 같다면 return
        if (CurrentState == _type)
            return;

        NextState = _type;
        IsAsyc = _async;
    }

    void Update()
    {
        if (Operation != null)
        {
            StackTime += Time.deltaTime;

			// Loding UI Set
			//UI_Tools.Instance.ShowLoadingUI(Operation.progress);
			UI_Tools.Instance.ShowLoadingUI(StackTime / 1.5f);

			//if (Operation.isDone == true)
			if (Operation.isDone == true && StackTime >= 1.5f)
			{
                CurrentState = NextState;
                ComplateLoad(CurrentState);

                Operation = null;
                NextState = eSceneType.SCENE_NONE;

                // Loding UI 삭제
                UI_Tools.Instance.HideUI(eUIType.PF_UI_LOADING, true);
            }
            else
                return;
        }

        if (CurrentState == eSceneType.SCENE_NONE)
            return;

        if (NextState != eSceneType.SCENE_NONE
            && CurrentState != NextState)
        {
            // 현재사용하던 scene에 대한 정리
            DisableScene(CurrentState);

            if (IsAsyc)
            {   // 비동기 로드
                //Operation - 중간매개체 
                Operation = SceneManager.LoadSceneAsync(NextState.ToString("F"));
                StackTime = 0.0f;
                // Loading UI Set
                UI_Tools.Instance.ShowLoadingUI(0.0f);
            }
            else
            {   // 동기 로드
                SceneManager.LoadScene(NextState.ToString("F"));
                CurrentState = NextState;
                NextState = eSceneType.SCENE_NONE;
                ComplateLoad(CurrentState);
            }
        }
    }

    void ComplateLoad(eSceneType _type)
    {
        switch (_type)
        {
            case eSceneType.SCENE_NONE:
                break;
            case eSceneType.SCENE_LOGO:
                break;
            case eSceneType.SCENE_LOBBY:
                {
                    LobbyManager.Instance.LoadLobby();
                }
                break;
            case eSceneType.test:
                {
                    GameManager.Instance.LoadGame();
                    UI_Manager.Instance.Load();
                    BoardManager.Instance.Load();
                }
                break;
            default:
                break;
        }
    }

    void DisableScene(eSceneType _type)
    {
        // 신 유아이 삭제
        UI_Tools.Instance.Clear();

        switch (_type)
        {
            case eSceneType.SCENE_NONE:
                break;
            case eSceneType.SCENE_LOGO:
                break;
            case eSceneType.SCENE_LOBBY:
                LobbyManager.Instance.DisableLobby();
                break;
            case eSceneType.test:
                //ActorManager.Instance.Clear();
                GameManager.Instance.GoToLobby();
                break;
            default:
                break;
        }
    }
}
