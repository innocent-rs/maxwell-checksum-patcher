namespace ChecksumPatcher;

public class CE002 : CE000
{
    private new const int FE000 = 0;
    private const int FE001 = 1;
    private const int FE002 = 2;
    private const int FE003 = 4;
    private readonly int FE004;
    public readonly List<uint> FE005;

    public string PE000
    {
        get
        {
            return CE027.E002(base.E001, base.E000, this.FE004);
        }
    }

    public char PE001
    {
        get
        {
            return (char)base.E001[base.E000];
        }
    }

    public byte PE002
    {
        get
        {
            return base.E001[base.E000 + 1];
        }
    }

    public ushort PE003
    {
        get
        {
            return BitConverter.ToUInt16(base.E001, base.E000 + 2);
        }
        set
        {
            CE027.E004(value, base.E001, base.E000 + 2);
        }
    }

    public ushort PE004
    {
        get
        {
            return BitConverter.ToUInt16(base.E001, base.E000 + 4);
        }
        set
        {
            CE027.E004(value, base.E001, base.E000 + 4);
        }
    }

    public CE002(byte[] param0, int param1, int param2)
        : base(param0, param1)
    {
        this.FE004 = param2;
    }
}