namespace ClubeDaLeitura.ModuloMulta;

public class RepositorioMulta
{
    public Multa[] Multas = new Multa[100];
    public int IndiceListaMultas = 0;
    public bool ListaVazia = false;

    public void RegistrarMulta(Multa novaMulta)
    {
        novaMulta.GerarId();
        Multas[IndiceListaMultas++] = novaMulta;
    }
    public Multa[] PegarListaMultasPendentes()
    {
        Multa[] multasPendentes = new Multa[100];

        for (int i = 0; i < Multas.Length; i++)
        {
            if (Multas[i] == null)
                continue;

            if (Multas[i].Status == "Pendente")
                multasPendentes[i] = Multas[i];
        }

        return multasPendentes;
    }
    public Multa SelecionarPorId(int idMultaEscolhida)
    {
        foreach (Multa m in Multas)
        {
            if (m == null)
                continue;

            if (m.Id == idMultaEscolhida)
                return m;
        }

        return null!;
    }
    public bool VerificarMultaQuitada(Multa multaEscolhida)
    {
        if (multaEscolhida == null)
            return false;

        if (multaEscolhida.Status == "Quitada")
            return true;
        else
            return false;
    }
}
