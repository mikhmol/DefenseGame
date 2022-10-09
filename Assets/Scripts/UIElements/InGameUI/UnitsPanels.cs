using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnitsPanels : MonoBehaviour
{
    public RectTransform personnelClassPanel, vehiclesClassPanel, facilitiesClassPanel;

    private float xDefPos = 760f, yDefPos = -45f, yPos = -0.5f;
    private int activeID;

    private void Start()
    {
        activeID = 0;
        personnelClassPanel.DOAnchorPos(new Vector2(xDefPos, yPos), 0.25f);
    }

    public void OpenClassButton(int classID)
    {
        if(classID == 0 && activeID != 0)
        {
            activeID = 0;
            OpenClassPanel(personnelClassPanel);
        }
        else if(classID == 1 && activeID != 1)
        {
            activeID = 1;
            OpenClassPanel(vehiclesClassPanel);
        }
        else if(classID == 2 && activeID != 2)
        {
            activeID = 2;
            OpenClassPanel(facilitiesClassPanel);
        }
    }

    private void OpenClassPanel(RectTransform panel)
    {
        vehiclesClassPanel.DOAnchorPos(new Vector2(xDefPos, yDefPos), 0.25f);
        facilitiesClassPanel.DOAnchorPos(new Vector2(xDefPos, yDefPos), 0.25f);
        personnelClassPanel.DOAnchorPos(new Vector2(xDefPos, yDefPos), 0.25f);

        panel.DOAnchorPos(new Vector2(xDefPos, yPos), 0.25f).SetDelay(0.25f);
    }
}
