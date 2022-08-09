using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponetns.Editor.Model
{
    public enum GameComponentType
    {
        Catastrophe,
        #region bunker
        BunkerEnviroment,
        BunkerWall,
        ItemBunker,
        BunkerObject,
        #endregion
        #region Character
        Phobia,
        Hobby,
        AdditionalInformation,
        Health,
        CharacterItem,
        Profession,
        Trait,
        Card,
        #endregion
        ExternalSurrounding
    }
}
