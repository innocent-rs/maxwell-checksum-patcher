// See https://aka.ms/new-console-template for more information

public class CE000
{
    protected int E000;
    protected byte[] E001;

    public int PE000
    {
        get
        {
            return this.E000;
        }
    }

    public CE000(byte[] param0, int param1)
    {
        this.E000 = param1;
        this.E001 = param0;
    }
}