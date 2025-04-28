using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloCaixa;
public class RepositorioCaixaEmMemoria : RepositorioBaseEmMemoria<Caixa>, IRepositorioCaixa
{
    public bool VerificarEtiquetasNovoRegistro(Caixa novaCaixa)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            if (novaCaixa.Etiqueta == Registros[i].Etiqueta)
                return true;
        }

        return false;
    }
    public bool VerificarEtiquetasEditarRegistro(Caixa caixaEscolhida, Caixa dadosEditados)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            if (dadosEditados.Etiqueta == Registros[i].Etiqueta && caixaEscolhida.Id != Registros[i].Id)
                return true;
        }

        return false;
    }
}
