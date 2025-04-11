namespace ClubeDaLeitura.ModuloCaixa;
public class RepositorioCaixa
{
    public Caixa[] Caixas = new Caixa[40];
    public int IndiceListaCaixa = 0;

    public void RegistrarCaixa(Caixa novaCaixa)
    {
        novaCaixa.GerarId();
        Caixas[IndiceListaCaixa++] = novaCaixa;
    }
    public Caixa[] PegarListaRegistrados()
    {
        return Caixas;
    }
    public void EditarCaixa(Caixa caixaEscolhida, Caixa dadosEditados)
    {
        caixaEscolhida.Etiqueta = dadosEditados.Etiqueta;
        caixaEscolhida.Cor = dadosEditados.Cor;
        caixaEscolhida.DiasEmprestimo = dadosEditados.DiasEmprestimo;
    }
    public void ExcluirCaixa(Caixa caixaEscolhida)
    {
        for (int i = 0; i < Caixas.Length; i++)
        {
            if (Caixas[i] == null)
                continue;

            else if (Caixas[i].Id == caixaEscolhida.Id)
            {
                Caixas[i] = null!;
                break;
            }
        }
    }
    public Caixa SelecionarPorId(int idCaixaEscolhida)
    {
        foreach (Caixa c in Caixas)
        {
            if (c == null)
                continue;
            if (c.Id == idCaixaEscolhida)
                return c;
        }
        return null!;
    }
    public bool VerificarEtiquetas(Caixa caixaVerificar)
    {
        for (int i = 0; i < Caixas.Length; i++)
        {
            if (caixaVerificar.Etiqueta == Caixas[i].Etiqueta)
                return true;
        }
        return false;
    }
    public bool VerificarRevistasCaixa(Caixa caixaEscolhida)
    {
        int revistas = 0;

        foreach (Caixa c in caixaEscolhida.Revistas)
        {
            if (c != null)
                revistas++;
        }

        if (revistas > 0)
            return true;
        else
            return false;
    }
}
