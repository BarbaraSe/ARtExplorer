using System.Collections.Generic;

[System.Serializable]
public class PaintingInfo
{
    public string Significance;
    public List<AboutPaintingItem> AboutPainting;
    public List<DetailItem> AboutArtist;
    public List<DetailItem> AboutEra;
    public List<DetailItem> FunFacts;
    public List<DetailItem> KeyPointsEra;
}

[System.Serializable]
public class DetailItem
{
    public string Title;
    public string Detail;
}

[System.Serializable]
public class AboutPaintingItem
{
    public string Title;
    public string Artist;
    public string Medium;
    public string Date;
}

    [System.Serializable]
    public class Wrapper
    {
        public List<PaintingInfo> Paintings;
    }