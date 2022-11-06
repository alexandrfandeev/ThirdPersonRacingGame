using System.Collections.Generic;
using _Project.Scripts.GUi.Race;

namespace _Project.Scripts.Core
{
    public class UIUpdatesContainer
    {
        public static UIUpdatesContainer Current;
        
        private readonly List<IUpdateUI> _updatedUI = new List<IUpdateUI>();
        
        public static void Initialize()
        {
            Current = new UIUpdatesContainer();
        }
        
        public void Register(IUpdateUI updateSearcher)
        {
            if (_updatedUI.Contains(updateSearcher)) return;
            _updatedUI.Add(updateSearcher);
        }

        public void UpdateFloat(float value)
        {
            _updatedUI.ForEach(x => x.UpdateFloat(value));
        }

        public void UpdateInt(int value)
        {
            _updatedUI.ForEach(x => x.UpdateInt(value));
        }
    }
    
    public interface IUpdateUI
    {
        public void UpdateFloat(float value);
        public void UpdateInt(int number);
    }
}
