﻿using UnityEngine;
using System.Collections;
using Nettention.Proud;

public partial class GameClient
{
    public GameObject m_treePrefab;

    private HostID m_myP2PGroupID = HostID.HostID_None; // will be filled after joining ville is finished.

    private void Update_InVille()
    {
       
    }

    private void OnGUI_InVille()
    {
        GUI.Label(new Rect(10, 10, 500, 70), "In Ville. You can plant or remove trees by touching terrain. You can also scribble on the terrain.");
        if (GUI.Button(new Rect(10, 90, 130, 30), "Tree"))
        {
            //m_fingerMode = FingerMode.Tree;
        }
        if (GUI.Button(new Rect(300, 90, 130, 30), "Scribble"))
        {
            //m_fingerMode = FingerMode.Scribble;
        }
    }

    void Start_InVilleRmiStub()
    {
        m_S2CStub.NotifyAddUnit = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int groupID, int treeID, UnityEngine.Vector3 position, string name) =>
        {
            if ((int)m_myP2PGroupID == groupID)
            {
                // pos 값 변경

                // Enemy 프리펩 생성
                GameObject Prefab = Resources.Load("Prefabs/Actors/" + name) as GameObject;

                Instantiate(Prefab, position, Prefab.transform.rotation);

                // plant a tree
                //GameObject o = (GameObject)Instantiate(m_treePrefab, position, Quaternion.identity);
                //WorldObject t = (WorldObject)o.GetComponent(typeof(WorldObject));
                //t.m_id = treeID;
            }

            return true;
        };

        //m_S2CStub.NotifyRemoveTree = (Nettention.Proud.HostID RemoteOfflineEventArgs, Nettention.Proud.RmiContext rmiContext, int groupID, int treeID) =>
        //{
        //    if ((int)m_myP2PGroupID == groupID)
        //    {
        //        // destroy picked tree
        //        WorldObject wo;
        //        if (WorldObject.m_worldObjects.TryGetValue(treeID, out wo))
        //        {
        //            Destroy(wo.gameObject);
        //        }
        //    }

        //    return true;
        //};
    }

    public void AddUnit(UnityEngine.Vector3 pos, string name)
    {
        // 서버에 보냄
        //m_C2SProxy.RequestAddUnit(HostID.HostID_Server, RmiContext.ReliableSend, pos, name);
    }
}
