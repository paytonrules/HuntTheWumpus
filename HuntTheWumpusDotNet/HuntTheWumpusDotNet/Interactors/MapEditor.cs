using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet.Interactors
{
    public class MapEditor
    {
        private Map map;
        public MapEditor(Map map)
        {
            this.map = map;
        }

        public void PutInCavern(String item, int cavern)
        {
            map.PlaceItem(cavern,
                          ConvertStringToMapItem(item));
        }

        private static MapItems ConvertStringToMapItem(string item)
        {
            return (MapItems) Enum.Parse(typeof (MapItems), Capitalize(item));
        }

        static private String Capitalize(String player)
        {
            return char.ToUpper(player[0]) + player.Substring(1);
        }

        public void AddPath(int startCavern, int endCavern, char direction)
        {
            map.AddPath(startCavern, endCavern, GamePresenter.ConvertCharToDirection(direction));
        }
    }
}
