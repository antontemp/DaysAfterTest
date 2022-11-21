using Script.Core.Implementation;
using UnityEngine;
using Zenject;

namespace Script.Core
{
    public class PlayerGui : MonoBehaviour
    {
        [SerializeField]
        float _leftPadding;

        [SerializeField]
        float _topPadding;

        [SerializeField]
        float _labelWidth;

        [SerializeField]
        float _labelHeight;

        [SerializeField]
        float _textureWidth;

        [SerializeField]
        float _textureHeight;

        [SerializeField]
        Color _foregroundColor;

        [SerializeField]
        Color _backgroundColor;

        private IMainManager _mainManager;
       
        private Texture2D _textureForeground;
        private Texture2D _textureBackground;

        [Inject]
        private void Construct(IMainManager manager)
        {
            _mainManager = manager;
        }
        public void Start()
        {
            _textureForeground = CreateColorTexture(_foregroundColor);
            _textureBackground = CreateColorTexture(_backgroundColor);
        }

        Texture2D CreateColorTexture(Color color)
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, color);
            texture.Apply();
            return texture;
        }

        public void OnGUI()
        {
            var player = _mainManager.ActivePlayer;
            var healthLabelBounds = new Rect(_leftPadding,  _topPadding, _labelWidth, _labelHeight);
            GUI.Label(healthLabelBounds, $"Health: {player.Settings.health}");

            var boundsBackground = new Rect(healthLabelBounds.xMax, healthLabelBounds.yMin, _textureWidth, _textureHeight);
            GUI.DrawTexture(boundsBackground, _textureBackground);

            var boundsForeground = new Rect(boundsBackground.xMin, boundsBackground.yMin, (player.Settings.health / 100.0f) * _textureWidth, _textureHeight);
            GUI.DrawTexture(boundsForeground, _textureForeground);
        }
    }
}
