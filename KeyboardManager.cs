using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisGame
{
    public class KeyboardManager
    {
        private readonly Dictionary<Keys, (int dx, int dy)> keyMappings = new();

        public void AddKeyMapping(Keys key, int dx, int dy)
        {
            keyMappings[key] = (dx, dy);
        }

        public (int dx, int dy) GetDirection(Keys key)
        {
            return keyMappings.TryGetValue(key, out var direction) ? direction : (0, 0);
        }

    }
}
