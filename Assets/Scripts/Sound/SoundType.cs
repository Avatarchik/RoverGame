public enum SoundType
{
    None = 0x000000,
    Any  = 0xffffff,

    Effect = (1 << 0),
    Music  = (2 << 1),
    Speech = (3 << 2)
}

public static class SoundTypeExtensions
{
    public static SoundType Combine (params SoundType[] types)
    {
        int mask = 0;
        foreach(SoundType type in types)
        {
            mask |= ((int) type);
        }

        return (SoundType) mask;
    }


    public static bool Intersects (this SoundType mask, SoundType type)
    {
        return (mask & type) > 0;
    }
}

