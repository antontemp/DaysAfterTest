using System;
using Script.Core.Implementation;
using Script.InputSpace;

namespace Script.Installer
{
    [Serializable]
    public class ControllerSettings
    {
        public KeyboardInput controllerBase;
        public MainManager mainManager;
        public MainUIController mainUIController;
        public Loader loader;
    }

}