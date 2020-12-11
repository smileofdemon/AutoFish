using System;

namespace AutoFish
{
    public interface IAuto
    {
        void Start();
        void Start(int count, Action everyCycleAction);
        void Stop();
        int GetCountLeft();
    }
}
