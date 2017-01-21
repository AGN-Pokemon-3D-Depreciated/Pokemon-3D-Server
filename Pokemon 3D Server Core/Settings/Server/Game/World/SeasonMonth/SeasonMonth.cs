using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.World.SeasonMonth
{
    public class SeasonMonth
    {
        public List<Season> January { get; private set; } = new List<Season> { new Season() };
        public List<Season> February { get; private set; } = new List<Season> { new Season() };
        public List<Season> March { get; private set; } = new List<Season> { new Season() };
        public List<Season> April { get; private set; } = new List<Season> { new Season() };
        public List<Season> May { get; private set; } = new List<Season> { new Season() };
        public List<Season> June { get; private set; } = new List<Season> { new Season() };
        public List<Season> July { get; private set; } = new List<Season> { new Season() };
        public List<Season> August { get; private set; } = new List<Season> { new Season() };
        public List<Season> September { get; private set; } = new List<Season> { new Season() };
        public List<Season> October { get; private set; } = new List<Season> { new Season() };
        public List<Season> November { get; private set; } = new List<Season> { new Season() };
        public List<Season> December { get; private set; } = new List<Season> { new Season() };
    }
}