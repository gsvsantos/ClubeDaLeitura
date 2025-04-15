using ClubeDaLeitura.ModuloAmigo;

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
    public Multa[] SelecionarMultasPendentes()
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
    public Multa[] SelecionarMultasAmigo(Amigo amigoEscolhido)
    {
        if (amigoEscolhido.Multas == null)
            return null!;

        Multa[] multasAmigoEscolhido = new Multa[100];

        for (int i = 0; i < amigoEscolhido.Multas.Length; i++)
        {
            if (amigoEscolhido.Multas[i] == null)
                continue;

            else
                multasAmigoEscolhido[i] = amigoEscolhido.Multas[i];
        }

        return multasAmigoEscolhido;
    }
    public void ExcluirMulta(Multa multaEscolhida)
    {

        for (int i = 0; i < Multas.Length; i++)
        {
            if (Multas[i] == null)
                continue;
            else if (Multas[i].Id == multaEscolhida.Id)
            {
                Multas[i] = null!;
                break;
            }
        }
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
}
