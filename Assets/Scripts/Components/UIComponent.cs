using System;

namespace Component {
    [Serializable]
    public struct PanelState
    {
       public TMPro.TextMeshProUGUI Rotate;
       public TMPro.TextMeshProUGUI Speed;
       public TMPro.TextMeshProUGUI Coordinate;
       public TMPro.TextMeshProUGUI Score;
       public TMPro.TextMeshProUGUI Laser;
       public MonoBeh.Entity UpdateLaser;
       public MonoBeh.GameOverPanel GameOverPanel;
    }

    [Serializable]
    public struct UpdateLaser
    {
        public TMPro.TextMeshProUGUI Text;
        public UnityEngine.UI.Slider Slider;
        public float Value;
    }
}