﻿public class CE02B : CE000
{
    private byte[] E005 = new byte[4]
    {
        80,
        67,
        73,
        82
    };
    private new const int FE000 = 4;
    private const int FE001 = 6;
    private const int FE002 = 16;
    private const int FE003 = 21;
    private const int FE004 = 295;

    public bool PE000
    {
        get
        {
            return CE027.E000(base.E001, this.E005, base.E000, false, new byte?()) == base.E000;
        }
    }

    public ushort PE001
    {
        get
        {
            return BitConverter.ToUInt16(base.E001, base.E000 + 4);
        }
    }

    public ushort PE002
    {
        get
        {
            return BitConverter.ToUInt16(base.E001, base.E000 + 6);
        }
    }

    public ushort PE003
    {
        get
        {
            return BitConverter.ToUInt16(base.E001, base.E000 + 295);
        }
    }

    public int PE004
    {
        get
        {
            return BitConverter.ToUInt16(base.E001, base.E000 + 16) * 512;
        }
    }

    public bool PE005
    {
        get
        {
            return (base.E001[base.E000 + 21] & 128) == 128;
        }
    }

    public CE02B(byte[] param0, int param1)
        : base(param0, param1)
    {
    }
}