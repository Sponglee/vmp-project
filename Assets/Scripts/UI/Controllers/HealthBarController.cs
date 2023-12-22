using Anthill.Core;
using Anthill.Inject;
using UnityEngine;

public class HealthBarController : ISystem
{
    [Inject] public Game Game { get; set; }
    private HealthBarView _view;
    private Camera _camera;
    
    public void AddedToEngine()
    {
        AntInject.Inject<HealthBarController>(this);
        _view = ObjectFactory.Create<HealthBarView>("UI/HealthBarView/HealthBarView", Game.UIRoot);
        _camera = Camera.main;
        _view.Hide();
    }

    public void RemovedFromEngine()
    {
        throw new System.NotImplementedException();
    }


    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();    
    }
    
    
    public int RequestBar(HealthBarKind aKind)
    {
        for (int i = 0, n = _view.HealthBarItems.Count; i < n; i++)
        {
            if (!_view.HealthBarItems[i].Active)
            {
                _view.HealthBarItems[i].Active = true;
                _view.HealthBarItems[i].SetKind(aKind);
                return i;
            }
        }

        int index = MakeBar();
        _view.HealthBarItems[index].SetKind(aKind);
        return index;
    }
    
    public void ReleaseBar(int aIndex)
    {
        if (aIndex >= 0 && aIndex < _view.HealthBarItems.Count && _view.HealthBarItems[aIndex] != null)
        {
            _view.HealthBarItems[aIndex].Active = false;
        }
    }
    
    public void UpdateBarPosition(int aBarIndex, Vector3 aWorldPosition)
    {
        if (aBarIndex >= 0 && aBarIndex < _view.HealthBarItems.Count)
        {
            var bar = _view.HealthBarItems[aBarIndex];
            bar.UpdatePosition(_camera.WorldToScreenPoint(aWorldPosition));
        }
    }

    public void UpdateBarValue(int aBarIndex, float aValue)
    {
        if (aBarIndex >= 0 && aBarIndex < _view.HealthBarItems.Count)
        {
            var bar = _view.HealthBarItems[aBarIndex];
            bar.UpdateHealthBar(aValue);
        }
    }
    
    private int MakeBar()
    {
        var tmpBar = ObjectFactory.CreateObject(_view.healthBarItemPrefab, _view.container);
        var hb = tmpBar.GetComponent<HealthBarItem>();
        _view.HealthBarItems.Add(hb);
        hb.Active = true;
        return _view.HealthBarItems.Count - 1;
    }

}
