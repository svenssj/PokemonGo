namespace CommunityNotifier.Core.Domain.Model
{
    public enum AreaEnum
    {
        EjValt,
        Centrum,
        Oxbacken,
        Djakneberget,
        Bjurhovda,
        Viksang,
        Skiljebo,
        Haga,
        Malmaberg,
        Hemdal,
        Logarangen,
        Hamnen,
        Raby,
        Vetterslund,
        Klockartorpet,
        Backby,
        Vallby,
        Pettersberg,
        Nordanby,
        Gideonsberg,
        Skallberget,
        Gryta,
        Skälby,
        Hamre,
        Notudden
    }


    public static class AreaMapper
    {
        public static AreaEnum MapFromStringToEnum(string area)
        {
            area = area.ToLower();
            if (area == AreaEnum.Centrum.ToString().ToLower())
            {
                return AreaEnum.Centrum;
            }
            if (area == AreaEnum.Haga.ToString().ToLower())
            {
                return AreaEnum.Haga;
            }
            if (area == AreaEnum.Malmaberg.ToString().ToLower())
            {
                return AreaEnum.Malmaberg;
            }
            if (area == AreaEnum.Oxbacken.ToString().ToLower())
            {
                return AreaEnum.Oxbacken;
            }
            if (area == AreaEnum.Djakneberget.ToString().ToLower())
            {
                return AreaEnum.Djakneberget;
            }
            if (area == AreaEnum.Viksang.ToString().ToLower())
            {
                return AreaEnum.Viksang;
            }
            if (area == AreaEnum.Skiljebo.ToString().ToLower())
            {
                return AreaEnum.Skiljebo;
            }
            if (area == AreaEnum.Hemdal.ToString().ToLower())
            {
                return AreaEnum.Hemdal;
            }
            if (area == AreaEnum.Bjurhovda.ToString().ToLower())
                return AreaEnum.Bjurhovda;
            else
            {
                return AreaEnum.EjValt;

            }
        }
    }
}