using AutoItX3Lib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoFish
{
    public class AutoArcheRage : IAuto
    {
        private const string WIN_TITLE = "- ArcheRage DX11 - 5.0.7.0.RM (r.420490) Jan  4 2019 (11:30:34) ArcheRage";
        private readonly string windowTitle;
        private readonly AutoItX3 ai;
        private Task task;
        private int repeatedly;
        private Action everyCycleAction;

        public AutoArcheRage(string _windowTitle)
        {
            ai = new AutoItX3();
            windowTitle = _windowTitle;
        }

        public void Start()
        {
            Start(-1, () => { });
        }

        public void Start(int count, Action _everyCycleAction)
        {
            everyCycleAction = _everyCycleAction;
            repeatedly = count;

            task = new Task(StartWrok);
            task.Start();
        }

        public void Stop()
        {
            repeatedly = 0;
            ai.WinActivate(windowTitle);
        }

        public int GetCountLeft()
        {
            return repeatedly;
        }

        private void StartWrok()
        {
            ai.WinActivate(WIN_TITLE);
            ai.WinWaitActive(WIN_TITLE);

            while (ai.WinActive(WIN_TITLE) == 1 && repeatedly > 0)
            {
                if (ai.PixelGetColor(865, 539) == 0x26AEF6)
                {
                    ai.MouseClick("LEFT", 865, 539, 1, 10);
                    ai.MouseMove(686, 313, 10);
                    while (ai.PixelGetColor(865, 539) == 0x26AEF6)
                    {
                        ai.MouseClick("LEFT", 686, 313, 1, 10);
                        Thread.Sleep(500);
                    }
                    while (ai.PixelGetColor(545, 612) != 0x121415) { }
                    repeatedly--;
                    everyCycleAction();
                }
            }
        }
    }
}
