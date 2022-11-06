using System.Collections.Generic;

namespace _Project.Scripts.Core.PauseSystem
{
    public class PauseManager
    {
        public static PauseManager Current;
        public bool IsOnPause;
        
        private readonly List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        public static void Initialize()
        {
            Current = new PauseManager();
        }

        public void Register(IPauseHandler pauseHandler)
        {
            if (_pauseHandlers.Contains(pauseHandler)) return;
            _pauseHandlers.Add(pauseHandler);
            if (IsOnPause) pauseHandler.SetPause();
        }

        public void StartPause()
        {
            IsOnPause = true; 
            _pauseHandlers.ForEach(x => x.SetPause());
        }

        public void StopPause()
        {
            IsOnPause = false; 
            _pauseHandlers.ForEach(x => x.Play());
        }
    }

    public interface IPauseHandler
    {
        public void SetPause();
        public void Play();
    }
}