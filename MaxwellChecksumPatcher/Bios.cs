namespace ChecksumPatcher;

public class Bios
{
    private byte[] PE0000 = new byte[2]
    {
        85,
        170
    };

    public readonly string caminhoArquivo;
    public readonly byte[] arquivoEmBytes;
    public readonly List<LinhaBios> linhasDaBios;

    public Bios(string param0)
    {
        this.caminhoArquivo = param0;
        this.arquivoEmBytes = File.ReadAllBytes(param0);
        this.linhasDaBios = this.LerBios();
    }

    private List<LinhaBios> LerBios()
    {
        List<LinhaBios> list = new List<LinhaBios>();
        
        int num = CE027.E000(this.arquivoEmBytes, this.PE0000, 0, false, new byte?());
        
        if (num > -1)
        {
            RomHeader obj;
            do
            {
                obj = new RomHeader(this.arquivoEmBytes, num);
                if (obj.E00B.PE000)
                {
                    list.Add(new LinhaBios(this.arquivoEmBytes, num));
                    num += obj.E00B.PE004;
                }
                else
                    break;
            }
            while (!obj.E00B.PE005);
        }
        return list;
    }

    public void EscreverBios(string param0)
    {
        foreach (LinhaBios obj in this.linhasDaBios)
            obj.ObterImageChecksum();

        File.WriteAllBytes(param0, this.arquivoEmBytes);
    }
}