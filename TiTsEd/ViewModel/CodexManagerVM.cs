using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiTsEd.Common;
using TiTsEd.Model;

namespace TiTsEd.ViewModel
{
    public class CodexManagerVM : ObjectVM
    {
        public GameVM Game { get; private set; }

        public const string PROP_UNLOCKEDCODEXENTRIES = "unlockedCodexEntries";
        public const string PROP_VIEWEDCODEXENTRIES = "viewedCodexEntries";

        public CodexManagerVM(GameVM game, AmfObject obj)
            : base(obj)
        {
            Game = game;
        }

        public List<XmlCodexEntry> AllCodexEntries
        {
            get;
            private set;
        }

        public AmfObject ViewedEntries
        {
            get
            {
                return GetObj(PROP_VIEWEDCODEXENTRIES) ?? new AmfObject(AmfTypes.Array);
            }
        }

        public AmfObject UnlockedEntries
        {
            get
            {
                return GetObj(PROP_UNLOCKEDCODEXENTRIES) ?? new AmfObject(AmfTypes.Array);
            }
        }

        public bool IsUnlocked(string name)
        {
            return UnlockedEntries.Contains(name);
        }

        public void Unlock(string name, bool unlocked)
        {
            if (null != name)
            {
                bool isUnlocked = IsUnlocked(name);
                if (unlocked)
                {
                    if (!isUnlocked)
                    {
                        UnlockedEntries.Push(name);
                        OnPropertyChanged(PROP_UNLOCKEDCODEXENTRIES);
                    }
                }
                else
                {
                    if (isUnlocked)
                    {
                        // remove it
                        OnPropertyChanged(PROP_UNLOCKEDCODEXENTRIES);
                    }
                }
            }
        }

        public void SetViewed(string name, bool viewed)
        {
            if (null != name)
            {
                bool isViewed = IsViewed(name);
                if (viewed)
                {
                    if (!isViewed)
                    {
                        ViewedEntries.Push(name);
                        OnPropertyChanged(PROP_VIEWEDCODEXENTRIES);
                    }
                }
                else
                {
                    if (isViewed)
                    {
                        // remove it
                        OnPropertyChanged(PROP_VIEWEDCODEXENTRIES);
                    }
                }
            }
        }

        public bool IsViewed(string name)
        {
            var obj = GetObj(PROP_VIEWEDCODEXENTRIES);
            return obj.Contains(name);
        }
    }
}
