using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    public enum panel { Menu, MultiMenu, MultiPlayer, SinglePlayer, ConfExit, LocalMenu, OnlineMenu, Settings }
    public panel curPanel;

    public List<bool> menus = new List<bool>();
    bool _menu = true, _multiMenu = false, _multiPlayer = false, _singlePlayer = false, _confExit = false, _localMenu = false, _onlineMenu = false, _settings = false;

    [SerializeField] GameObject menu, multiMenu, multiPlayer, singlePlayer, confExit, localMenu, onlineMenu, settings;
    public List<GameObject> panelsList = new List<GameObject>();


    public void Start()
    {
        menus.Add(_menu);
        menus.Add(_multiMenu);
        menus.Add(_multiPlayer);
        menus.Add(_singlePlayer);
        menus.Add(_confExit);
        menus.Add(_localMenu);
        menus.Add(_onlineMenu);
        menus.Add(_settings);


        panelsList.Add(menu);
        panelsList.Add(multiMenu);
        panelsList.Add(multiPlayer);
        panelsList.Add(singlePlayer);
        panelsList.Add(confExit);
        panelsList.Add(localMenu);
        panelsList.Add(onlineMenu);
        panelsList.Add(settings);
    }


    public void setEnum()
    {
        switch (curPanel)
        {
            case panel.Menu:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _menu = true;
                menu.SetActive(true);

                break;

            case panel.MultiMenu:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _multiMenu = true;
                multiMenu.SetActive(true);

                break;

            case panel.MultiPlayer:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _multiPlayer = true;
                multiPlayer.SetActive(true);

                break;

            case panel.SinglePlayer:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _singlePlayer = true;
                singlePlayer.SetActive(true);

                break;

            case panel.ConfExit:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _confExit = true;
                confExit.SetActive(true);

                break;

            case panel.LocalMenu:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _localMenu = true;
                localMenu.SetActive(true);

                break;

            case panel.OnlineMenu:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _onlineMenu = true;
                onlineMenu.SetActive(true);

                break;

            case panel.Settings:

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i] = false;
                    panelsList[i].SetActive(false);
                }
                _settings = true;
                settings.SetActive(true);

                break;

        }
    }

    void setPanel()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i])
            {
                curPanel = (panel)i;
            }
        }
    }

}
